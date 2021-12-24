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

            PatchPrefabs();
            PatchSpawns();
        }

        public static void PatchPrefabs()
        {
            hatchFixPrefab = new HatchFixPrefab();
            hatchFixPrefab.Patch();
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
            if (!config.AdjustCreatureSpawns)
            {
                return;
            }
            string rockPuncherClassId = "b6e25aff-b0cd-48ef-91d1-a187af94a992";
            string skyRayClassId = "6a1b444f-138f-46fa-88bb-d673a2ceb689";
            string pinnacaridClassId = "f9eccfe2-a06f-4c06-bc57-01c2e50ffbe8";
            string snowStalkerBabyClassId = "78d3dbce-856f-4eba-951c-bd99870554e2";
            string arcticPeeperClassId = "a1c75dba-8703-481a-bdda-669071ab268f";
            string hoopfishClassId = "284ceeb6-b437-4aca-a8bd-d54f336cbef8";
            string triopsClassId = "f96f54f1-e323-4841-a025-c86fb1292cbf";
            string penguinClassId = "74ded0e7-d394-4703-9e53-4384b37f9433";
            string spinnerfishClassId = "29694eb8-0bfa-454d-a5db-21aa39ecd93b";
            string iceWormSpawnerClassId = CraftData.GetClassIdForTechType(TechType.IceWormSpawner);
            if (WaterLevel <= -500f)
            {
                LootDistributionHandler.EditLootDistributionData(rockPuncherClassId, BiomeType.CrystalCave_Open, 3f, 1);
                LootDistributionHandler.EditLootDistributionData(rockPuncherClassId, BiomeType.FabricatorCavern_Open, 3f, 1);
                LootDistributionHandler.EditLootDistributionData(skyRayClassId, BiomeType.CrystalCave_Open, 3f, 2);
                LootDistributionHandler.EditLootDistributionData(skyRayClassId, BiomeType.FabricatorCavern_Open, 3f, 2);
                LootDistributionHandler.EditLootDistributionData(pinnacaridClassId, BiomeType.CrystalCave_Open, 3f, 2);
                LootDistributionHandler.EditLootDistributionData(pinnacaridClassId, BiomeType.FabricatorCavern_Open, 3f, 2);
                LootDistributionHandler.EditLootDistributionData(penguinClassId, BiomeType.CrystalCave_Open, 3f, 2);
                LootDistributionHandler.EditLootDistributionData(penguinClassId, BiomeType.FabricatorCavern_Open, 3f, 2);
                LootDistributionHandler.EditLootDistributionData(spinnerfishClassId, BiomeType.CrystalCave_Open, 3f, 2);
                LootDistributionHandler.EditLootDistributionData(spinnerfishClassId, BiomeType.FabricatorCavern_Open, 3f, 2);
            }
            if (WaterLevel <= -20f)
            {
                LootDistributionHandler.EditLootDistributionData(skyRayClassId, BiomeType.TwistyBridges_Shallow_Ground, 0.9f, 4);
                LootDistributionHandler.EditLootDistributionData(pinnacaridClassId, BiomeType.TwistyBridges_Shallow_Open, 1f, 1);
                LootDistributionHandler.EditLootDistributionData(snowStalkerBabyClassId, BiomeType.SparseArctic_Open, 1f, 1);
            }
            if (WaterLevel <= -70f)
            {
                LootDistributionHandler.EditLootDistributionData(iceWormSpawnerClassId, BiomeType.PurpleVents_Ground, 0.07f, 1);
                LootDistributionHandler.EditLootDistributionData(penguinClassId, BiomeType.PurpleVents_Ground, 0.3f, 5);
                LootDistributionHandler.EditLootDistributionData(penguinClassId, BiomeType.PurpleVents_Wall, 0.3f, 5);
                LootDistributionHandler.EditLootDistributionData(penguinClassId, BiomeType.PurpleVents_Vent, 0.3f, 5);
                LootDistributionHandler.EditLootDistributionData(iceWormSpawnerClassId, BiomeType.ThermalSpires_Ground, 0.07f, 1);
                LootDistributionHandler.EditLootDistributionData(penguinClassId, BiomeType.ThermalSpires_Ground, 0.3f, 5);
            }
            if (WaterLevel <= -150f)
            {
                LootDistributionHandler.EditLootDistributionData(pinnacaridClassId, BiomeType.LilyPads_Open, 1f, 1);
                LootDistributionHandler.EditLootDistributionData(pinnacaridClassId, BiomeType.LilyPads_Deep_Open, 1f, 1);
                LootDistributionHandler.EditLootDistributionData(iceWormSpawnerClassId, BiomeType.LilyPads_Ground, 0.07f, 1);
            }
            if (WaterLevel >= 40f)
            {
                LootDistributionHandler.EditLootDistributionData(arcticPeeperClassId, BiomeType.ArcticSpires_Generic, 1f, 6);
                LootDistributionHandler.EditLootDistributionData(hoopfishClassId, BiomeType.ArcticSpires_Generic, 1f, 6);
                LootDistributionHandler.EditLootDistributionData(triopsClassId, BiomeType.ArcticSpires_Cave, 1f, 6);
                LootDistributionHandler.EditLootDistributionData(arcticPeeperClassId, BiomeType.GlacialBasin_Generic, 1f, 6);
                LootDistributionHandler.EditLootDistributionData(hoopfishClassId, BiomeType.GlacialBasin_Generic, 1f, 6);
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

        public static float CaveDepth = -320;

        internal static HatchFixPrefab hatchFixPrefab;

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