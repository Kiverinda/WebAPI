using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public class ServiceHdd
    {
        public double GetFreeMemoryToAllHdd()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            double freeMemoryToHdd = 0;

            foreach (DriveInfo d in allDrives)
            {
                freeMemoryToHdd += d.TotalFreeSpace;
            }

            return freeMemoryToHdd;
        }
    }
}
