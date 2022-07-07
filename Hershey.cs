using System;
using System.Diagnostics;
using System.Drawing;


namespace SCTBuilder
{
    public class Hershey
    {
        static readonly string cr = Environment.NewLine;
        static readonly string space = new string(' ', 27);
        public static string result;
        public static int width;

        public static float[] Adjust (float lat, float lon, 
            float LeftRightSeconds, float UpDownSeconds, int Angle = 0, float Scale = 1)
        {
            // X and Y are Hershey units (one char is 30 x 30)
            // Latitude is Y and Longitude is X
            float[] result = new float[2];
            PointF Origin = new PointF(lon, lat);
            SizeF Offset = new SizeF(LeftRightSeconds/360, UpDownSeconds/360);
            PointF Coord = PointF.Add(Origin, Offset);
            int angle = (int)InfoSection.MagneticVariation + Angle;
            Coord = LatLongCalc.RotatePointF(Coord, Origin, angle);
            result[0] = Coord.Y; result[1] = Coord.X;
            return result;
        }

        private static int[] SymbolRef(string FixType)
        {
            int[] Symbol;
            switch (FixType)
            {
                case "REP-PT":
                default:
                    Symbol = MapSymbols.FIX;
                    break;
                case "VOR":
                case "VOT":
                    Symbol = MapSymbols.VOR;
                    break;
                case "VORTAC":
                    Symbol = MapSymbols.VORTAC;
                    break;
                case "TACAN":
                    Symbol = MapSymbols.TACAN;
                    break;
                case "VOR/DME":
                    Symbol = MapSymbols.VORDME;
                    break;
                case "DME":
                    Symbol = MapSymbols.DME;
                    break;
                case "WAYPOINT":
                    Symbol = MapSymbols.WAYPOINT;
                    break;
                case "NDB/DME":
                case "MARINE NDB/DME":
                    Symbol = MapSymbols.NDBDME;
                    break;
                case "NDB":
                case "MARINE NDB":
                case "UHF/NDB":
                    Symbol = MapSymbols.NDB;
                    break;
            }
            return Symbol;
        }

        public static string DrawSymbol(object[] FixData, float Scale = 1f)
        {
            // FixData contains: ID(opt), FacilityID, Frequency(opt), Latitude, Longitude, NameOrUse, FixType
            string Result = cr;  // " (DrawSymbol StartCR) " + 
            PointF PenUp = new PointF(-1, -1);
            string Lat0; string Lon0; string Lat1; string Lon1;
            // int angle = SCTcommon.RoundUpTo10((float)InfoSection.MagneticVariation);
            string Fix = FixData[1].ToString();
            string FixType = FixData[6].ToString();
            if (FixType == "FIX")
                FixType = FixData[5].ToString();
            float lat = Convert.ToSingle(FixData[3]);
            float lon = Convert.ToSingle(FixData[4]);
            int[] Symbol = SymbolRef(FixType);
            // Declare values used in loops below
            int numCoords = Symbol[0];
            Hershey.width = Symbol[1];
            Hershey.result = string.Empty;
            PointF[] Coords = new PointF[numCoords];
            float myX; float myY;                      
            // Loop through the symbol points, creating the initial pattern
            int Counter = 0; 
            for (int i = 2; i <= Symbol[0] * 2; i += 2)
            {
                myX = Symbol[i + 1]; myY = Symbol[i];           // Lat is Y, Lon is X
                if ((myY != -1) && (myX != -1))
                    Coords[Counter] = new PointF(myX /= 3600F * Scale, myY /= 3600F * Scale);
                else
                    Coords[Counter] = new PointF(-1F, -1F);
                Counter++;
            }
            // Get the centroid

            PointF centroid = LatLongCalc.Centroid(Coords, numCoords);
            // Find the offset of the centroid from the FIX
            SizeF CentOffset = new SizeF(lon-centroid.X, lat - centroid.Y);
            // Move the symbol so it appears over the FIX
            for (int i = 0; i < Coords.Length; i++)
            {
                if (Coords[i] != PenUp)
                {
                    Coords[i] = PointF.Add(Coords[i], CentOffset);
                }
            }
            // Get the new centroid
            centroid = LatLongCalc.Centroid(Coords, numCoords);
            // Rotate the symbol to True North around the Centroid
            for (int i = 0; i < Coords.Length; i++)
            {
                if (Coords[i] != PenUp)
                {
                    Coords[i] = LatLongCalc.RotatePointF(Coords[i], Coords[0], InfoSection.MagneticVariation);
                }
            }
            // Now write out the symbol strings in typical end-to-start rotation
            PointF start = PointF.Empty; PointF end;
            Result += space + "; Symbol for " + FixType + " " + Fix;
            foreach (PointF pointF in Coords)
            {
                if (pointF != PenUp)
                    end = pointF;
                else
                    end = PointF.Empty;
                if (!(start.IsEmpty) && !(end.IsEmpty))
                {
                    Lat0 = Conversions.Degrees2SCT(start.Y, true);
                    Lat1 = Conversions.Degrees2SCT(end.Y, true);
                    Lon0 = Conversions.Degrees2SCT(start.X, false);
                    Lon1 = Conversions.Degrees2SCT(end.X, false);
                    if (Result.IndexOf(cr, Result.Length - 1) == -1) Result += cr;   // "(DrawSymbol add cr)" + 
                    Result += SCTstrings.SSDout(Lat0, Lon0, Lat1, Lon1);
                }
                start = end;
            }
            // Strip trailing crs
            while (Result.IndexOf(cr, Result.Length - 1) != -1)
            {
                Result = Result.Substring(0, Result.Length - 1);
            }
            Hershey.result = Result;
            return Result;
        }

