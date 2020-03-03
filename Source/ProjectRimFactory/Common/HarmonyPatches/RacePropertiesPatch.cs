using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using Verse;

namespace ProjectRimFactory.Common
{
    public partial class HarmonyPatches
    {
        [HarmonyPatch(typeof(RaceProperties), "get_IsFlesh")]
        public static class RacePropertiesPatch
        {
            public static void Postfix(ref bool __result, RaceProperties __instance)
            {
                if (__instance.FleshType == PRFDefOf.PRFDroneFlesh)
                {
                    __result = false;
                }
            }
        }
    }
}
