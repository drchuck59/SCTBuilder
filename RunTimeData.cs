using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCTBuilder
{
    public class RomanSimplex
    {
        // Initialize jagged array of Simplex font vectors
        // At Scale = 1, 1 unit = 30 seconds DMS
        // First value is # of vectors, second is width
        public static int[][] Simplex = new int[][]
        {
            // 0 Ascii 32 (<space>)
            new int[] { 0, 16 },
            // Ascii 33 (!)
            new int[] {8, 10,
                    5,21, 5,7, -1,-1, 5,2, 4,1, 5,0, 6,1, 5,2},
            // Ascii 34 (")
            new int[] {5, 16,
                    4,21, 4,14, -1,-1, 12,21, 12,14},
            // Ascii 35 (#)
            new int[] {11, 21,
                    11,25, 4,-7, -1,-1, 17,25, 10,-7, -1,-1, 4,12, 18,12, -1,-1, 3,6, 17,6},
            // Ascii 36 ($)
            new int[] {26, 20,
                    8,25, 8,-4, -1,-1, 12,25, 12,-4, -1,-1, 17,18, 15,20, 12,21, 8,21,
                    5,20, 3,18, 3,16, 4,14, 5,13, 7,12, 13,10, 15,9, 16,8, 17,6,
                    17,3, 15,1, 12,0, 8,0, 5,1, 3,3},
            // 5 Ascii 37 (%)
            new int[] {31, 24,
                    21,21, 3,0, -1,-1, 8,21, 10,19, 10,17, 9,15, 7,14, 5,14, 3,16,
                    3,18, 4,20, 6,21, 8,21, 10,20, 13,19, 16,19, 19,20, 21,21, -1,-1,
                    17,7, 15,6, 14,4, 14,2, 16,0, 18,0, 20,1, 21,3, 21,5, 19,7, 17,7},
            // Ascii 38 (&)
            new int[] {34, 26,
                    23,12, 23,13, 22,14, 21,14, 20,13, 19,11, 17,6, 15,3, 13,1, 11,0,
                    7,0, 5,1, 4,2, 3,4, 3,6, 4,8, 5,9, 12,13, 13,14, 14,16, 14,18,
                    13,20, 11,21, 9,20, 8,18, 8,16, 9,13, 11,10, 16,3, 18,1, 20,0,
                    22,0, 23,1, 23,2},
            // Ascii 39 (')
            new int[] {7, 10,
                    5,19, 4,20, 5,21, 6,20, 6,18, 5,16, 4,15 },
            // Ascii 40 ( ( )
            new int[] {10,14,
                    11,25, 9,23, 7,20, 5,16, 4,11, 4,7, 5,2, 7,-2, 9,-5, 11,-7},
            // Ascii 41 ( ) )
            new int[] { 10, 14,
                    3,25, 5,23, 7,20, 9,16,  10,11, 10,7, 9,2, 7,-2, 5,-5, 3,-7},
            // 10 Ascii 42 (*)
            new int[] { 8, 16,
                     8,21, 8,9, -1,-1, 3,18, 13,12, -1,-1, 13,18, 3,12 },
            // Ascii 43 (+)
            new int[] { 5, 26,
                    13,18, 13,0, -1,-1, 4,9, 22,9},
            // Ascii 44 (,)
            new int[] { 8,10,
                    6,1, 5,0, 4,1, 5,2, 6,1, 6,-1, 5,-3, 4,-4},
            // Ascii 45 (-)
            new int[] { 2, 26,
                    4, 9, 22,9},
            //Ascii 46 (.)
            new int[] { 5, 10,
                    5,2, 4,1, 5,0, 6,1, 5,2},
            // 15 Ascii 47 (/)
            new int[] { 2, 22, 20,25, 2,-7},
            // Ascii 48 (0)
            new int[] { 17, 20,
                    9,21, 6,20, 4,17, 3,12, 3,9, 4,4, 6,1, 9,0, 11,0, 14,1,
                    16,4, 17,9, 17,12, 16,17, 14,20, 11,21, 9,21},
            // Ascii 49 (1)
            new int[] { 4,20,
                    6,17, 8,18, 11,21, 11,0},
            // Ascii 50 (2)
            new int[] { 14, 20,
                    4,16, 4,17, 5,19, 6,20, 8,21, 12,21, 14,20, 15,19, 16,17, 16,15, 15,13,
                    13,10, 3,0, 17,0},
            // Ascii 51 (3)
            new int[] {15, 20,
                    5,21, 16,21, 10,13, 13,13, 15,12, 16,11, 17,8, 17,6, 16, 3, 14,1, 11,0,
                    8,0, 5,1, 4,2, 3,4},
            // 20 Ascii 52 (4)
            new int[] {6, 20,
                    13,21, 3,7, 18,7, -1,-1, 13,21, 13,0 },
            // Ascii 53 (5)
            new int[] {17, 20,
                    15,21, 5,21, 4,12, 5,13, 8,14, 11,14, 14,13, 16,11, 17,8, 17,6,
                    16,3, 14,1, 11,0, 8,0, 5,1, 4,2, 3,4 },
            // Ascii 54 (6)
            new int[] {23, 20,
                    16,18, 15,20, 12,21, 10,21, 7,20, 5,17, 4,12, 4,7, 5,3, 7,1,
                    10,0, 11,0, 14,1, 16,3, 17,6, 17,7, 16,10, 14,12, 11,13, 10,13,
                    7,12, 5,10, 4,7},
            // Ascii 55 (7)
            new int[] {5, 20,
                17,21, 7,0, -1,-1, 3,21, 17,21},
            // Ascii 56 (8)
            new int[] {29, 20,
                    8,21, 5,20, 4,18, 4,16, 5,14, 7,13, 11,12, 14,11, 16,9, 17,7,
                    17,4, 16,2, 15,1, 12,0, 8,0, 5,1, 4,2, 3,4, 3,7, 4,9,
                    6,11, 9,12, 13,13, 15,14, 16,16, 16,18, 15,20, 12,21, 8,21 },
            // 25 Ascii 57 (9)
            new int[] {23, 20,
                    16,14, 15,11, 13,9, 10,8, 9,8, 6,9, 4,11, 3,14, 3,15, 4,18,
                    6,20, 9,21, 10,21, 13,20, 15,18, 16,14, 16,9, 15,4, 13,1, 10,0,
                    8,0, 5,1, 4,3 },
            // Ascii 58 (:)
            new int[] {11, 10,
                    5,14, 4,13, 5,12, 6,13, 5,14, -1,-1, 5,2, 4,1, 5,0, 6,1, 5,2 },
            // Ascii 59 (;)
            new int[] {14, 10,
                    5,14, 4,13, 5,12, 6,13, 5,14, -1,-1, 6,1, 5,0, 4,1, 5,2, 6,1,
                    6,-1, 5,-3, 4,-4 },
            // Ascii 60 (<)
            new int[] {3, 24, 20,18, 4, 9, 20, 0 },
            // Ascii 61 (=)
            new int[] {5, 26, 4,12, 22,12, -1,-1, 4,6, 22,6 },
            // 30 Ascii 62 (>)
            new int[] {3, 24, 4,18, 20,9, 4,0 },
            // Ascii 63 (?)
            new int[] {20, 18,
                    3,16, 3,17, 4,19, 5,20, 7,21, 11,21, 13,20, 14,19, 15,17, 15,15,
                    14,13, 13,12, 9,10, 9, 7, -1,-1, 9,2, 8,1, 9,0, 10,1, 9,2 },
            // Ascii 64 (@)
            new int[] {55,27,
                   18,13,17,15,15,16,12,16,10,15, 9,14, 8,11, 8, 8, 9, 6,11, 5,14, 5,16,
                    6,17, 8,-1,-1,12,16,10,14, 9,11, 9, 8,10, 6,11, 5,-1,-1,18,16,17, 8,
                   17, 6,19, 5,21, 5,23, 7,24,10,24,12,23,15,22,17,20,19,18,20,15,21,12,
                   21, 9,20, 7,19, 5,17, 4,15, 3,12, 3, 9, 4, 6, 5, 4, 7, 2, 9, 1,12, 0,
                   15, 0,18, 1,20, 2,21, 3,-1,-1,19,16,18, 8,18, 6,19, 5 },
            // Ascii 65 (A)
            new int[] {8,18,
                    9,21, 1, 0,-1,-1, 9,21,17, 0,-1,-1, 4, 7,14, 7},
            // Ascii 66 (B)
            new int[] {23,21,
                    4,21, 4, 0,-1,-1, 4,21,13,21,16,20,17,19,18,17,18,15,17,13,16,12,13,
                   11,-1,-1, 4,11,13,11,16,10,17, 9,18, 7,18, 4,17, 2,16, 1,13, 0, 4, 0 },
            // 35 Ascii 67 (C)
            new int[] {18,21,
                   18,16, 17,18, 15,20, 13,21, 9,21, 7,20, 5,18, 4,16, 3,13, 3,8, 4,5, 
                    5,3, 7,1, 9,0, 13,0, 15,1, 17,3, 18,5 },
            // Ascii 68 (D)
            new int[] {15,21,
                    4,21, 4, 0,-1,-1, 4,21,11,21,14,20,16,18,17,16,18,13,18, 8,17, 5,16,
                    3,14, 1,11, 0, 4, 0 },
            // Ascii 69 (E)
            new int[] {11,19,
                    4,21, 4, 0,-1,-1, 4,21,17,21,-1,-1, 4,11,12,11,-1,-1, 4, 0,17, 0 },
            // Ascii 70 (F)
            new int[] {8,18,
                    4,21, 4, 0,-1,-1, 4,21,17,21,-1,-1, 4,11,12,11 },
            // Ascii 71 (G)
            new int[] {22,21,
                   18,16,17,18,15,20,13,21, 9,21, 7,20, 5,18, 4,16, 3,13, 3, 8, 4, 5, 5,
                    3, 7, 1, 9, 0,13, 0,15, 1,17, 3,18, 5,18, 8,-1,-1,13, 8,18, 8 },
            // 40 Ascii 72 (H)
            new int[] {    8,22,
                    4,21, 4, 0,-1,-1,18,21,18, 0,-1,-1, 4,11,18,11 },
            // Ascii 73 (I)
            new int[] {2, 8, 4,21, 4, 0 },
            // Ascii 74 (J)
            new int[] {10,16,
                12,21,12, 5,11, 2,10, 1, 8, 0, 6, 0, 4, 1, 3, 2, 2, 5, 2, 7 },
            // Ascii 75 (K)
            new int[] {8,21, 4,21, 4, 0,-1,-1,18,21, 4, 7,-1,-1, 9,12,18, 0 },
            // Ascii 76 (L)
            new int[] {5,17, 4,21, 4, 0,-1,-1, 4, 0,16, 0},
            // 45 Ascii 77 (M)
            new int[] {11,24,
                    4,21, 4, 0,-1,-1, 4,21,12, 0,-1,-1,20,21,12, 0,-1,-1,20,21,20, 0 },
            // Ascii 78 (N)
            new int[] { 8,22,
                4,21, 4, 0,-1,-1, 4,21,18, 0,-1,-1,18,21,18, 0},
            // Ascii 79 (O)
            new int[] {21,22,
                    9,21, 7,20, 5,18, 4,16, 3,13, 3, 8, 4, 5, 5, 3, 7, 1, 9, 0,13, 0,15,
                    1,17, 3,18, 5,19, 8,19,13,18,16,17,18,15,20,13,21, 9,21 },
            // Ascii 80 (P)
            new int[] {13,21,
                    4,21, 4, 0,-1,-1, 4,21,13,21,16,20,17,19,18,17,18,14,17,12,16,11,13,
                   10, 4,10 },
            // Ascii 81 (Q)
            new int[] {24,22,
                    9,21, 7,20, 5,18, 4,16, 3,13, 3, 8, 4, 5, 5, 3, 7, 1, 9, 0,13, 0,15,
                    1,17, 3,18, 5,19, 8,19,13,18,16,17,18,15,20,13,21, 9,21,-1,-1,12, 4,
                   18,-2 },
            // 50 Ascii 82 (R)
            new int[] {16,21,
                    4,21, 4, 0,-1,-1, 4,21,13,21,16,20,17,19,18,17,18,15,17,13,16,12,13,
                   11, 4,11,-1,-1,11,11,18, 0 },
            // Ascii 83 (S)
            new int[] {20, 20,
                   17,18,15,20,12,21, 8,21, 5,20, 3,18, 3,16, 4,14, 5,13, 7,12,13,10,15,
                    9,16, 8,17, 6,17, 3,15, 1,12, 0, 8, 0, 5, 1, 3, 3},
            // Ascii 84 (T)
            new int[] {5,16, 8,21, 8, 0,-1,-1, 1,21,15,21 },
            // Ascii 85 (U)
            new int[] {10,22,
                    4,21, 4, 6, 5, 3, 7, 1,10, 0,12, 0,15, 1,17, 3,18, 6,18,21 },
            // Ascii 86 (V)
            new int[] {5,18, 1,21, 9, 0,-1,-1,17,21, 9, 0 },
            // 55 Ascii 87 (W)
            new int[] {11,24,
                    2,21, 7, 0,-1,-1,12,21, 7, 0,-1,-1,12,21,17, 0,-1,-1,22,21,17, 0 },
            // Ascii 88 (X)
            new int[] {5,20, 3,21,17, 0,-1,-1,17,21, 3, 0 },
            // Ascii 89 (Y)
            new int[] {6,18, 1,21, 9,11, 9, 0,-1,-1,17,21, 9,11 },
            // Ascii 90 (Z)
            new int[] {8,20, 17,21, 3, 0,-1,-1, 3,21,17,21,-1,-1, 3, 0,17, 0 },
            // Ascii 91 ([)
            new int[] {11,14, 4,25, 4,-7,-1,-1, 5,25, 5,-7,-1,-1, 4,25,11,25,-1,-1, 4,-7,11,-7 },
            // 60 Ascii 92 (\)
            new int[] {2,14, 0,21,14,-3 },
            // Ascii 93 (])
            new int[] {11,14, 9,25, 9,-7,-1,-1,10,25,10,-7,-1,-1, 3,25,10,25,-1,-1, 3,-7,10,-7 },
            // Ascii 94 (^)
            new int[] {10,16, 6,15, 8,18,10,15,-1,-1, 3,12, 8,17,13,12,-1,-1, 8,17, 8, 0 },
            // Ascii 95 (_)
            new int[] {2,16, 0,-2, 16,-2 },
            // Ascii 96 (`)
            new int[] {7,10, 6,21, 5,20, 4,18, 4,16, 5,15, 6,16, 5,17 },
            // 65 Ascii 97 (a)
            new int[] {17,19,
                    15,14,15, 0,-1,-1,15,11,13,13,11,14, 8,14, 6,13, 4,11, 3, 8, 3, 6, 4,
                    3, 6, 1, 8, 0,11, 0,13, 1,15, 3 },
            // Ascii 98 (b)
            new int[] {17,19,
                    4,21, 4, 0,-1,-1, 4,11, 6,13, 8,14,11,14,13,13,15,11,16, 8,16, 6,15,
                    3,13, 1,11, 0, 8, 0, 6, 1, 4, 3 },
            // Ascii 99 (c)
            new int[] {14,18,
                   15,11, 13,13, 11,14, 8,14, 6,13, 4,11, 3,8, 3,6, 4,3, 6,1, 8,0,
                11,0, 13,1, 15,3 },
            // Ascii 100 (d)
            new int[] {17,19,
                   15,21,15, 0,-1,-1,15,11,13,13,11,14, 8,14, 6,13, 4,11, 3, 8, 3, 6, 4,
                    3, 6, 1, 8, 0,11, 0,13, 1,15, 3 },
            // Ascii 101 (e)
            new int[] {17,18,
                    3, 8,15, 8,15,10,14,12,13,13,11,14, 8,14, 6,13, 4,11, 3, 8, 3, 6, 4,
                    3, 6, 1, 8, 0,11, 0,13, 1,15, 3 },
            // 70 Ascii 102 (f)
            new int[] {8,12, 10,21, 8,21, 6,20, 5,17, 5, 0,-1,-1, 2,14, 9,14 },
            // Ascii 103 (g)
            new int[] {22,19,
                   15,14,15,-2,14,-5,13,-6,11,-7, 8,-7, 6,-6,-1,-1,15,11,13,13,11,14, 8,
                   14, 6,13, 4,11, 3, 8, 3, 6, 4, 3, 6, 1, 8, 0,11, 0,13, 1,15, 3 },
            // Ascii 104 (h)
            new int[] {10,19,
                    4,21, 4, 0,-1,-1, 4,10, 7,13, 9,14,12,14,14,13,15,10,15, 0 },
            // Ascii 105 (i)
            new int[] {8, 8,
                    3,21, 4,20, 5,21, 4,22, 3,21,-1,-1, 4,14, 4, 0 },
            // Ascii 106 (j)
            new int[] {11,10,
                    5,21, 6,20, 7,21, 6, 22, 5,21,-1,-1, 6,14, 6,-3, 5,-6, 3,-7, 1,-7 },
            // 75 Ascii 107 (k)
            new int[] {8,17, 4,21, 4, 0,-1,-1,14,14, 4, 4,-1,-1, 8, 8,15, 0 },
            // Ascii 108 (l)
            new int[] {2, 8, 4,21, 4, 0 },
            // Ascii 109 (m)
            new int[] {18,30,
                    4,14, 4, 0,-1,-1, 4,10, 7,13, 9,14,12,14,14,13,15,10,15, 0,-1,-1,15,
                   10,18,13,20,14,23,14,25,13,26,10,26, 0 },
            // Ascii 110 (n)
            new int[] {10,19,
                    4,14, 4, 0,-1,-1, 4,10, 7,13, 9,14,12,14,14,13,15,10,15, 0 },
            // Ascii 111 (o)
            new int[] {17,19,
                    8,14, 6,13, 4,11, 3, 8, 3, 6, 4, 3, 6, 1, 8, 0,11, 0,13, 1,15, 3,16,
                    6,16, 8,15,11,13,13,11,14, 8,14 },
            // 80 Ascii 112 (p)
            new int[] {17,19,
                    4,14, 4,-7,-1,-1, 4,11, 6,13, 8,14,11,14,13,13,15,11,16, 8,16, 6,15,
                    3,13, 1,11, 0, 8, 0, 6, 1, 4, 3 },
            // Ascii 113 (q)
            new int[] {17,19,
                   15,14,15,-7,-1,-1,15,11,13,13,11,14, 8,14, 6,13, 4,11, 3, 8, 3, 6, 4,
                    3, 6, 1, 8, 0,11, 0,13, 1,15, 3 },
            // Ascii 114 (r)
            new int[] {8,13, 4,14, 4, 0,-1,-1, 4, 8, 5,11, 7,13, 9,14,12,14 },
            // Ascii 115 (s)
            new int[] {17,17,
                   14,11,13,13,10,14, 7,14, 4,13, 3,11, 4, 9, 6, 8,11, 7,13, 6,14, 4,14,
                    3,13, 1,10, 0, 7, 0, 4, 1, 3, 3 },
            // Ascii 116 (t)
            new int[] {8,12, 5,21, 5, 4, 6, 1, 8, 0,10, 0,-1,-1, 2,14, 9,14 },
            // 85 Ascii 117 (u)
            new int[] {10,19,
                    4,14, 4, 4, 5, 1, 7, 0,10, 0,12, 1,15, 4,-1,-1,15,14,15, 0 },
            // Ascii 118 (v)
            new int[] {5,16, 2,14, 8, 0,-1,-1,14,14, 8, 0 },
            // Ascii 119 (w)
            new int[] {11,22,
                    3,14, 7,0, -1,-1, 11,14, 7,0, -1,-1, 11,14, 15,0, -1,-1, 19,14, 15,0 },
            // Ascii 120 (x)
            new int[] {5,17, 3,14,14, 0,-1,-1,14,14, 3, 0 },
            // Ascii 121 (y)
            new int[] {9,16, 2,14, 8,0, -1,-1, 14,14, 8,0, 6,-4, 4,-6, 2,-7, 1,-7 },
            // 90 Ascii 122 (z)
            new int[] {8,17, 14,14, 3, 0,-1,-1, 3,14,14,14,-1,-1, 3, 0,14, 0 },
            // Ascii 123 ({)
            new int[] {39,14,
                    9,25, 7,24, 6,23, 5,21, 5,19, 6,17, 7,16, 8,14, 8,12, 6,10,-1,-1, 7,
                   24, 6,22, 6,20, 7,18, 8,17, 9,15, 9,13, 8,11, 4, 9, 8, 7, 9, 5, 9, 3,
                    8, 1, 7, 0, 6,-2, 6,-4, 7,-6,-1,-1, 6, 8, 8, 6, 8, 4, 7, 2, 6, 1, 5,
                   -1, 5,-3, 6,-5, 7,-6, 9,-7 },
            // Ascii 124 (|)
            new int[] {2, 8, 4,25, 4,-7 },
            // Ascii 125 (})
            new int[] {39,14,
                    5,25, 7,24, 8,23, 9,21, 9,19, 8,17, 7,16, 6,14, 6,12, 8,10,-1,-1, 7,
                   24, 8,22, 8,20, 7,18, 6,17, 5,15, 5,13, 6,11,10, 9, 6, 7, 5, 5, 5, 3,
                    6, 1, 7, 0, 8,-2, 8,-4, 7,-6,-1,-1, 8, 8, 6, 6, 6, 4, 7, 2, 8, 1, 9,
                   -1, 9,-3, 8,-5, 7,-6, 5,-7 },
            // 94 Ascii 126 (~)
            new int[] { 23,24,
                    3,6, 3,8, 4,11, 6,12, 8,12, 10,11, 14,8, 16,7, 18,7, 20,8, 21,10,
                    -1,-1, 3,8, 4,10, 6,11, 8,11, 10,10, 14,7, 16,6, 18,6, 20,7, 21,10, 21,12},
        };
    }

    public class MapSymbols
    {
        // At Scale = 1, 1 unit = 30 seconds DMS, or 30/3600 degrees
        // First value is # of vectors, second is width, third is angle offset, next pair is center
        // Triangle - RNAV Fix
        public static int[] FIX = new int[]
        { 4, 22, 1,1, 15,30, 30,1, 1,1 };
        // VOR symbol - hexagon
        public static int[] VOR = new int[]
            {7, 30, 1,9, 1,15, 9,30, 21,30, 30,15, 21,1, 1,9};
        // VORTAC - hexagon with bold angled lines
        public static int[] VORTAC = new int[]
            {16, 30, 9,4, 2,15, 9,27, 21,27, 28,15, 24,4, 9,4,
            -1,-1, 1,17, 7,27, -1,-1, 23,27, 29,17, -1,-1, 9,1, 23,1};
        // TACAN - outline of the VORTAC
        public static int[] TACAN = new int[]
            {19, 25, 3,11, 11,11, 8,17, 1,24, 7,30, 12,25, 19,25,
            24,30, 30,24, 17,23, 20,11, 20,3, 11,3, -1,-1, 
                15,16, 15,18, 17,18, 17,16, 15,17};
        // VOR-DME - hexagon inside a square
        public static int[] VORDME = new int[]
            {13, 30, 1,9, 1,15, 9,30, 21,30, 30,15, 21,1, 1,9,
            -1,-1, 1,1, 1,30, 30,30, 1,30, 1,1};
        // DME - square
        public static int[] DME = new int[]
            {5, 21, 5,1, 1,30, 30,30, 1,30, 1,1};
        // Waypoint - weird star
        public static int[] WAYPOINT = new int[]
            {28, 30,  
                1,15, 2,15, 6,16, 10,17, 13,20, 14,24, 15,29, 15,30, 
                15,29, 16,24, 17,20, 21,17, 25,16, 29,15, 30,15, 
                29,15, 25,14, 21,13, 16,6, 15,2, 15,1,
                15,2, 14,6, 13,10, 10,13, 6,14, 2,15, 1,15
            };
        // NDB - all
        public static int[] NDB = new int[]
        {   65, 28, 
            14,16, 15,16, 16,14, 14,16, 14,14, -1,-1, 
            14,12, 12,14, 12,16, 14,18, 16,18, 18,16, 18,14, 16,12, 14,12, -1,-1,
            13,10, 10,13, 10,17, 13,20, 17,20, 20,17, 20,13, 17,10, 13,10, -1,-1, 
            12,8, 8,12, 8,18, 12,22, 18,22, 22,18, 22,12, 18,8, 12,8, -1,-1,
            11,6, 6,11, 6,19, 11,24, 19,24, 24,19, 24,11, 19,6, 11,6, -1,-1,
            10,4, 4,10, 4,20, 10,26, 20,26, 26,20, 26,10, 20,4, 10,4, -1,-1,
            9,2, 2,9, 2,21, 9,28, 21,28, 28,21, 28,9, 21,2, 9,2 
        };
        // NDB - DME
        public static int[] NDBDME = new int[]
        {   71, 29,
            14,16, 15,16, 16,14, 14,16, 14,14, -1,-1,
            14,12, 12,14, 12,16, 14,18, 16,18, 18,16, 18,14, 16,12, 14,12, -1,-1,
            13,10, 10,13, 10,17, 13,20, 17,20, 20,17, 20,13, 17,10, 13,10, -1,-1,
            12,8, 8,12, 8,18, 12,22, 18,22, 22,18, 22,12, 18,8, 12,8, -1,-1,
            11,6, 6,11, 6,19, 11,24, 19,24, 24,19, 24,11, 19,6, 11,6, -1,-1,
            10,4, 4,10, 4,20, 10,26, 20,26, 26,20, 26,10, 20,4, 10,4, -1,-1,
            9,2, 2,9, 2,21, 9,28, 21,28, 28,21, 28,9, 21,2, 9,2, -1,-1,
            1,1, 1,29, 29,29, 1,29, 1,1
        };
        // No map relationship
        public static int[] Octagon = new int[]
            {9, 30, 1,10, 1,20, 10,30, 20,30, 30,20, 30,10, 20,1, 10,1, 1,10 };
        public static int[] Star4Pt = new int[]
            { 9, 30, 1,15, 12,18, 15,30, 18,18, 30,15, 18,12, 15,1, 12,12, 1,15};
        public static int[] Star5Pt = new int[]
            { 11, 30, 1,18, 12,18, 15,30, 18,18, 30,18, 18,12, 23,1, 15,7, 7,1, 12,12, 1,18 };
        public static int[] Diamond = new int[]
            { 5, 30, 1,15, 15,30, 30,15, 15,1, 1,15 };
    }

 
    public class APTView
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Frequency { get; set; }
    }
    public class FolderMgt
    {
        private static string outputFolder = string.Empty;
        private static string dataFolder = string.Empty;
        public static string DataFolder
        {
            get { return dataFolder; }
            set { dataFolder = value; }
        }

        public static string OutputFolder
        {
            get { return outputFolder; }
            set { outputFolder = value; }
        }

        public static readonly string AppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "//SCTbuilder";

        public static readonly DirectoryInfo INIdirectory = Directory.CreateDirectory(AppDataFolder);

        public static readonly string INIFilePath = INIdirectory.FullName + "SCTBuilder.xml";

    }

    public class UserMessage
    {
        private static readonly string cr = Environment.NewLine;
        public static readonly string FirstTime =
            "It appears the program is running for the first time." + cr +
                "First create or select a data folder ('DATA')." + cr +
                "This folder will hold ALL the SCTBuilder data subfolders." + cr +
                "Then click the 'Update AIRAC' button to retrieve the current FAA AIRAC.";
        public static readonly string CorruptINI =
            "SCTBuilder closed with insufficient settings to resume.  " + cr +
                "First create or select a data folder ('DATA')." + cr +
                "This folder will hold ALL the SCTBuilder data subfolders." + cr +
                "Then click the 'Update AIRAC' button to retrieve the current FAA AIRAC.";
        public static readonly string AIRACdataMismatch =
            "The last AIRAC you used does not match the AIRAC in the data folder.  " + cr +
                "The program will update to the data in the data folder.";
        public static readonly string NGdataMismatch =
            "The NaviGraph AIRAC " + InfoSection.NG_AIRAC + " does not match the FAA AIRAC " + CycleInfo.AIRAC + "." + cr +
                "Use of NaviGraph data has been disabled.";
        public static readonly string NoValidAirports =
            "Cannot select Runways.  No airports with runways found meeting search parameters.";
        public static readonly string MissingSelectionCriteria =
            "You must have a square identifed, an output folder, and some selection items before you can Preview.";
        public static readonly string FENameRequired =
            "Facility Engineer name may not be blank";
        public static readonly string PathInvalid =
            "The directory does not exist.  Consider using the selection button (...).";
        public static readonly string DataFolderRequired =
            "You must select your data folder before you can perform searches.";
        public static readonly string OutputFolderRequired =
            "You must select your output folder before you can write results.";
        public static readonly string LatOutOfBounds =
            "Latitude must be a value between -90 to +90 degrees";
        public static readonly string LonOutOfBounds =
            "Longitude must be a value between -180 to +180 degrees";
        public static readonly string NorthUnderSouth =
            "The North Limit must be north of the South Limit";
        public static readonly string WestRightOfEast =
            "The West Limit must be west of the East Limit";
        public static readonly string LimitsMissing =
            "One or more limits are missing from the square.";
        public static readonly string APTselectRequired =
            "In order to select Runways, airports must be selected.";
        public static readonly string NAVAIDrequired =
            "In order to select Airways, NavAids must be selected.";
        public static readonly string APTrequired =
            "You must first select an airport first.";
        public static readonly string ARTCCrequired =
            "You must first select an ARTCC to filter by ARTCC.";
        public static readonly string CoordsInvalid =
            "Invalid coordinates - please try again";
        public static readonly string FAADownloadError =
            "FAA download returned an error.  Correct the error or enter issue in GitHub.";
        public static readonly string PrefsSaved =
            "Preferences saved.";
    }

    public class VersionInfo                            // Internal information
    {
        public readonly static string Title = "SCT Builder v1.4";
    }
    public class FilterBy                               // Filter source for SCT2 output
    {
        private static double northlimit = 0;
        private static double southlimit = 0;
        private static double westlimit = 0;
        private static double eastlimit = 0;

        public static string Method { get; set; }
        public static double NorthLimit
        {
            get { return northlimit; }
            set { northlimit = value; }
        }
        public static double SouthLimit
        {
            get { return southlimit; }
            set { southlimit = value; }
        }
        public static double EastLimit
        {
            get { return eastlimit; }
            set { eastlimit = value; }
        }
        public static double WestLimit
        {
            get { return westlimit; }
            set { westlimit = value; }
        }

    }
    public class InfoSection
    {
        private static string fe = string.Empty;
        private static string afe = string.Empty;
        private static string artcc = string.Empty;
        private static string apt = string.Empty;
        private static double centerLat = 0;
        private static double centerLon = 0;
        private static bool useFixesAsCoords;
        private static bool drawFixLabelsOnDiagrams;
        private static bool drawFixSymbolsOnDiagrams;
        private static bool oneFilePerSidStar;
        private static bool drawAltRestrictsOnDiagrams;
        private static bool drawSpeedRestrictsOnDiagrams;
        private static bool includeSidStarReferences;
        private static bool useNaviGraphData;
        private static bool hasNaviGraphData;
        private static int nG_AIRAC;
        private static bool rolloverLongitude;
        private static double northlimit = 0;
        private static double southlimit = 0;
        private static double westlimit = 0;
        private static double eastlimit = 0;
        private static double northoffset = 0;
        private static double southoffset = 0;
        private static double westoffset = 0;
        private static double eastoffset = 0;

        public static string SectorName
        {
            get { return SponsorARTCC + "_" + CycleInfo.AIRAC.ToString(); }
        }
        public static string DefaultPosition    // Ignored by VRC
        {
            get
            {
                return SponsorARTCC + "_xx_OBS";
            }
        }
        public static string FacilityEngineer
        {
            get { return fe; }
            set { fe = value; }
        }
        public static string AsstFacilityEngineer
        {
            get { return afe; }
            set { afe = value; }
        }
        public static string SponsorARTCC
        {
            get { return artcc; }
            set { artcc = value; }
        }
        public static string DefaultAirport
        {
            get { return apt; }
            set { apt = value; }
        }
        public static string CenterLatitude_SCT               // Latitude of default sector center point as SCT format
        {
            get
            { return Conversions.Degrees2SCT(centerLat, true); }
            set
            {
                if (value.IsNumeric()) centerLat = Convert.ToDouble(value);
                else centerLat = Conversions.DMS2Degrees(value);
            }
        }
        public static double CenterLatitude_Dec               // Latitude of default sector center point
        {
            get
            { return centerLat; }
            set
            { centerLat = value; }
        }
        public static string CenterLongitude_SCT  // Longitude of default sector center point
        {
            get
            { return Conversions.Degrees2SCT(centerLon, false); }
            set
            {
                if (value.IsNumeric()) centerLon = Convert.ToDouble(value);
                else centerLon = Conversions.DMS2Degrees(value);
            }
        }
        public static double CenterLongitude_Dec
        {
            get
            { return centerLon; }
            set
            { centerLon = value; }
        }
        public static double NMperDegreeLatitude { get { return 60f; } } // Always 60 NM

        public static double NMperDegreeLongitude { get { return LatLongCalc.NMperLongDegree(); } }

        public static double MagneticVariation       // Varies by location
        {
            // East declination is positive; west is negative
            // 'Heading' == True or Map, 'Bearing' == Magnetic
            // True (map) Heading = Mag Bearing + Declination
            // Magnetic Bearing = True Heading - Declination
            // E deviations are positive, W deviations are negative
            get
            {
                if (DefaultAirport.Length != 0)
                    return SCTcommon.GetMagVar(DefaultAirport);
                else return 0;
            }
        }

        public static bool UseFixesAsCoords
        {
            get { return useFixesAsCoords; }
            set { useFixesAsCoords = value; } 
        }
        
        public static bool DrawFixLabelsOnDiagrams
        {
            get { return drawFixLabelsOnDiagrams; }
            set { drawFixLabelsOnDiagrams = value; }
        }

        public static bool DrawFixSymbolsOnDiagrams
        {
            get { return drawFixSymbolsOnDiagrams; }
            set { drawFixSymbolsOnDiagrams = value; }
        }

        public static bool DrawAltRestrictsOnDiagrams
        {
            get { return drawAltRestrictsOnDiagrams; }
            set { drawAltRestrictsOnDiagrams = value; }
        }

        public static bool DrawSpeedRestrictsOnDiagrams
        {
            get { return drawSpeedRestrictsOnDiagrams; }
            set { drawSpeedRestrictsOnDiagrams = value; }
        }

        public static bool UseNaviGraph
        {
            get { return useNaviGraphData; } 
            set { useNaviGraphData = value; }
        }
        public static bool HasNaviGraph
        {
            get { return hasNaviGraphData; }
            set { hasNaviGraphData = value; }
        }

        public static int NG_AIRAC
        {
            get { return nG_AIRAC; }
            set { nG_AIRAC = value; }
        }

        public static bool RollOverLongitude
        {
            get { return rolloverLongitude; } 
            set { rolloverLongitude = value; }
        }

        public static bool OneFilePerSidStar
        {
            get { return oneFilePerSidStar; }
            set { oneFilePerSidStar = value; }
        }

        public static bool IncludeSidStarReferences
        {
            get { return includeSidStarReferences; }
            set { includeSidStarReferences = value; }
        }

        public static double NorthLimit
        {
            get { return northlimit; }
            set { northlimit = value; }
        }
        public static double SouthLimit
        {
            get { return southlimit; }
            set { southlimit = value; }
        }
        public static double EastLimit
        {
            get { return eastlimit; }
            set { eastlimit = value; }
        }
        public static double WestLimit
        {
            get { return westlimit; }
            set { westlimit = value; }
        }
        public static double NorthOffset
        {
            get { return northoffset; }
            set { northoffset = value; }
        }
        public static double SouthOffset
        {
            get { return southoffset; }
            set { southoffset = value; }
        }
        public static double EastOffset
        {
            get { return eastoffset; }
            set { eastoffset = value; }
        }
        public static double WestOffset
        {
            get { return westoffset; }
            set { westoffset = value; }
        }
        public static double SectorScale { get { return 1f; } }      // Always 1, ignored in VRC
    }
    public static class SCTchecked
    {
        public static bool ChkARB { get; set; }
        public static bool ChkAPT { get; set; }
        public static bool LimitAPT2ARTC { get; set; }
        public static bool ChkVOR { get; set; }
        public static bool ChkNDB { get; set; }
        public static bool ChkFIX { get; set; }
        public static bool ChkAWY { get; set; }
        public static bool ChkOceanic { get; set; }
        public static bool ChkRWY { get; set; }
        public static bool ChkSID { get; set; }
        public static bool ChkSTAR { get; set; }
        public static bool ChkOneVRCFile { get; set; }
        public static bool ChkOneESFile { get; set; }
        public static bool ChkSSDname { get; set; }
        public static bool ChkSUA { get; set; }
        public static bool ChkSUA_ClassB { get; set; }
        public static bool ChkSUA_ClassC { get; set; }
        public static bool ChkSUA_ClassD { get; set; }
        public static bool ChkSUA_Danger { get; set; }
        public static bool ChkSUA_Prohibited { get; set; }
        public static bool ChkSUA_Restricted { get; set; }
        public static bool ChkConfirmOverwrite { get; set; }
        public static bool IncludeSUAfile { get; set;  }
        public static bool ChkES_SCTfile { get; set; }
        public static bool ChkES_SSDfile { get; set; }
    }

    public static class CrossForm
    {
        public static double Lat { get; set; }
        public static double Lon { get; set; }
        public static double Distance { get; set; }
        public static double Bearing { get; set; }

        public static bool TestTextBox(TextBox tb, int method = 0)
        {
            // Tests the string for correct coordinate format
            // Places Lat and/or Lon in public field above
            // Returns FALSE if unable to parse text  and sets public fields to -1
            bool ParsedResult = false;
            if (tb.Name.IndexOf("Lat") != -1) method = 1;
            if (tb.Name.IndexOf("Lon") != -1) method = 2;
            if (method == 0)
            {
                // Determine the format if not forced (aka, method 0)
                if ((tb.Text.ToUpperInvariant().IndexOf("N") != -1) || (tb.Text.ToUpperInvariant().IndexOf("S") != -1)) method = 1;
                if ((tb.Text.ToUpperInvariant().IndexOf("W") != -1) || (tb.Text.ToUpperInvariant().IndexOf("E") != -1)) method += 2;
            }
            if (tb.TextLength != 0)
            {
                if (LatLonParser.TryParseAny(tb, method))
                {
                    switch (method)
                    {
                        case 0:
                        case 3:
                            Lat = LatLonParser.ParsedLatitude;
                            Lon = LatLonParser.ParsedLongitude;
                            ParsedResult = true;
                            tb.Text = Conversions.Degrees2SCT(Lat, true) + " " +
                                Conversions.Degrees2SCT(Lon, false);
                            break;
                        case 1:
                            Lat = LatLonParser.ParsedLatitude;
                            Lon = -1;
                            tb.Text = Conversions.Degrees2SCT(Lat, true);
                            ParsedResult = true;
                            break;
                        case 2:
                            Lon = LatLonParser.ParsedLongitude;
                            Lat = -1;
                            tb.Text = Conversions.Degrees2SCT(Lon, false);
                            ParsedResult = true;
                            break;
                    }
                    tb.BackColor = Color.White;
                }
                else
                {
                    tb.BackColor = Color.Yellow;
                    Lat = Lon = -1;
                }
            }
            return ParsedResult;
        }

    }
} 
