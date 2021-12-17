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
                        var creatureTechType = CraftData.GetTechType(__instance.gameObject);
                        if (Mod.AirBreathingCreatures.Contains(creatureTechType))
                        {
                            return;
                        }
                        __instance.liveMixin.Kill();
                    }
                }
            }
        }

        [HarmonyPatch(typeof(Hoverbike))]
        internal static class HoverBike_Patches
        {
            [HarmonyPatch(nameof(Hoverbike.Start))]
            [HarmonyPostfix]
            public static void Start_Postfix(Hoverbike __instance)
            {
                __instance.waterLevelOffset += Mod.WaterLevel;
            }

            [HarmonyPatch(nameof(Hoverbike.AllowedToPilot))]
            [HarmonyPostfix]
            public static void AllowedToPilot_Postfix(Hoverbike __instance, ref bool __result)
            {
                __result = __instance.transform.position.y > Mod.WaterLevel;
            }
        }

        [HarmonyPatch(typeof(Hoverbike))]
        [HarmonyPatch(nameof(Hoverbike.Awake))]
        internal static class Hoverbike_Awake_Patch
        {
            [HarmonyPostfix]
            public static void Postfix(Hoverbike __instance)
            {
                __instance.gameObject.GetComponent<Constructable>().allowedUnderwater = true;
            }
        }

        [HarmonyPatch(typeof(Constructor))]
        [HarmonyPatch(nameof(Constructor.OnRightHandDown))]
        internal static class Constructor_OnRightHandDown_Patch
        {
            [HarmonyPrefix]
            public static bool Prefix()
            {
                if (Mod.config.FixConstructor)
                {
                    return false; // skip original method
                }
                return true;
            }
            [HarmonyPostfix]
            public static void Postfix(Constructor __instance, ref bool __result)
            {
                if (!Mod.config.FixConstructor)
                {
                    return;
                }
                if (__result == true)
                {
                    return; // if we already deployed it, we don't need to deploy a second MBV!
                }
                if (PrecursorMoonPoolTrigger.inMoonpool || PrisonManager.IsInsideAquarium(__instance.transform.position) || Player.main.IsInSub())
                {
                    ErrorMessage.AddMessage("Can't deploy here!");
                    return; // don't need to throw it if you're inside a precursor base, or your own base
                }
                // I nabbed this from dnSpy, that's why it looks so weird
                Vector3 forward = MainCamera.camera.transform.forward;
                __instance.pickupable.Drop(__instance.transform.position + forward * 0.7f + Vector3.down * 0.3f, default(Vector3));
                __instance.GetComponent<Rigidbody>().AddForce(forward * 6.5f, ForceMode.VelocityChange);
                __instance.Deploy(true);
                __instance.OnDeployAnimationStart();
                LargeWorldEntity.Register(__instance.gameObject);
                Utils.PlayEnvSound(__instance.releaseSound, MainCamera.camera.transform.position, 20f);
                GoalManager.main.OnCustomGoalEvent("Release_Constructor");
                __result = true; // play the animation
            }
        }
    }
}
