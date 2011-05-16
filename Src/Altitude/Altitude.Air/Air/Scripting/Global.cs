using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Altitude.Air.Scripting
{
   public class Global
    {
        public static int current_storage_id { get; set; }

        public RunningMode CurrentRunningMode { get; set; }

        public int SlaveCount { get; set; }
    }
}
