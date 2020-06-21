using System;
using System.Collections.Generic;
using System.IO;


namespace SCTBuilder
{
    public class Hershey
    {
        public static double[] EndPosition (string Message, double Lat, double Lon, double Angle, double Scale)
        {
            // RETURNS Lat and Long of bottom right position of Message
            // To super/sub script, add Height as "B" (Pivot is "mx+b")
            // e.g, a New position would be EndPosition + Adjust
            double B = 0f;
            double[] result = new double[2];
            double MsgWidth = Width(Message);
            double TrueAngle = Angle + InfoSection.MagneticVariation;
            result[0] = Lat + ScaleX(PivotX(TrueAngle, MsgWidth, B), Scale);
            result[1] = Lon + ScaleY(PivotY(TrueAngle, MsgWidth, B), Scale);
            return result;
        }

        public static double[] Adjust (double LeftRight, double UpDown, 
            double Lat, double Lon, double Angle, double Scale)
        {
            // X and Y are Hershey units (one char is 30 x 30)
            // RETURNS the new Lat Long from the request
            double[] result = new double[2];
            double Width = LeftRight;
            double Offset = UpDown;
            double TrueAngle = Angle + InfoSection.MagneticVariation;
            result[0] = Lat + ScaleX(PivotX(TrueAngle, Width, Offset), Scale);
            result[1] = Lon + ScaleY(PivotY(TrueAngle, Width, Offset), Scale);
            return result;
        }

        private static double Height(double Scale)
        {
            return 30 * InfoSection.NMperDegreeLatitude * Scale;
        }

        private static double Width(string Message)
        {
            // Returns the width in Hershey units for a given string
            int AsciiC; int[] SmplxC; double SWidth = 0;
            foreach (char c in Message)
            {
                AsciiC = c;
                if ((AsciiC - 32 >= 0) & (AsciiC - 32 < 95))
                    AsciiC -= 32;
                else AsciiC = 10;     // 10 is '*' for 'error'
                SmplxC = RomanSimplex.Simplex[AsciiC];
                SWidth += SmplxC[1];
            }
            return SWidth;
        }

        public static string DrawSymbol(string FixType, double Lat, double Lon, double Angle, double Scale)
        {
            double X; double Y; double VectorLat; double VectorLong;
            string Lat0 = string.Empty; string Lon0 = string.Empty;string Lat1; string Lon1;
            string cr = Environment.NewLine; string space = new string(' ', 27);
            double[] Centered = new double[2];
            int[] Symbol;
            switch (FixType)
            {
                case "FIX":
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
                    Symbol = MapSymbols.NDBDME;
                    break;
                case "NDB":
                case "MARINE NDB":
                case "UHF/NDB":
                    Symbol = MapSymbols.NDB;
                    break;
            }
            // The standard square is 30x30, but don't adjust just yet
            Centered = Adjust(Symbol[2]/-2, -15, Lat, Lon, Angle, Scale);
            string Result = space + "; " + FixType + " symbol " + cr;
            double TrueAngle = Angle + InfoSection.MagneticVariation;
            for (int i = 2; i < Symbol[0]*2; i += 2)
            {
                Y = Symbol[i + 1]; X = Symbol[i];                     // Used to adjust which is X and the other Y Symbols are X,Y
                if ((X == -1) & (Y == -1))                            // Next point is a break
                    Lat1 = Lon1 = string.Empty;
                else                                           
                {
                    VectorLat = ScaleX(PivotX(TrueAngle, X, Y), Scale);            // These adjust the vectors for the desire text angle
                    VectorLong = ScaleY(PivotY(TrueAngle, X, Y), Scale);
                    Lat1 = Conversions.DecDeg2SCT(Centered[0] + VectorLat, true);
                    Lon1 = Conversions.DecDeg2SCT(Centered[1] + VectorLong, false);
                }
                if ((Lat0.Length != 0) && (Lat1.Length != 0) && (Lat0 != Lat1))
                {
                    Result += SCTstrings.SSDout(Lat0, Lon0, Lat1, Lon1) + cr;
                }
                Lat0 = Lat1; Lon0 = Lon1;                            // No matter what happened, move End to Start
            }
            // Console.WriteLine(Result);
            return Result;                                      // Lat Long string to draw ONE character!  (Sheesh)
        }

