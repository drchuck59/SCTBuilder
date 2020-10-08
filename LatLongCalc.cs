using System;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Windows.Forms.VisualStyles;
using Renci.SshNet.Security.Cryptography;
using System.Collections.Generic;

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
            double[] Coords = new double[2];
            double angleRad = Deg2Rad(angle);
            Coords[0] = radius * Math.Cos(angleRad);
            Coords[1] = radius * Math.Sin(angleRad);
            return Coords;
        }

        public static double[] CartesianToPolar(double x, double y)
        {
            double[] Polar = new double[2];
            Polar[0] = Math.Sqrt((x * x) + (y * y));
            Polar[1] = Math.Atan2(y, x);
            return Polar;
        }

        public static double NMperLongDegree()
        {
            // Assumes all Lat/Longs are in Decimal degrees
            double DegPerNMequator = 69.172;
            double radCenterLat;
            // Use the user's desired center if possible
            if (InfoSection.CenterLatitude_Dec == 0)
                radCenterLat =
                    LatLongCalc.Deg2Rad((FilterBy.NorthLimit + FilterBy.SouthLimit) / 2);
            else
                radCenterLat = LatLongCalc.Deg2Rad(InfoSection.CenterLatitude_Dec);
            double result = Math.Cos(radCenterLat) * DegPerNMequator;
            return result;
        }

        public static double RWYBearing(string Heading, string RwyID)
        {
            if (RwyID.Length == 0) return -1;
            // If there is no heading, create one from the RWY
            if (Heading.Length == 0)
            {
                if (!RwyID.All(char.IsDigit))
                    if ((RwyID.Length > 1) &&
                        (RwyID.Substring(0, 1).All(char.IsDigit)))
                        return Convert.ToSingle(RwyID.Substring(0, RwyID.Length - 1));
                    else return -1;
                else return Convert.ToSingle(RwyID);
            }
            if (!Heading.All(char.IsDigit))      // See if this is a compass bearing
            {
                // Test for compass bearing
                const string cPoints = "NWNESESW";
                if (cPoints.IndexOf(Heading, 0) != -1)
                    return Convert.ToSingle((CompassPoints)Enum.Parse(typeof(CompassPoints), Heading, true));
                else
                {
                    return Convert.ToSingle(RwyID.Substring(0, RwyID.Length - 1));
                }
            }
            else
            {
                return Convert.ToSingle(Heading);
            }
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



        public static PointF RotatePoint(PointF pointToRotate, PointF centerPoint, double angleInDegrees)
        {
            // Given a center point and point to rotate (aka a line), rotate the line X degrees
            double radians = LatLongCalc.Deg2Rad(angleInDegrees);
            double sin = Math.Sin(radians);
            double cos = Math.Cos(radians);

            // Translate point back to origin
            pointToRotate.X -= centerPoint.X;
            pointToRotate.Y -= centerPoint.Y;

            // Rotate point
            double xnew = pointToRotate.X * cos - pointToRotate.Y * sin;
            double ynew = pointToRotate.X * sin + pointToRotate.Y * cos;

            // Translate point back
            PointF newPoint = new PointF((float)xnew + centerPoint.X, (float)ynew + centerPoint.Y);
            return newPoint;
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

        public static double[] Destination(double Latitude, double Longitude, double Dist, double Brg, char Type)
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

        public static void VincentyDestination(double Latitude, double Longitude, double Dist, double Brg, char Type)
        {
            // Destination coordinates given distance and bearing from starting point
            // Calculates using Haversine formula (spherical earth) - theoretically 0.5 millimeter error
            // Reference: https://www.movable-type.co.uk/scripts/latlong-vincenty.html
            // ** for another time **
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

        public static PointF Centroid(PointF[] Coords)
        {
            // Calculates the Centroid of a closed polygon
            // All symbols return to their origin on the first pass
            // After that pass, can ignore the other points
            float[] result = new float[2];
            float lastX = 0; float lastY = 0;
            int numPoints = 0;
            for (int i = 0; i < Coords.Length; i++)
            {
                if (Coords[i].X == -1)
                {
                    break;
                }
                else
                {
                    result[0] += Coords[i].X;
                    result[1] += Coords[i].Y;
                    lastX = Coords[i].X;
                    lastY = Coords[i].Y;
                    numPoints++;
                }
            }
            // regardless of how I got here, back out the closing point and reduce point count
            result[0] -= lastX;
            result[1] -= lastY;
            numPoints--;
            result[0] = result[0] / numPoints;
            result[1] = result[1] / numPoints;
            return new PointF(result[0], result[1]);
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
    }
}


