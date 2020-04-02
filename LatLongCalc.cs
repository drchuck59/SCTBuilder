using System;
using System.Linq;
using System.Data;

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
    class LatLongCalc
    {
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
            if (Heading.Length == 0)
            {
                if (!RwyID.All(char.IsDigit)) return Convert.ToSingle(RwyID.Substring(0, RwyID.Length - 1));
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
            double radians = (Math.PI / 180) * degrees;
            return radians;
        }

        public static double Rad2Deg(double radians)
        // Convert  radians into degrees
        {
            double degrees = (180 / Math.PI) * radians;
            // Atan returns +/- 180; convert to 0->360
            degrees = (degrees + 180) % 360;
            return degrees;
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
            Brg = NormalizedBrg(Brg);
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

        public static double[] Destination(double Lat1, double Lon1, double Dist, double Brg, char Type = 'S')
        {
            // Destination given distance and bearing from starting point
            // Reference: https://www.movable-type.co.uk/scripts/latlong.html
            double R = EarthRadius(Type);           // Radius of earth to match distance vector
            double rLat1 = Deg2Rad(Lat1);         // All math must be in radians
            double rLon1 = Deg2Rad(Lon1);
            double rBrg = Deg2Rad(Brg);
            double rLat2 = Math.Asin(Math.Sin(rLat1) * Math.Cos(Dist / R) +
                          Math.Cos(rLat1) * Math.Sin(Dist / R) * Math.Cos(rBrg));
            double rLon2 = rLon1 + Math.Atan2(Math.Sin(rBrg) * Math.Sin(Dist / R) * Math.Cos(rLat1),
                                Math.Cos(Dist / R) - Math.Sin(rLat1) * Math.Sin(rLat2));
            double[] Endpoint = new double[]
            {
                Rad2Deg(rLat2),
                NormalizedLon(rLon2)
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
                double rLat1 = Deg2Rad(Lat1); double rLon1 = Deg2Rad(Lon1); double rBrg1 = Deg2Rad(Brg1);
                double rLat2 = Deg2Rad(Lat2); double rLon2 = Deg2Rad(Lon2); double rBrg2 = Deg2Rad(Brg2);

                double Delta12 = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin((Lat2 - rLat1) / 2), 2) +
                                Math.Cos(rLat1) * Math.Cos(rLat2) * Math.Pow(Math.Sin((Lon2 - Lon1) / 2), 2)));
                double ThetaA = Math.Acos((Math.Sin(rLat2) - Math.Sin(rLat1 * Math.Cos(Delta12))) /
                                Math.Sin(Delta12 * Math.Cos(rLat1)));
                double ThetaB = Math.Acos((Math.Sin(rLat1) - Math.Sin(rLat2 * Math.Cos(Delta12))) /
                                Math.Sin(Delta12 * Math.Cos(rLat2)));
                if (Math.Sin(rLon2 - rLon1) > 0)
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
                    result[0] = Math.Asin(Math.Sin(rLat1 * Math.Cos(Delta13) +
                        Math.Cos(rLat1) * Math.Sin(Delta13) * Math.Cos(rBrg1)));
                    double Lambda13 = Math.Atan2(Math.Sin(rBrg1) * Math.Sin(Delta13) * Math.Cos(rLat1),
                                        Math.Cos(Delta13) - Math.Sin(rLat1) * Math.Sin(result[0]));
                    result[1] = rLon1 + Lambda13;
                    result[0] = Rad2Deg(result[0]);
                    result[1] = NormalizedLon(result[1]);
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

        public static double[] Arc_Radius(double Lat1, double Lon1, double Segment, double Radius)
        {
            double[] result = new double[2];
            // Return the the coordinates from a point ArcDegrees (always positive) given the radius
            // As the segments are expected to be small, can use trigonometry
            //double rLat1 = Deg2Rad(Lat1); double rLon1 = Deg2Rad(Lon1); double rDeg = Deg2Rad(Deg); double rArc = Deg2Rad(Radius);
            // I konw both legs are equal and one angle, so each angle is (180-angle)/2
            return result;
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

