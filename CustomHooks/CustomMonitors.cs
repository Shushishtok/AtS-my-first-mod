using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstMod.CustomHooks
{
    public static class CustomMonitors
    {
        public static BuildingCompletedMonitor buildingCompletedMonitor = new BuildingCompletedMonitor();
    }
}
