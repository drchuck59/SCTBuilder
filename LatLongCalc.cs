﻿using System;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Diagnostics;

namespace SCTBuilder
{
    public enum CompassPoints
    {
        /// <summary>
        /// Use to lookup bearings of common compass directions
        /// </summary>
        N = 360,
        NE = 45,
        E = 90,
        SE = 135,
        S = 180,
        SW = 225,
        W = 270,
        NW = 315,
    }

    class ArcCalculator
    {
        public struct Point2D
        {
            public double X { get; }
            public double Y { get; }
            public Point2D(double x, double y)
            {
                X = x;
                Y = y;
            }
            public override string ToString() => $"{X:N3}; {Y:N3}";
        }
        public struct Segment2D
        {
            public Point2D Start { get; }
            public Point2D End { get; }
            public double Argument => Math.Atan2(End.Y - Start.Y, End.X - Start.X);

            public Segment2D(Point2D start, Point2D end)
            {
                Start = start;
                End = end;
            }
        }
        public struct Circle2D
        {
            private const double FullCircleAngle = 2 * Math.PI;
            public Point2D Center { get; }
            public double Radius { get; }

            public Circle2D(Point2D center, double radius)
            {
                if (radius <= 0)
                    throw new ArgumentOutOfRangeException(nameof(radius));

                Center = center;
                Radius = radius;
            }

            public IEnumerable<Point2D> GetPointsOfArch(int numberOfPoints, double startAngle, double endAngle)
            {
                double normalizedEndAngle;

                if (startAngle < endAngle)
                {
                    normalizedEndAngle = endAngle;
                }
                else
                {
                    normalizedEndAngle = endAngle + FullCircleAngle;
                }

                var angleRange = normalizedEndAngle - startAngle;
                angleRange = angleRange > FullCircleAngle ? FullCircleAngle : angleRange;
                var step = angleRange / numberOfPoints;
                var currentAngle = startAngle;

                while (currentAngle <= normalizedEndAngle)
                {
                    var x = Center.X + Radius * Math.Cos(currentAngle);
                    var y = Center.Y + Radius * Math.Sin(currentAngle);
                    yield return new Point2D(x, y);
                    currentAngle += step;
                }
            }

            public IEnumerable<Point2D> GetPoints(int numberOfPoints)
                => GetPointsOfArch(numberOfPoints, 0, FullCircleAngle);
        }
    }

    class LatLongCalc
    {

        public static double[] PolarToCartesian(double angle, double radius)
        {
            // Convert polar coordinates to X/Y coordinates
            double[] Coords = new double[2];
            double angleRad = Deg2Rad(angle);
            Coords[0] = radius * Math.Cos(angleRad);
            Coords[1] = radius * Math.Sin(angleRad);
            return Coords;
        }

        public static double[] CartesianToPolar(double x, double y)
        {
            // Convert X/Y coordinates to Polar
            double[] Polar = new double[2];
            Polar[0] = Math.Sqrt((x * x) + (y * y));
            Polar[1] = Math.Atan2(y, x);
            return Polar;
        }

        public static double NMperLongDegree()
        {
            // Assumes all Lat/Longs are in Decimal degrees
            double NMperDecDegEquator = 60;
            double radCenterLat;
            // Use the user's desired center if possible
            if (InfoSection.CenterLatitude_Dec == 0)
                radCenterLat =
                    LatLongCalc.Deg2Rad((FilterBy.NorthLimit + FilterBy.SouthLimit) / 2);
            else
                radCenterLat = LatLongCalc.Deg2Rad(InfoSection.CenterLatitude_Dec);
            double result = Math.Cos(radCenterLat) * NMperDecDegEquator;
            return result;
        }

