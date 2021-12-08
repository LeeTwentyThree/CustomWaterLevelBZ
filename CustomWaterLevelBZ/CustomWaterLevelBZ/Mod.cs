using QModManager.API.ModLoading;
using HarmonyLib;
using SMLHelper.V2.Handlers;
using System.Reflection;

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
        }

        public static float WaterLevel
        {
            get
            {
                return config.WaterLevel;
            }
        }
    }
}