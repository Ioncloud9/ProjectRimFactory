using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using RimWorld;
using System.Reflection;
using ProjectRimFactory.Storage;
using Verse.AI;
using System.Reflection.Emit;
using HarmonyLib;

namespace ProjectRimFactory.Common
{
    public partial class HarmonyPatches
    {
        [HarmonyPatch(typeof(PathGrid), "CalculatedMovementDifficultyAt")]
        public static class PathGridPatch
        {
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> previousInstructions)
            {
                FieldInfo match = typeof(Thing).GetField("def");
                FieldInfo match2 = typeof(BuildableDef).GetField("pathCost");
                FieldInfo previousOperand = null;
                foreach (CodeInstruction instruction in previousInstructions)
                {
                    var curOperand = instruction.operand as FieldInfo;
                    if (previousOperand == match && curOperand == match2)
                    {
                        yield return new CodeInstruction(OpCodes.Ldarg_1);
                        yield return new CodeInstruction(OpCodes.Ldarg_0);
                        yield return new CodeInstruction(OpCodes.Ldfld,
                            typeof(PathGrid).GetField("map", BindingFlags.NonPublic | BindingFlags.Instance));
                        yield return new CodeInstruction(OpCodes.Call,
                            typeof(PathGridPatch).GetMethod("ApparentPathCost"));
                    }
                    else
                    {
                        yield return instruction;
                    }

                    previousOperand = instruction.operand as FieldInfo;
                    if (previousOperand == null) break;
                }
            }

            public static int ApparentPathCost(ThingDef def, IntVec3 c, Map map)
            {
                Building b = c.GetFirstBuilding(map);
                if (b is Building_MassStorageUnit)
                {
                    return (b.def == def) ? b.def.pathCost : 0;
                }

                return def.pathCost;
            }
        }
    }
}