        public static string WriteHF(string Message, double Lat, double Long, double Angle, double Scale)
        {
            // NOTE!! Angle is determined by calling routine!  Did it include Mag Var?
            // *** Need to subscript the Message down 15 and right 15.
            double curLat = Lat;
            double curLong = Long;
            string space = new string(' ', 27);
            string cr = Environment.NewLine;
            int AsciiC; int[] SmplxC; string Result = string.Empty;
            foreach (char c in Message)
            {
                AsciiC = c;
                if ((AsciiC - 32 >= 0) & (AsciiC - 32 < 95))
                    AsciiC -= 32;
                else AsciiC = 10;     // 10 is '*' for 'error'
                SmplxC = RomanSimplex.Simplex[AsciiC];
                Result += DrawChar(SmplxC, curLat, curLong, Angle, Scale, c);
                curLong += ScaleX(PivotX(Angle, SmplxC[1]), Scale);
                curLat += ScaleY(PivotY(Angle, SmplxC[1]), Scale);
            }
            //Console.WriteLine(space + "; " + Message + Environment.NewLine + Result);
            return space + "; " + Message + cr + Result;
        }

        private static string DrawChar(int[] hFont, double Lat, double Long, double Angle, double Scale, char c = ' ')
        {
            // Each vector needs to be (a) rotate to the angle of the line of text and (b) Scaled
            // One unit vector = 1 second or 90-100 feet.  Use the Scale function to adjust.
            string Result = string.Empty; double X; double Y; double VectorLat; double VectorLong;
            string DrawStart = string.Empty; string DrawEnd; bool FirstLine = true;
            string cr = Environment.NewLine; string space = new string(' ', 27);
            // Console.WriteLine("Passed " + ((hFont.Length - 2) / 2) + " vectors");
            for (int i = 2; i < hFont.Length; i += 2)
            {
                X = hFont[i + 1]; Y = hFont[i];                     // Used to adjust which is X and the other Y
                // Console.WriteLine("Processing line " + i / 2 + " (" + X + ", " + Y + ")");
                if (DrawStart.Length == 0)                          // Start or break in character lines - get next vector
                {
                    if ((X == -1) & (Y == -1))                      // Next point is a break (highly unlikely)
                        DrawEnd = string.Empty;
                    else                                            // Get next vector (which will get moved to Start)
                    {
                        VectorLat = ScaleX(PivotX(Angle, X, Y), Scale);            // These adjust the vectors for the desire text angle
                        VectorLong = ScaleY(PivotY(Angle, X, Y), Scale);
                        // Console.WriteLine("X (" + (X/3600f) + ") now " + VectorLat + " and Y (" + (Y/3600f) + ") now " + VectorLong);
                        DrawEnd = Conversions.DecDeg2SCT(Lat + VectorLat, true) + " "
                            + Conversions.DecDeg2SCT(Long +  VectorLong, false);
                    }
                }
                else                                                // Start point has coordinates, see if end point does
                {
                    if ((X == -1) & (Y == -1))
                        DrawEnd = string.Empty;
                    else                                            // End point also valid - write this char line
                    {
                        VectorLat = ScaleX(PivotX(Angle, X, Y), Scale);            // These adjust the vectors for the desire text angle
                        VectorLong = ScaleY(PivotY(Angle, X, Y), Scale);
                        DrawEnd = Conversions.DecDeg2SCT(Lat + VectorLat, true) + " "
                            + Conversions.DecDeg2SCT(Long + VectorLong, false);
                        if (DrawStart != DrawEnd)
                        {
                            Result += space + DrawStart + " " + DrawEnd;
                            if (FirstLine)
                            {
                                Result += " ; " + c + cr;
                                FirstLine = false;
                            }
                            else Result += cr;
                        }
                    }
                }
                DrawStart = DrawEnd;                            // No matter what happened, move End to Start
            }
            // Console.WriteLine(Result);
            return Result;                                      // Lat Long string to draw ONE character!  (Sheesh)
        }

        private static double PivotX(double Angle, double X, double B = 0)
        {
            // mx + b... But in our case B offset is usually 0 
            // Might use B to create super/subscripts.
            // NOTE!!! Angle is determined by calling routine - did it correct for MagVar?
            double RadAngle = LatLongCalc.Deg2Rad(Angle);
            return (X * Convert.ToDouble(Math.Cos(RadAngle))) - (B * Convert.ToDouble(Math.Sin(RadAngle)));
        }

        private static double PivotY(double Angle, double X, double B = 0)
        {
            double RadAngle = LatLongCalc.Deg2Rad(Angle);
            return (B * Convert.ToDouble(Math.Cos(RadAngle))) + (X * Convert.ToDouble(Math.Sin(RadAngle)));
        }
        private static double ScaleX(double DistNM, double Scale)
        {
            return DistNM / InfoSection.NMperDegreeLongitude * Scale;
        }

        private static double ScaleY(double DistNM, double Scale)
        {
            return DistNM / InfoSection.NMperDegreeLatitude * Scale;
        }
    }
}
