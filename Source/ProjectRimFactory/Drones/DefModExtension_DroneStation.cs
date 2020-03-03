using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Verse;

namespace ProjectRimFactory.Drones
{
    public class DefModExtension_DroneStation : DefModExtension
    {
        public int maxNumDrones;
        public bool displayDormantDrones;
        public List<WorkTypeDef> workTypes;

        public DefModExtension_DroneStation() {}
    }
}
