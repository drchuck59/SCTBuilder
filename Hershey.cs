using System;
using System.Drawing;


namespace SCTBuilder
{
    public class Hershey
    {
        static readonly string cr = Environment.NewLine;
        static readonly string space = new string(' ', 27);

        public static float[] Adjust (float lat, float lon, 
            float LeftRightSeconds, float UpDownSeconds, int Angle = 0, float Scale = 1)
        {
            // X and Y are Hershey units (one char is 30 x 30)
            // Latitude is Y and Longitude is X
            float[] result = new float[2];
            PointF Origin = new PointF(lon, lat);
            SizeF Offset = new SizeF(LeftRightSeconds/3600, UpDownSeconds/3600);
            PointF Coord = PointF.Add(Origin, Offset);
            int angle = (int)InfoSection.MagneticVariation + Angle;
            Coord = LatLongCalc.RotatePoint(Coord, Origin, angle);
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

        public static string DrawSymbol(object[] FixData)
        {
            // FixData contains: ID(opt), FacilityID, Frequency(opt), Latitude, Longitude, NameOrUse, FixType
            string Lat0; string Lon0; string Lat1; string Lon1;
            int angle = (int)InfoSection.MagneticVariation;
            string Fix = FixData[1].ToString();
            string FixType = FixData[6].ToString();
            if (FixType == "FIX")
                FixType = FixData[5].ToString();
            float lat = Convert.ToSingle(FixData[3]);
            float lon = Convert.ToSingle(FixData[4]);
            int[] Symbol = SymbolRef(FixType);
            // Declare values used in loop below
            PointF[] Coords = new PointF[Symbol[0]];
            float myX; float myY;                      
            // Loop through the symbol points, creating the initial pattern
            int Counter = 0; 
            for (int i = 2; i <= Symbol[0] * 2; i += 2)
            {
                myX = Symbol[i + 1]; myY = Symbol[i];           // Lat is Y, Lon is X
                if ((myY != -1) && (myX != -1))
                    Coords[Counter] = new PointF(myX /= 3600F, myY /= 3600F);
                else
                    Coords[Counter] = new PointF(-1F, -1F);
                Counter++;
            }
            // Rotate the symbol to True North - with the first point as the origin (skip breaks)
            // WHY is Mag Var correct, but rotation is NOT?
            PointF PenUp = new PointF(-1, -1);
            for (int i = 0; i < Coords.Length; i++)
            {
                if (Coords[i] != PenUp)
                {
                    Coords[i] = LatLongCalc.RotatePoint(Coords[i], Coords[0], angle);
                }
            }
            // Get the centroid
            PointF centroid = LatLongCalc.Centroid(Coords);
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
            // Now write out the symbol strings in typical end-to-start rotation
            PointF start = PointF.Empty; PointF end;
            string Result = space + "; Symbol for " + FixType + " " + Fix;
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
                    Result += cr + SCTstrings.SSDout(Lat0, Lon0, Lat1, Lon1);
                }
                start = end;
            }
            return Result;
        }

        public static string WriteHF(string Message, double Lat, double Lon, int Angle = 0, float Scale = 1)
        {
            // Lat is 'Y'!! Lon is 'X'!!
            float curLat = (float)Lat;
            float curLon = (float)Lon;
            PointF origin = new PointF(curLon, curLat);
            PointF nextChar = origin;
            float scale = Scale / 3600F;
            int angle = (int)InfoSection.MagneticVariation + Angle;
            int AsciiC; int[] SmplxC; 
            string Result = string.Empty;
            foreach (char c in Message)
            {
                AsciiC = c;
                if ((AsciiC - 32 >= 0) & (AsciiC - 32 < 95))
                    AsciiC -= 32;
                else AsciiC = 10;     // 10 is '*' for 'error'
                SmplxC = RomanSimplex.Simplex[AsciiC];
                Result += DrawChar(c, SmplxC, nextChar, Angle, Scale);
                // Use points to update here
                SizeF offset = new SizeF(SmplxC[1] * scale, 0);
                nextChar = PointF.Add(nextChar, offset);
                nextChar = LatLongCalc.RotatePoint(nextChar, origin, angle);
                origin = nextChar;
            }
            return cr + space + "; Label " + Message + cr + Result;
        }

        private static string DrawChar(char c, int[] hFont, PointF origin, int Angle, float Scale)
        {
            // Each vector needs to be (a) rotate to the angle of the line of text and (b) Scaled
            // One unit vector = 1 second or 90-100 feet.  Use the Scale function to adjust.
            string result = string.Empty; float X; float Y; 
            bool isFirst = true;
            int angle = (int)InfoSection.MagneticVariation + Angle;
            float scale = Scale / 3600F;
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
                    end = LatLongCalc.RotatePoint(end, origin, angle);
                }
                if (!(start.IsEmpty) && !(end.IsEmpty))
                {
                    if (result.Length != 0) result += cr;
                    result +=
                        SCTstrings.CharOut(Conversions.Degrees2SCT(start.Y, true), Conversions.Degrees2SCT(start.X, false),
                        Conversions.Degrees2SCT(end.Y, true), Conversions.Degrees2SCT(end.X, false));
                    if (isFirst)
                    {
                        result += ";" + c.ToString();
                        isFirst = false;
                    }
                }
                start = end;                            // No matter what happened, move End to Start
            }
            return result;                                      // Lat Long string to draw ONE character!  (Sheesh)
        }
    }
}
