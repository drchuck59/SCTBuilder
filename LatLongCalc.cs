using System;
using System.Linq;
using System.Threading.Tasks;

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
        public static double NMperLongDegree(double Latitude)
        {
                // Assumes all Lat/Longs are in Decimal degrees
                double Lat = Deg2Rad(Latitude);
                double result = Math.Cos(Lat) * EarthRadius('N');
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
            return degrees;
        }

        public static double Distance(double lat1, double lon1, double lat2, double lon2, char unit)
        // Calculate Distance between to coordinates (Haversine method)
        {
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(Deg2Rad(lat1)) * Math.Sin(Deg2Rad(lat2)) +
                    Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) * Math.Cos(Deg2Rad(theta));
                dist = Math.Acos(dist);
                dist = Rad2Deg(dist);
                dist = dist * 60 * 1.1515;
                if (unit == 'K')
                {
                    dist *= 1.609344;
                }
                else if (unit == 'N')
                {
                    dist *= 0.8684;
                }
                return (dist);
            }
        }

        public static double Bearing(double StartLat, double StartLon, double EndLat, double EndLon)
        {
            // Math is done in radians
            double Lat1 = Deg2Rad(StartLat);
            double Lat2 = Deg2Rad(EndLat);
            double dLon = Deg2Rad(EndLon - StartLon);
            double x = Math.Cos(Lat2) * Math.Sin(dLon);
            double y = Math.Cos(Lat1) * Math.Sin(Lat2) - Math.Sin(Lat1) * Math.Cos(Lat2) * Math.Cos(dLon);
            double Brg = Math.Atan2(y, x);
            Brg = Rad2Deg(Brg);
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

        public static double[] CalcLocation(double Lat1, double Lon1, double Dist, double Brg, char Type = 'S')
        {
            double R = EarthRadius(Type);           // Radius of earth to match distance vector
            double radLat1 = Deg2Rad(Lat1);         // All math must be in radians
            double radLon1 = Deg2Rad(Lon1);
            double radBrg = Deg2Rad(Brg);
            double Lat2 = Math.Asin(Math.Sin(radLat1) * Math.Cos(Dist / R) +
                Math.Cos(radLat1) * Math.Sin(Dist / R) * Math.Cos(radBrg));
            double Lon2 = radLon1 + Math.Atan2(Math.Sin(radBrg) * Math.Sin(Dist / R) * Math.Cos(radLat1),
                                Math.Cos(Dist / R) - Math.Sin(radLat1) * Math.Sin(Lat2));
            double[] Endpoint = new double[]
            {
                Rad2Deg(Lat2),
                Rad2Deg(Lon2)
            };
            return Endpoint;
            }
        }
    }

