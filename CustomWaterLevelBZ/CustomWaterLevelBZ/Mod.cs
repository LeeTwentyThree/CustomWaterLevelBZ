using QModManager.API.ModLoading;
using HarmonyLib;
using SMLHelper.V2.Handlers;
using System.Reflection;
using System.Collections.Generic;

namespace CustomWaterLevelBZ
{
    [QModCore]
    public static class Mod
    {
        public static WaterLevelConfig config = OptionsPanelHandler.Main.RegisterModOptions<WaterLevelConfig>();

        internal static Assembly assembly = Assembly.GetExecutingAssembly();

        [QModPatch]
        public static void Patch()
        {
            Harmony harmony = new Harmony("Lee23.CustomWaterLevelBZ");
            harmony.PatchAll(assembly);

            PatchSpawns();
        }

        public static void PatchSpawns()
        {
            if (config.AddSpiralPlants)
            {
                string spiralPlantClassId = "08a2d1df-4a80-47cf-b03c-30e024e5bfe2";
                LootDistributionHandler.AddLootDistributionData(spiralPlantClassId, new LootDistributionData.SrcData()
                {
                    distribution = new List<LootDistributionData.BiomeData>()
                    {
                        new LootDistributionData.BiomeData()
                        {
                            biome = BiomeType.TreeSpires_Ground,
                            count = 1,
                            probability = 0.5f
                        },
                    },
                    prefabPath = spiralPlantClassId
                });
            }
        }

        public static float WaterLevel
        {
            get
            {
                return config.WaterLevel;
            }
        }

        public static readonly List<TechType> AirBreathingCreatures = new List<TechType>()
        {
            TechType.SpikeyTrap,
            TechType.SnowStalker,
            TechType.SnowStalkerBaby,
            TechType.Pinnacarid,
            TechType.RockPuncher,
            TechType.Penguin,
            TechType.PenguinBaby,
            TechType.Skyray,
            TechType.SpinnerFish,
            TechType.TrivalveBlue,
            TechType.TrivalveYellow,
            TechType.Rockgrub,
            TechType.IceWorm,
            TechType.IceWormSpawner,
            TechType.LargeVentGarden,
            TechType.SmallVentGarden
        };

        public static float DefaultFogDistance = 0f;

        public static float UndergroundFogDistance = 0.6f;

        public static float DefaultColorDecay = 0.1f;

        public static float UndergroundColorDecay = 2f;

        public static float CaveDepth = -550f;

        public static float PlayerY
        {
            get
            {
                if (MainCamera.camera == null)
                {
                    return 0f;
                }
                return MainCamera.camera.transform.position.y;
            }
        }

        public static bool PlayerWalkingInCave
        {
            get
            {
                if (PlayerY < WaterLevel)
                {
                    return false;
                }
                return PlayerY < CaveDepth;
            }
        }
    }
}