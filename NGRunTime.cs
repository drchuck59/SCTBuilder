using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SCTBuilder
{
    class NGRunTime
    {
        // NaviGraph based routines are found here.
        // These are NOT the Oceanic routes, which are static
        // These are FAA offshore routes and include:
        // Y, AR (Atlantic) and Q routes (Gulf of Mexico)
        // The west coast does not have equivalent routes

        public static void AtlanticRoutes()
        {
            DataView dvRTE = new DataView (NaviGraph.wpNavRTE);


        }
    }
}
