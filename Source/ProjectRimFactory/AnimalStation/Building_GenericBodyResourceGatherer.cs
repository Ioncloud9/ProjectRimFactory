using RimWorld;
using Verse;

namespace ProjectRimFactory.AnimalStation
{
    [StaticConstructorOnStartup]
    public class Building_GenericBodyResourceGatherer : Building_CompHarvester
    {
        public override bool CompValidator(CompHasGatherableBodyResource comp)
        {
            return true;
        }
    }
}
