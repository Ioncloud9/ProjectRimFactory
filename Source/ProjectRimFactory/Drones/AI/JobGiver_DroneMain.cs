using ProjectRimFactory.Common;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logging;
using ProjectRimFactory.Logging;
using Verse;
using Verse.AI;

namespace ProjectRimFactory.Drones.AI
{
    [StaticConstructorOnStartup]
    public class JobGiver_DroneMain : ThinkNode_JobGiver
    {
        private static readonly ILogToken _lt = LogManager.GetToken();
        public JobGiver_DroneMain(){}
        protected override Job TryGiveJob(Pawn pawn)
        {
            var log = LogHelper.GetLogger();
            log.Debug(_lt, x => x("Entered TryGiveJob"));
            Pawn_Drone drone = (Pawn_Drone)pawn;
            if (drone.station != null)
            {
                if (drone.station.Spawned && drone.station.Map == pawn.Map)
                {
                    Job result;
                    if (drone.station is Building_WorkGiverDroneStation b)
                    {
                        pawn.workSettings = new Pawn_WorkSettings(pawn);
                        pawn.workSettings.EnableAndInitialize();
                        pawn.workSettings.DisableAll();
                        foreach (WorkTypeDef def in b.WorkTypes)
                        {
                            pawn.workSettings.SetPriority(def, 3);
                        }
                        // So the station finds the best job for the pawn
                        result = b.TryIssueJobPackageDrone(drone, true).Job;
                        if (result == null)
                        {
                            result = b.TryIssueJobPackageDrone(drone, false).Job;
                        }
                    }
                    else
                    {
                        result = drone.station.TryGiveJob();
                    }
                    if (result == null)
                    {
                        result = new Job(PRFDefOf.PRFDrone_ReturnToStation, drone.station);
                    }
                    return result;
                }
                return new Job(PRFDefOf.PRFDrone_SelfTerminate);
            }
            return null;
        }
    }
}
