using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;

namespace ProjectRimFactory.Archo
{
    [StaticConstructorOnStartup]
    public class PlaceWorker_PortalGenerator : PlaceWorker
    {
        public override void DrawGhost(ThingDef def, IntVec3 center, Rot4 rot, Color ghostCol, Thing thing = null)
        {
            base.DrawGhost(def, center, rot, ghostCol, thing);
            PortalGeneratorUtility.DrawBlueprintFieldEdges(center);
        }
    }
}