        public static double RWYBearing(string Heading, string RwyID, string ICAO, bool hardSFC)
        {
            double result = -1;
            if (RwyID.Length != 0)                      // If no RwyID, don't create a centerline
            {
                if (Heading.Length != 0)
                {
                    if (Heading.All(char.IsDigit))      // If a bearing, return it
                    {
                        result = Convert.ToDouble(Heading);
                    }
                    else                                // May be a compass bearing
                    {
                        const string cPoints = "NWNESESW";
                        if (cPoints.IndexOf(Heading, 0) != -1)
                        {
                            result = Convert.ToSingle((CompassPoints)Enum.Parse(typeof(CompassPoints), Heading, true));
                            // Debug.WriteLine("ICAO:" + ICAO + " Rwy: " + RwyID + " Compass_Brg: " + result);
                        }
                    }
                }
                else
                // Heading is empty. If the APT has an ICAO AND a conventional RWY, the RWY deserves a centerline
                {
                    if ((ICAO.Length != 0) && (hardSFC))
                    {
                        if (RwyID.All(char.IsDigit))
                        {
                            result = Convert.ToDouble(RwyID) * 10;
                            // Debug.WriteLine("ICAO:" + ICAO + " Rwy: " + RwyID + " Brg: " + result);
                        }
                        else
                        {
                            // This Regex should match any valid RWY ID
                            if (Regex.Match(RwyID, "([^A-Za-z]{0,1})([0-9]{1,2})([LCR]{0,1})([^A-Za-z])").Success)
                            {
                                if(Extensions.Right(RwyID,1).All(char.IsDigit))
                                    result = Convert.ToSingle(RwyID) * 10;
                                else
                                    result = Convert.ToSingle(RwyID.Substring(0,RwyID.Length-1)) * 10;
                                // Debug.WriteLine("ICAO:" + ICAO + " Rwy: " + RwyID + " Brg: " + result);
                            }
                            //else
                            //    Debug.WriteLine("ICAO:" + ICAO + " Rwy: " + RwyID + " No RWY Bearing");
                        }
                    }
                }
            }
            return result;
        }

        public static double DMS2DecDeg(double DD, double MM, double SS, string quadrant)
        {
            double result;
            switch (quadrant)
            {
                case "W":
                    result = -1 * (SS / 3600 + MM / 60 + DD);
                    break;
                case "S":
                    result = -1 * (SS / 3600 + MM / 60 + DD);
                    break;
                case "N":
                    result = SS / 3600 + MM / 60 + DD;
                    break;
                case "E":
                    result = SS / 3600 + MM / 60 + DD;
                    break;
                default:
                    result = -181;
                    break;
            }
            return result;
        }

        public static double Deg2Rad(double degrees)
        // Convert  degrees into radians
        {
            double radians = (Math.PI / 180.0) * degrees;
            return radians;
        }

        public static double Rad2Deg(double radians)
        // Convert  radians into degrees
        {
            double degrees = (180.0 / Math.PI) * radians;
            // Atan returns +/- 180; convert to 0->360
            return degrees;
        }


        public static PointF RotatePointF(PointF pointToRotate, PointF centerPoint, double angleInDegrees)
        {
            // Given a center point and point to rotate (aka a line), rotate the line X degrees
            double angleInRadians = LatLongCalc.Deg2Rad(angleInDegrees);
            double sinTheta = Math.Sin(angleInRadians);
            double cosTheta = Math.Cos(angleInRadians);
            return new PointF
            {
                X = (float)(cosTheta * (pointToRotate.X - centerPoint.X) +
                sinTheta * (pointToRotate.Y - centerPoint.Y)) + centerPoint.X,
                Y = (float)(sinTheta * -1 * (pointToRotate.X - centerPoint.X) +
                cosTheta * (pointToRotate.Y - centerPoint.Y) ) + centerPoint.Y,
            };
        }

