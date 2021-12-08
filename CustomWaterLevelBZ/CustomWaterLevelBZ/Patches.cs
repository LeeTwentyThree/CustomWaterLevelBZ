using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;

namespace CustomWaterLevelBZ
{
    internal static class Patches
    {
        [HarmonyPatch(typeof(Ocean))]
        internal static class Ocean_Patches
        {
            [HarmonyPatch(nameof(Ocean.Awake))]
            [HarmonyPostfix]
            public static void Awake_Postfix(Ocean __instance)
            {
                __instance.defaultOceanLevel = Mod.WaterLevel;

                UpdateOceanPosition();
            }

            [HarmonyPatch(nameof(Ocean.RestoreOceanLevel))]
            [HarmonyPostfix]
            public static void RestoreOceanLevel_Postfix()
            {
                UpdateOceanPosition();
            }

            private static void UpdateOceanPosition()
            {
                var pos = Ocean.main.transform.position;
                pos.y = Mod.WaterLevel;
                Ocean.main.transform.position = pos;
            }
        }

        [HarmonyPatch(typeof(WaterPlane))]
        [HarmonyPatch(nameof(WaterPlane.Start))]
        internal static class WaterPlane_Start_Patch
        {
            [HarmonyPostfix]
            public static void Postfix(WaterPlane __instance)
            {
                __instance.transform.position = new Vector3(0f, Mod.WaterLevel, 0f);
            }
        }

        [HarmonyPatch(typeof(WaterSurface))]
        [HarmonyPatch(nameof(WaterSurface.Start))]
        internal static class WaterSurface_Start_Patch
        {
            [HarmonyPostfix]
            public static void Postfix(WaterSurface __instance)
            {
                __instance.transform.position = new Vector3(0f, Mod.WaterLevel, 0f);
            }
        }

        [HarmonyPatch(typeof(WorldForces))]
        [HarmonyPatch(nameof(WorldForces.Start))]
        internal static class WorldForces_Awake_Patch
        {
            [HarmonyPostfix]
            public static void Postfix(WorldForces __instance)
            {
                __instance.waterDepth += Mod.WaterLevel;
            }
        }

        [HarmonyPatch(typeof(PipeSurfaceFloater))]
        [HarmonyPatch(nameof(PipeSurfaceFloater.UpdateRigidBody))]
        internal static class PipeSurfaceFloater_UpdateRigidbody_Patch
        {
            [HarmonyPostfix]
            public static void Postfix(PipeSurfaceFloater __instance)
            {
                UWE.Utils.SetIsKinematicAndUpdateInterpolation(__instance.rigidBody, true);
            }
        }

        [HarmonyPatch(typeof(VFXSchoolFish))]
        [HarmonyPatch(nameof(VFXSchoolFish.Awake))]
        internal static class VFXSchoolFish_Awake_Patch
        {
            [HarmonyPostfix]
            public static void Postfix(VFXSchoolFish __instance)
            {
                bool shouldRemove = __instance.transform.position.y > Mod.WaterLevel - 5f;
                if (Mod.config.RemoveSchoolsOfFish)
                {
                    shouldRemove = true;
                }
                if (shouldRemove)
                {
                    UnityEngine.Object.Destroy(__instance.gameObject);
                }
            }
        }

        [HarmonyPatch(typeof(Creature))]
        [HarmonyPatch(nameof(Creature.Start))]
        internal static class Creature_Start_Patch
        {
            [HarmonyPostfix]
            public static void Postfix(Creature __instance)
            {
                if (Mod.config.SuffocateFish)
                {
                    var yPos = __instance.transform.position.y;
                    if (yPos > Mod.WaterLevel + 3f)
                    {
                        __instance.liveMixin.Kill();
                    }
                }
            }
        }
    }
}
