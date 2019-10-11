using System;
using System.Collections.Generic;
using System.IO;


namespace SCTBuilder
{
    public class Hershey
    {
        public static string DrawHF(string Message, float Lat, float Long, float Angle, float Scale)
        {
            float curLat = Lat;
            float curLong = Long;
            string space = new string(' ', 27);
            int AsciiC; int[] SmplxC; string Result = string.Empty;
            foreach (char c in Message)
            {
                AsciiC = c;
                if ((AsciiC - 32 >= 0) & (AsciiC - 32 < 95))
                    AsciiC -= 32;
                else AsciiC = 10;     // 10 is '*' for 'error'
                SmplxC = HersheyFont.Simplex[AsciiC];
                Result += WriteChar(SmplxC, curLat, curLong, Angle, Scale);
                curLong += ScaleX(PivotX(SmplxC[1], 0f, Angle), Scale);
                curLat += ScaleY(PivotY(SmplxC[1], 0f, Angle), Scale);
            }
            //Console.WriteLine(space + "; " + Message + Environment.NewLine + Result);
            return space + "; " + Message + Environment.NewLine + Result;
        }

        private static string WriteChar(int[] hFont, float Lat, float Long, float Angle, float Scale)
        {
            // Each vector needs to be (a) rotate to the angle of the line of text and (b) Scaled
            // One unit vector = 1 second or 90-100 feet.  Use the Scale function to adjust.
            string Result = string.Empty; float X; float Y; float VectorLat; float VectorLong;
            string DrawStart = string.Empty; string DrawEnd;
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
                        VectorLat = ScaleX(PivotX(X, Y, Angle), Scale);            // These adjust the vectors for the desire text angle
                        VectorLong = ScaleY(PivotY(X, Y, Angle), Scale);
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
                        VectorLat = ScaleX(PivotX(X, Y, Angle), Scale);            // These adjust the vectors for the desire text angle
                        VectorLong = ScaleY(PivotY(X, Y, Angle), Scale);
                        DrawEnd = Conversions.DecDeg2SCT(Lat + VectorLat, true) + " "
                            + Conversions.DecDeg2SCT(Long + VectorLong, false);
                        if (DrawStart != DrawEnd)
                            Result += space + DrawStart + " " + DrawEnd + cr;
                    }
                }
                DrawStart = DrawEnd;                            // No matter what happened, move End to Start
            }
            // Console.WriteLine(Result);
            return Result;                                      // Lat Long string to draw ONE character!  (Sheesh)
        }

        private static float PivotX(float X, float Y, float Angle)
        {
            float RadAngle = Angle * (float)Math.PI / 180f;
            return (X * Convert.ToSingle(Math.Cos(RadAngle))) - (Y * Convert.ToSingle(Math.Sin(RadAngle)));
        }

        private static float PivotY(float X, float Y, float Angle)
        {
            float RadAngle = Angle * (float)Math.PI / 180f;
            return (Y * Convert.ToSingle(Math.Cos(RadAngle))) + (X * Convert.ToSingle(Math.Sin(RadAngle)));
        }
        private static float ScaleX(float DistNM, float Scale)
        {
            return DistNM / InfoSection.NMperDegreeLongitude * Scale;
        }

        private static float ScaleY(float DistNM, float Scale)
        {
            return DistNM / InfoSection.NMperDegreeLatitude * Scale;
        }
    }
}