        public static string WriteHF(string Message, double Lat, double Lon, int Angle = 0, float Scale = 1)
        {
            // Lat is 'Y'!! Lon is 'X'!!
            float curLat = (float)Lat;
            float curLon = (float)Lon;
            PointF origin = new PointF(curLon, curLat);
            PointF charAnchor = origin;
            float scale = Scale / 3600F;
            int AsciiC; int[] SmplxC; 
            string Result = Hershey.result = string.Empty;
            Hershey.width = 0;
            foreach (char c in Message)
            {
                // The string must be drawn along a line.
                // Therefore, each char must start along that line
                AsciiC = c;
                if ((AsciiC - 32 >= 0) & (AsciiC - 32 < 95))
                    AsciiC -= 32;
                else AsciiC = 10;     // 10 is '*' for 'error'
                SmplxC = RomanSimplex.Simplex[AsciiC];
                Hershey.width += SmplxC[1];
                // SmplxC[1] is the width of this character
                Result += DrawChar(c, SmplxC, charAnchor, Angle, scale);
                // Use points to update here
                SizeF offset = new SizeF(SmplxC[1] * scale, 0);
                // The new origin will be the first origin plus the width of the next char
                charAnchor = PointF.Add(charAnchor, offset);
                charAnchor = LatLongCalc.RotatePointF(charAnchor, origin, Angle);
                origin = charAnchor;
            }
            Hershey.result = Result;
            return cr + space + "; Label " + Message +  Result;  // "(WriteHF no cr)" +
        }

        private static string DrawChar(char c, int[] hFont, PointF origin, int angle, float scale)
        {
            // Each vector needs to be (a) rotate to the angle of the line of text and (b) Scaled
            // One unit vector = 1 second or 90-100 feet.  Use the Scale function to adjust.
            string result = string.Empty;
            float X; float Y; 
            bool isFirst = true;
            PointF end; PointF start = PointF.Empty; 
            // Rotate through vectors in usual manner
            for (int i = 2; i < hFont.Length; i += 2)
            {
                Y = hFont[i + 1]; X = hFont[i];                     // X-Lon, Y-Lat
                if ((X == -1) || (Y == -1))                          // Next point is a break
                    end = PointF.Empty;
                else                                                // Get next vector (which will get moved to Start)
                {
                    SizeF vector = new SizeF(X * scale, Y * scale);
                    end = PointF.Add(origin, vector);
                    end = LatLongCalc.RotatePointF(end, origin, angle);
                }
                if (!(start.IsEmpty) && !(end.IsEmpty))
                {
                     result += cr;   //if (result.IndexOf(cr, result.Length - 1) == -1) "(DrawChar add cr)" + 
                    result +=
                        SCTstrings.CharOut(Conversions.Degrees2SCT(start.Y, true), Conversions.Degrees2SCT(start.X, false),
                        Conversions.Degrees2SCT(end.Y, true), Conversions.Degrees2SCT(end.X, false));
                    // If first point of character draw, add semi-colon and character
                    if (isFirst)
                    {
                        result += ";" + c.ToString();
                        isFirst = false;
                    }
                }
                start = end;                            // No matter what happened, move End to Start
            }
            while (result.IndexOf(cr, result.Length - 1) != -1)
            {
                result = result.Substring(0, result.Length - 1) + "(DrawChar remove cr)";

            }
            return result;                                      // Lat Long string to draw ONE character!  (Sheesh)
        }
    }
}
