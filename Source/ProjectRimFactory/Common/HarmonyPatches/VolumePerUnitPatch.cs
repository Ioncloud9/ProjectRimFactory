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
        [HarmonyPatch(typeof(ThingDef), "get_VolumePerUnit")]
        public class VolumePerUnitPatch
        {
            public static void Postfix(ThingDef __instance, ref float __result)
            {
                if (__instance == PRFDefOf.Paperclip)
                {
                    __result = 0.00001f;
                }
            }
        }
    }
}