        public static double Distance(double lat1, double lon1, double lat2, double lon2, char unit = 'N')
        // Calculate Distance between to coordinates (Haversine method)
        // Reference: https://www.movable-type.co.uk/scripts/latlong.html
        {
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double R = EarthRadius(unit);
                double rLat1 = Deg2Rad(lat1);
                double rLat2 = Deg2Rad(lat2);
                double deltaLat = Deg2Rad(lat2 - lat1);
                double deltaLon = Deg2Rad(lon2 - lon1);
                double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                    Math.Cos(rLat1) * Math.Cos(rLat2) *
                    Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);
                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                double d = R * c;
                return d;
            }
        }

        public static double Bearing(double Lat1, double Lon1, double Lat2, double Lon2)
        {
            // Return the initial bearing between two coordinates in degrees
            // Reference: https://www.movable-type.co.uk/scripts/latlong.html
            double rLat1 = Deg2Rad(Lat1);
            double rLon1 = Deg2Rad(Lon1);
            double rLat2 = Deg2Rad(Lat2);
            double rLon2 = Deg2Rad(Lon2);
            double y = Math.Sin(rLon2 - rLon1) * Math.Cos(rLat2);
            double x = Math.Cos(rLat1) * Math.Sin(rLat2) -
                Math.Sin(rLat1) * Math.Cos(rLat2) * Math.Cos(rLon2 - rLon1);
            double Brg = Math.Atan2(y, x);
            Brg = (Rad2Deg(Brg) + 360) % 360;
            return Brg;
        }


        private static double EarthRadius(char Type)
        {
            switch (Type)
            {
                case 'S':
                    return 3958.8;       // taken as average 
                case 'm':
                    return 6371000;
                case 'f':
                    return 3958.8 * 5280;
                case 'N':
                default:
                    return 3440.1;
            }
        }

        public static double[] Destination(double Latitude, double Longitude, double Dist, double Brg, char Type = 'N')
        {
            // Destination coordinates given distance and bearing from starting point
            // Calculates using Haversine formula (spherical earth) - up to 3% error
            // Reference: https://www.movable-type.co.uk/scripts/latlong.html

            double R = EarthRadius(Type);           // Radius of earth to match distance vector
            double radBrg = Deg2Rad(Brg);             // Angles must be in radians
            double AngDist = Dist / R;
            double radLat = Deg2Rad(Latitude);
            double radLon = Deg2Rad(Longitude);
            double Lat2 = Math.Asin(Math.Sin(radLat) * Math.Cos(AngDist) +
                          Math.Cos(radLat) * Math.Sin(AngDist) * Math.Cos(radBrg));
            double forAtanA = Math.Sin(radBrg) * Math.Sin(AngDist) * Math.Cos(radLat);
            double forAtanB = Math.Cos(AngDist) - Math.Sin(radLat) * Math.Sin(Lat2);
            double Lon2 = radLon + Math.Atan2(forAtanA, forAtanB);
            double finalLat = Rad2Deg(Lat2);
            double finalLon = NormalizedLon(Lon2);
            double[] Endpoint = new double[]
            {
                finalLat,
                finalLon
            };
            return Endpoint;
        }

        public static double[] Intersection
            (double Lat1, double Lon1, double Brg1, double Lat2, double Lon2, double Brg2)
        // This complex routine calculates the intersection of two lines
        // RETURNS coordinates of intersection
        // Reference: https://www.movable-type.co.uk/scripts/latlong.html
        {
            double[] result = new double[2];
            if (IsValidLat(Lat1) && IsValidLon(Lon1) && IsValidLat(Lat2) && IsValidLon(Lon2))
            {
                double rBrg12; double rBrg21;
                double rBrg1 = Deg2Rad(Brg1);
                double rBrg2 = Deg2Rad(Brg2);

                double Delta12 = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin((Lat2 - Lat1) / 2), 2) +
                                Math.Cos(Lat1) * Math.Cos(Lat2) * Math.Pow(Math.Sin((Lon2 - Lon1) / 2), 2)));
                double ThetaA = Math.Acos((Math.Sin(Lat2) - Math.Sin(Lat1 * Math.Cos(Delta12))) /
                                Math.Sin(Delta12 * Math.Cos(Lat1)));
                double ThetaB = Math.Acos((Math.Sin(Lat1) - Math.Sin(Lat2 * Math.Cos(Delta12))) /
                                Math.Sin(Delta12 * Math.Cos(Lat2)));
                if (Math.Sin(Lon2 - Lon1) > 0)
                {
                    rBrg12 = ThetaA;
                    rBrg21 = 2 * Math.PI - ThetaB;
                }
                else
                {
                    rBrg12 = 2 * Math.PI - ThetaA;
                    rBrg21 = ThetaB;
                }
                double Alpha1 = rBrg1 - rBrg12;
                double Alpha2 = rBrg21 - rBrg2;
                // Check if lines are parallel or divergent
                if ((Math.Sin(Alpha1) == 0) && (Math.Sin(Alpha2) == 0) ||
                    (Math.Sin(Alpha1) * Math.Sin(Alpha2) < 0))
                {
                    result[0] = result[1] = -1;     // -1,-1 to indicate "does_not_intersect"
                }
                else
                {
                    double Alpha3 = Math.Acos(-1 * Math.Cos(Alpha1) * Math.Cos(Alpha2) +
                                    Math.Sin(Alpha1) * Math.Sin(Alpha2) * Math.Cos(Delta12));
                    double Delta13 = Math.Atan2(Math.Sin(Delta12) * Math.Sin(Alpha1) * Math.Sin(Alpha2),
                                        Math.Cos(Alpha2) + Math.Cos(Alpha1) * Math.Cos(Alpha3));
                    result[0] = Math.Asin(Math.Sin(Lat1 * Math.Cos(Delta13) +
                        Math.Cos(Lat1) * Math.Sin(Delta13) * Math.Cos(rBrg1)));
                    double Lambda13 = Math.Atan2(Math.Sin(rBrg1) * Math.Sin(Delta13) * Math.Cos(Lat1),
                                        Math.Cos(Delta13) - Math.Sin(Lat1) * Math.Sin(result[0]));
                    result[1] = NormalizedLon(Lon1 + Lambda13);
                }
            }
            else result[0] = result[1] = -1;
            return result;
        }

        public static double[] Segment(double Lat1, double Lon1, double Lat2, double Lon2, double f = 0.5)
        {
            // Determines the coordinates along a percentage (as decimal) of two coordinates
            // The default is the midpoint
            double[] result = new double[2];
            if (IsValidLat(Lat1) && IsValidLon(Lon1) && IsValidLat(Lat2) && IsValidLon(Lon2))
            {
                double rLat1 = Rad2Deg(Lat1); double rLon1 = Rad2Deg(Lon1);
                double rLat2 = Rad2Deg(Lat2); double rLon2 = Rad2Deg(Lon2);
                double R = EarthRadius('N');
                double d = Distance(Lat1, Lon1, Lat2, Lon2);
                double delta = d / R;     // Angular distance
                double a = Math.Sin((1 - f) * delta) / Math.Sin(delta);
                double b = Math.Sin(f * delta) / Math.Sin(delta);
                double x = a * Math.Cos(rLat1) * Math.Cos(rLon1) + b * Math.Cos(rLat2) * Math.Cos(rLon2);
                double y = a * Math.Cos(rLat1) * Math.Sin(rLon1) + b * Math.Cos(rLat2) * Math.Sin(rLon2);
                double z = a * Math.Sin(rLat1) + b * Math.Sin(rLat2);
                result[0] = Math.Atan2(z, Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)));
                result[1] = Math.Atan2(y, x);
                result[0] = Rad2Deg(result[0]);
                result[1] = NormalizedLon(result[1]);
            }
            else result[0] = result[1] = -1;
            return result;
        }

        public static double[] Midpoint(double Lat1, double Lon1, double Lat2, double Lon2)
        {
            // Same as Segment, but allows for simpler calls to midpoint
            return Segment(Lat1, Lon1, Lat2, Lon2);
        }

        public static PointF Centroid(PointF[] Coords, int numCoords)
        {
            // Calculates the Centroid of a closed polygon from our Fix symbols
            float area = 0.0f;
            float Cx = 0; 
            float Cy = 0;
            float tmp;
            int numPoints = 0; int k;
            // While some symbols are complex, the first draw pattern is always simple
            // That is, look for a return to the origin on the first pass (a -1/-1 point)
            // After that pass, can ignore the other points
            for (int i = 2; i < numCoords; i++)
            {
                if (Coords[i].X == -1)
                {
                    break;
                }
                else numPoints += 1;
            }
            // Now we have a simple polygon to calculate the centroid
            for (int i = 0; i <= numPoints; i++)
            {
                k = (i + 1) % (numPoints + 1);
                tmp = Coords[i].X * Coords[k].Y -
                      Coords[k].X * Coords[i].Y;
                area += tmp;
                Cx += (Coords[i].X + Coords[k].X) * tmp;
                Cy += (Coords[i].Y + Coords[k].Y) * tmp;
            }
            area *= 0.5f;
            Cx *= 1.0f / (6.0f * area);
            Cy *= 1.0f / (6.0f * area);
            return new PointF(Cx, Cy);
        }

        private static double NormalizedLon(double radLon)
        {
            // Accepts Longitude in radians, returns decimal Longitude
            return (Rad2Deg(radLon) + 540) % 360 - 180;
        }

        private static double NormalizedBrg(double radBrg)
        {
            // Accepts bearing in radians, returns decimal bearing
            return (Rad2Deg(radBrg) + 540) % 360;
        }

        private static bool IsValidLat(double Lat)
        {
            return Math.Abs(Lat) <= 90;
        }

        private static bool IsValidLon(double Lon)
        {
            return Math.Abs(Lon) <= 180;
        }

        public static bool IsInsideARTCC(string ARTCC, double Latitude, double Longitude)
        {
            // Creates a polygon of the ARTCC and checks that the coords lie inside
            // Generally, avoid this approach, as most waypoints/APTs have the owning ARTCC
            // Returns true if the point p lies inside the polygon[] with n vertices 
            PointF p = new PointF((float)Latitude, (float)Longitude);
            PointF[] polygon = MakePolygon(ARTCC);
            // There must be at least 3 vertices in polygon[] 
            int n = polygon.Count();
            if (n < 3) return false;

            // Create a point for line segment from p to 10 degrees right of ARTCC limit
            float east = polygon.Max(point => point.Y) + PolygonWidth(polygon);
            PointF extreme =  new PointF (east, p.Y) ;

            // Count intersections of the above line with sides of polygon 
            int count = 0, i = 0;
            do
            {
                int next = (i + 1) % n;

                // Check if the line segment from 'p' to 'extreme' intersects 
                // with the line segment from 'polygon[i]' to 'polygon[next]' 
                if (DoIntersect(polygon[i], polygon[next], p, extreme))
                {
                    // If the point 'p' is colinear with line segment 'i-next', 
                    // then check if it lies on segment. If it lies, return true, 
                    // otherwise false 
                    if (Orientation(polygon[i], p, polygon[next]) == 0)
                        return OnSegment(polygon[i], p, polygon[next]);

                    count++;
                }
                i = next;
            } while (i != 0);

            // Return true if count is odd, false otherwise 
            return count % 2 == 1; 
        }

        private static PointF[] MakePolygon(string ARTCC)
        {
            DataView dvARB = new DataView(Form1.ARB)
            {
                Sort = "Sequence",
                RowFilter = "[ARTCC] = '" + ARTCC + "'"
            };
            PointF[] polygon = new PointF[dvARB.Count];
            if (dvARB.Count != 0)
            {
                int n = 0;
                foreach (DataRowView drvARB in dvARB)
                {
                    polygon[n] = new PointF((float)(drvARB["Latitude"]), (float)drvARB["Longitude"]);
                    n++;
                }
                polygon[n] = new PointF((float)(dvARB[0]["Latitude"]), (float)dvARB[0]["Longitude"]);
            }
            dvARB.Dispose();
            return polygon;
        }

        private static float PolygonWidth(PointF[] polygon)
        {
            float east = polygon.Max(point => point.Y);  // Largest lon cannot be smaller than -180
            float west = polygon.Min(point => point.Y);  // Largest lon cannot be smaller than -180
            return east - west;
        }

        private static bool DoIntersect(PointF p1, PointF q1, PointF p2, PointF q2)
        {
            // Find the four orientations needed for general and 
            // special cases 
            int o1 = Orientation(p1, q1, p2);
            int o2 = Orientation(p1, q1, q2);
            int o3 = Orientation(p2, q2, p1);
            int o4 = Orientation(p2, q2, q1);

            // General case 
            if (o1 != o2 && o3 != o4)
                return true;

            // Special Cases 
            // p1, q1 and p2 are colinear and p2 lies on segment p1q1 
            if (o1 == 0 && OnSegment(p1, p2, q1)) return true;

            // p1, q1 and p2 are colinear and q2 lies on segment p1q1 
            if (o2 == 0 && OnSegment(p1, q2, q1)) return true;

            // p2, q2 and p1 are colinear and p1 lies on segment p2q2 
            if (o3 == 0 && OnSegment(p2, p1, q2)) return true;

            // p2, q2 and q1 are colinear and q1 lies on segment p2q2 
            if (o4 == 0 && OnSegment(p2, q1, q2)) return true;

            return false; // Doesn't fall in any of the above cases 
        }


        private static bool OnSegment(PointF p, PointF q, PointF r)
        // Given three colinear points p, q, r, the function checks if 
        // point q lies on line segment 'pr' 
        {
            if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                    q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                return true;
            return false;
        }

        private static int Orientation(PointF p, PointF q, PointF r)
        // To find orientation of ordered triplet (p, q, r). 
        // The function returns following values 
        // 0 --> p, q and r are colinear 
        // 1 --> Clockwise 
        // 2 --> Counterclockwise 
        {
            float val = Convert.ToSingle((q.Y - p.Y) * (r.X - q.X) -
                    (q.X - p.X) * (r.Y - q.Y));

            if (val == 0) return 0; // colinear 
            return (val > 0) ? 1 : 2; // clock or counterclock wise 
        }
    }
}


