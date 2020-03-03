using RimWorld;
using Verse;

namespace ProjectRimFactory.AnimalStation
{
    [StaticConstructorOnStartup]
    public class Building_Shearer : Building_CompHarvester
    {
        public override bool CompValidator(CompHasGatherableBodyResource comp)
        {
            return comp is CompShearable;
        }
    }
}
