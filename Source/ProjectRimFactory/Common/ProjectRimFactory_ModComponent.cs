using HarmonyLib;
using System;
using RimWorld;
using RimWorld.Planet;
using System.Linq;
using System.Reflection;
using Logging;
using ProjectRimFactory.Logging;
using Verse;

//using SimpleFixes;
using ProjectRimFactory.Storage;
using SimpleFixes;
using UnityEngine;

namespace ProjectRimFactory.Common
{
    public partial class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("com.spdskatr.projectrimfactory");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [StaticConstructorOnStartup]
    public class ProjectRimFactory_ModComponent : Mod
    {
        private static readonly ILogToken _lt = LogManager.GetToken();
        public ProjectRimFactory_ModSettings settings;

        public ProjectRimFactory_ModComponent(ModContentPack content) : base(content)
        {
            var log = LogHelper.GetLogger();
            log.Debug(_lt, x => x("Mod constructor"));
            settings = GetSettings<ProjectRimFactory_ModSettings>();
            NoMessySpawns.Instance.Add(
                (position, map, respawningAfterLoad) =>
                    !respawningAfterLoad
                    || map?.thingGrid
                        .ThingsListAtFast(position)
                        .OfType<Building_MassStorageUnit>()
                        .Any() != true, (Building_MassStorageUnit b, Map map) => true);
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            settings.DoWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "ProjectRimFactoryModName".Translate();
        }

        public override void WriteSettings()
        {
            settings.Write();
        }

        public static bool ShouldSuppressDisplace(IntVec3 cell, Map map, bool respawningAfterLoad)
        {
            return !respawningAfterLoad || map?.thingGrid.ThingsListAtFast(cell).OfType<Building_MassStorageUnit>().Any() != true;
        }
    }
}
