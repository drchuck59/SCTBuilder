using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SCTBuilder
{

    class LatLonParser
    {
        private static double decLatitude;
        private static double decLongitude;
        public static double ParsedLatitude
        {
            get { return decLatitude; }
            set { decLatitude = value; }
        }
        public static double ParsedLongitude
        {
            get { return decLongitude; }
            set { decLongitude = value; }
        }


        // Can add Degree and DegreeMinute testing if needed
        private const string DegreePattern = @"
^\s*                 # Ignore any whitespace at the start of the string
(?<latSuf>[NS])?     # Optional suffix
(?<latDeg>.+?)       # Match anything and we'll try to parse it later
[D\*\u00B0]?\s*      # Degree symbol ([D|*|°] optional) followed by optional whitespace
(?<latSuf>[NS])?\s+  # Suffix could also be here. Need some whitespace to separate

(?<lonSuf>[EW])?     # Now try the longitude
(?<lonDeg>.+?)       # Degrees
[D\*\u00B0]?\s*      # Degree symbol + whitespace
(?<lonSuf>[EW])?     # Optional suffix
\s*$                 # Match the end of the string (ignoring whitespace)";

        // DO NOT DELETE! THIS WORKS!
        Regex rgxLat = new Regex(@"^(?<latSuf>[NS])?(?<latDeg>\d{1,3})[D\*\u00B0\u00BA\-\.\s](?<latMin>\d{1,2})[M'\.\u2032\u2019\s](?<latSec>.+?)[\u0022\u2033\u201D]?(?<latSuf>[NS])?$");
        Regex rgxLon = new Regex(@"^(?<lonSuf>[EW])?(?<lonDeg>\d{1,3})[D\*\u00B0\u00BA\-\.\s](?<lonMin>\d{1,2})[M'\.\u2032\u2019\s](?<lonSec>.+?)[\u0022\u2033\u201D]?(?<lonSuf>[EW])?$");
        Regex rgxDMS2 = new Regex(@"^(?<latSuf>[NS])?(?<latDeg>\d{1,3})[D\*\u00B0\u00BA\-\.\s](?<latMin>\d{1,2})[M'\.\u2032\u2019\s](?<latSec>.+?)[\u0022\u2033\u201D]?(?<latSuf>[NS])?(\s+)?(?<lonSuf>[EW])?(?<lonDeg>\d{1,3})[D\*\u00B0\u00BA\-\.\s](?<lonMin>\d{1,2})[M'\.\u2032\u2019\s](?<lonSec>.+?)[\u0022\u2033\u201D]?(?<lonSuf>[EW])?$");
        Regex rgxDD = new Regex(@"^(?<negate>(\-))?(?<Degrees>\d{1,3})(\.)?(?<Decimals>\d+)?$");
        Regex rgxDD2 = new Regex(@"^(?<negateLat>(\-))?(?<Degrees>\d{1,3})(\.)?(?<Decimals>\d+)?(\s+)?(?<negateLon>(\-))?(?<Degrees>\d{1,3})(\.)?(?<Decimals>\d+)?$");
        Regex rgxISO = new Regex(@"^\s*(?<latitude> [+-][0-9]{2,6}(?: \. [0-9]+)?)(?<longitude>[+-][0-9]{3,7}(?: \. [0-9]+)?)(?<altitude> [+-][0-9]+(?: \. [0-9]+)?)?/");

        private string cr = Environment.NewLine;
        // Same as above, but a constant string
        private const string DecimalDegrees = @"^ (?<negate>(\-))?(?<Degrees>\d{1,3})(\.)? (?<Decimals>\d+)?$";
        private const string DecDegree2 = @"^(?<latNegate>(\-))?(?<latDegrees>\d{1,3})(\.)?(?<latDecimals>\d+)?(\s+)?(?<lonNegate>(\-))?(?<lonDegrees>\d{1,3})(\.)?(?<lonDecimals>\d+)?$";
        private const string LatLonDMS = @"^(?<latSuf>[NS])?(?<latDeg>\d{1,3})[D\*\u00B0\u00BA\-\.\s](?<latMin>\d{1,2})[M'\.\u2032\u2019\s](?<latSec>.+?)[\u0022\u2033\u201D]?(?<latSuf>[NS])?(\s+)?(?<lonSuf>[EW])?(?<lonDeg>\d{1,3})[D\*\u00B0\u00BA\-\.\s](?<lonMin>\d{1,2})[M'\.\u2032\u2019\s](?<lonSec>.+?)[\u0022\u2033\u201D]?(?<lonSuf>[EW])?$";
        private const string LatitudeDMS = @"^(?<latSuf>[NS])?(?<latDeg>\d{1,3})[D\*\u00B0\u00BA\-\.\s](?<latMin>\d{1,2})[M'\.\u2032\u2019\s](?<latSec>.+?)[\u0022\u2033\u201D]?(?<latSuf>[NS])?$";
        private const string LongitudeDMS = @"^(?<lonSuf>[EW])?(?<lonDeg>\d{1,3})[D\*\u00B0\u00BA\-\.\s](?<lonMin>\d{1,2})[M'\.\u2032\u2019\s](?<lonSec>.+?)[\u0022\u2033\u201D]?(?<lonSuf>[EW])?$";
        private const string IsoPattern = @"^\s*(?<latitude> [+-][0-9]{2,6}(?: \. [0-9]+)?)(?<longitude>[+-][0-9]{3,7}(?: \. [0-9]+)?)(?<altitude> [+-][0-9]+(?: \. [0-9]+)?)?/";
        // IsoPattern is untested
        private const RegexOptions Options = RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase;

        //private static readonly Regex degreeRegex = new Regex(DegreePattern, Options);
        //private static readonly Regex degreeMinuteRegex = new Regex(DegreeMinutePattern, Options);
        private static readonly Regex degreeMinuteSecondRegex =
            new Regex(LatLonDMS, Options);
        private static readonly Regex LatitudeRegex =
            new Regex(LatitudeDMS, Options);
        private static readonly Regex decimalDegreesOneRegex =
            new Regex(DecimalDegrees, Options);
        private static readonly Regex decimalDegreesTwoRegex =
            new Regex(DecDegree2, Options);

        // Have one calling routing and go to each regex
        // If successful, 'Parsed...' will have the value(s) if needed
        public static bool TryParseAny(TextBox tb)
        {
            string input = tb.Text; 
            string tbName = tb.Name;
            int typeBox = 0;
            if (tbName.IndexOf("Lat") != -1) typeBox = 1;
            if (tbName.IndexOf("Lon") != -1) typeBox = 2;
            switch (typeBox)
            {
                case 0:
                    if (TryLatLonDMS(input)) return true;
                    if (TryDecDeg2(input)) return true;
                    break;
                case 1:
                    if (TryLatLonDMS(input)) return true;
                    if (TryLatDMS(input)) return true;
                    if (TryDecDeg(input, typeBox)) return true;
                    break;
                case 2:
                    if (TryLatLonDMS(input)) return true;
                    if (TryLonDMS(input)) return true;
                    if (TryDecDeg(input, typeBox)) return true;
                    break;
            }
            return false;
        }

        private static bool TryLatLonDMS(string input)
        {
            // Parses a latitude and longitude coordinate with degree, minute and seconds.
            // Returns null if no match
            string[] names;
            string inputTest = input.Trim();
            Regex rgx = degreeMinuteSecondRegex;
            Match m = rgx.Match(inputTest.Trim());
            bool success = m.Success;
            if (m.Success)
            {
                Debug.WriteLine("LatLon DMS successs...");
                names = rgx.GetGroupNames();
                foreach (var name in names)
                {
                    Group grp = m.Groups[name];
                    Debug.WriteLine("Group {0}: {1}", name, grp.Value);
                }
                success = DMSToDD(input, rgx);
            }
            else
            {
                Debug.WriteLine("LatLon Match failed.");
            }
            return success;
        }

        private static bool TryLatDMS(string input)
        {
            // Parses a latitude coordinate with degree, minute and seconds.
            // Returns null if no match, Returns decimal degrees if match
            string[] names;
            string inputTest = input.Trim();
            Regex rgx = LatitudeRegex;
            Match m = rgx.Match(inputTest.Trim());
            bool success = m.Success;
            if (success)
            {
                Debug.WriteLine("LatDMS success...");
                names = rgx.GetGroupNames();
                foreach (var name in names)
                {
                    Group grp = m.Groups[name];
                    Debug.WriteLine("Group {0}: {1}", name, grp.Value);
                    if ((name == "latSuf") && (grp.Value.Trim().Length == 0))
                    {
                        Debug.WriteLine("latSuf value empty, Success value = " + m.Success.ToString());
                        success = false;
                    }
                }
                success = DMSToDD(input, rgx);
            }
            else
            {
                Debug.WriteLine("Latitude Match failed.");
                success = false;
            }
            return success;
        }

        private static bool TryLonDMS(string input)
        {
            // Parses a latitude coordinate with degree, minute and seconds.
            // Returns null if no match, Returns decimal degrees if match
            string[] names;
            string inputTest = input.Trim();
            Regex rgx = new Regex(LongitudeDMS);
            Match m = rgx.Match(inputTest.Trim());
            bool success = m.Success;
            if (success)
            {
                Debug.WriteLine("LonDMS success...");
                names = rgx.GetGroupNames();
                foreach (var name in names)
                {
                    Group grp = m.Groups[name];
                    Debug.WriteLine("Group {0}: {1}", name, grp.Value);
                    if ((name == "lonSuf") && (grp.Value.Trim().Length == 0))
                    {
                        Debug.WriteLine("lonSuf value empty, Success value = " + m.Success.ToString());
                        success = false;
                    }
                }
                success = DMSToDD(input, rgx);
            }
            else
            {
                Debug.WriteLine("Longitude Match failed.");
                success = false;
            }
            return success;
        }

        public static bool TryDecDeg(string input, int Lat1Lon2)
        {
            // Parses a single coordinate with degrees in decimal format.
            // Returns null if no match, Returns decimal degrees if match
            // Returns failure if both coordinates are present
            string[] names; double value;
            string inputTest = input.Trim();
            Regex rgx = decimalDegreesOneRegex;
            Match m = rgx.Match(inputTest.Trim());
            bool success = m.Success;
            if (success)
            {
                Debug.WriteLine("Decimal Degree success...");
                names = rgx.GetGroupNames();
                foreach (var name in names)
                {
                    Group grp = m.Groups[name];
                    Debug.WriteLine("Group {0}: {1}", name, grp.Value);
                }
                value = Convert.ToDouble(m.Groups[0].Value);
                switch (Lat1Lon2)
                {
                    case 1:     // Value is a Latitude
                        if (Math.Abs(value) <= 90)
                        {
                            decLatitude = value;
                            success = true;
                        }
                        else
                        {
                            Debug.WriteLine("Latitude exceeds Abs 90 degrees.");
                            success = false;
                        }
                        break;
                    case 2:     // Value is a Longitude
                        if (Math.Abs(value) <= 180)
                        {
                            decLongitude = value;
                            success = true;
                        }
                        else
                        {
                            Debug.WriteLine("Longitude exceeds Abs 180 degrees.");
                            success = false;
                        }
                        break;
                    default:
                        success = false;
                        break;
                }
            }
            else
            {
                Debug.WriteLine("Decimal Degree Match failed.");
            }
            return success;
        }

        private static bool TryDecDeg2(string input)
        {
            // Parses latitude and longitude coordinate with degrees in decimal format.
            // Returns null if no match, Returns decimal degrees if match
            string[] names;
            string inputTest = input.Trim();
            Regex rgx = decimalDegreesTwoRegex;
            Match m = rgx.Match(inputTest.Trim());
            if (m.Success)
            {
                Debug.WriteLine("Decimal Degree Two success...");
                names = rgx.GetGroupNames();
                foreach (var name in names)
                {
                    Group grp = m.Groups[name];
                    Debug.WriteLine("Group {0}: {1}", name, grp.Value);
                }
            }
            else
            {
                Debug.WriteLine("Decimal Degree Match failed.");
            }
            return m.Success;
        }

        private static bool DMSToDD(string input, Regex rgx)
        {
            // MODIFIES global decimal value of latitude and/or longitude
            // REQUIRES a successful "Try" to assure no runtime errors.
            string[] names;
            double[] Coords = new double[2];
            bool validCoord = true;
            double LatNegate = 0.0;
            double LonNegate = 0.0;
            Debug.WriteLine("");
            string inputTest = input.Trim();
            Match m = rgx.Match(inputTest.Trim());
            if (m.Success)
            {
                Coords[0] = Coords[1] = 0.0;
                names = rgx.GetGroupNames();
                foreach (var name in names)
                {
                    Group grp = m.Groups[name];
                    Debug.WriteLine("Group {0}: {1}", name, grp.Value);
                    switch (name)
                    {
                        case "latSuf":
                            if (grp.Value == "S")
                                LatNegate = -1.0;
                            else
                                LatNegate = 1.0;
                            break;
                        case "latDeg":
                            Coords[0] = Convert.ToDouble(grp.Value);
                            break;
                        case "latMin":
                            Coords[0] += Convert.ToDouble(grp.Value) / 60.0;
                            break;
                        case "latSec":
                            Coords[0] += Convert.ToDouble(grp.Value) / 3600.0;
                            break;
                        case "lonSuf":
                            if (grp.Value == "W")
                                LonNegate = -1.0;
                            else
                                LatNegate = 1.0;
                            break;
                        case "lonDeg":
                            Coords[1] = Convert.ToDouble(grp.Value);
                            break;
                        case "lonMin":
                            Coords[1] += Convert.ToDouble(grp.Value) / 60.0;
                            break;
                        case "lonSec":
                            Coords[1] += Convert.ToDouble(grp.Value) / 3600.0;
                            break;
                        default:
                            break;
                    }
                }
                if (LatNegate != 0)
                {
                    double Lat = Coords[0] * LatNegate;
                    if (Math.Abs(Lat) <= 90)
                    {
                        decLatitude = Lat;
                        validCoord = true;
                        Debug.WriteLine("Lat: " + decLatitude.ToString());
                    }
                    else validCoord = false;

                }
                if (LonNegate != 0 && validCoord)
                {
                    double Lon = Coords[1] * LonNegate;
                    if (Math.Abs(Lon) <= 180)
                    {
                        decLongitude = Lon;
                        validCoord = true;
                        Debug.WriteLine("Lon: " + decLongitude.ToString());
                    }
                    else validCoord = false;
                }
            }
            else
            {
                Debug.WriteLine("Match failed.");
                validCoord = false;
            }
            return validCoord;
        }
    }
}
