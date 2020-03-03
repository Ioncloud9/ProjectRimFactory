using System.Linq;
using UnityEngine;
using Verse;

namespace ProjectRimFactory.AnimalStation
{
    [StaticConstructorOnStartup]
    public class PlaceWorker_ShowAdjacent : PlaceWorker
    {
        public override void DrawGhost(ThingDef def, IntVec3 center, Rot4 rot, Color ghostCol, Thing thing = null)
        {
            base.DrawGhost(def, center, rot, ghostCol, thing);
            GenDraw.DrawFieldEdges(GenAdj.OccupiedRect(center, rot, def.size).ExpandedBy(1).Cells.ToList().FindAll((IntVec3 c) => c.Standable(Find.CurrentMap)));
        }
    }
}
