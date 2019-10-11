using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public static float NMperLongDegree(float Latitude = 200)
        {
            if (Latitude != 200)
            {
                // Assumes all Lat/Longs are in Decimal degrees!
                const double rad = Math.PI / 180;
                float NMatEquator = 69.172f;
                float radLatitude = Convert.ToSingle(Latitude * rad);
                float result = Convert.ToSingle(Math.Cos(radLatitude) * NMatEquator);
                return result;
            }
            else return 0f;

        }
        public static float GetBearing(string Heading, string RwyID)
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
        private static float CompassBearing(CompassPoints c)
        {
            switch (c)
            {
                default:
                case CompassPoints.N:
                    return 360f;
                case CompassPoints.NE:
                    return 45f;
                case CompassPoints.E:
                    return 90f;
                case CompassPoints.SE:
                    return 135f;
                case CompassPoints.S:
                    return 180f;
                case CompassPoints.SW:
                    return 235f;
                case CompassPoints.W:
                    return 270;
                case CompassPoints.NW:
                    return 315f;
            }
        }
        public static float DMS2DecDeg(float DD, float MM, float SS, string quadrant)
        {
            float result;
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
    }
}
