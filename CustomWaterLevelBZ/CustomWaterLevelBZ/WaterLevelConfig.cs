using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;

namespace CustomWaterLevelBZ
{
    [Menu("Custom Water Level")]
    public class WaterLevelConfig : ConfigFile
    {
        [Slider(Label = "Water level", Tooltip = "The level of the water, in meters, relative to the default water level.\nRESTART REQUIRED.", DefaultValue = 0, Min = -1250, Max = 500, Step = 5)]
        public float WaterLevel = 0f;
        [Toggle(Label = "Suffocate fish", Tooltip = "Whether fish should suffocate when above water or not.")]
        public bool SuffocateFish = true;
        [Toggle(Label = "Remove ALL shoals of fish", Tooltip = "Whether ALL shoals of fish (regardless of depth) should be removed from the game. They may ruin the immersion.")]
        public bool RemoveSchoolsOfFish = false;
        [Toggle(Label = "Mobile Vehicle Bay fix", Tooltip = "If enabled, allows you to deploy Mobile Vehicle Bays on land.")]
        public bool FixConstructor = true;
        [Toggle(Label = "Improve PRAWN air mobility", Tooltip = "If enabled, allows the PRAWN Suit to move freely in water AND on land.")]
        public bool BuffExosuit = true;
        [Toggle(Label = "Unobtainable resource fix", Tooltip = "If enabled, spawns spiral plants outside of Ventgardens to make the game possible with low water levels.\nRESTART REQUIRED.")]
        public bool AddSpiralPlants = true;
        [Toggle(Label = "Adjust creature spawns", Tooltip = "If enabled, spawns land creatures in areas that would otherwise be empty, and vice versa.\nRESTART REQUIRED.")]
        public bool AdjustCreatureSpawns = true;
        [Toggle(Label = "Warmth fix", Tooltip = "If enabled, you will gain heat while sheltered in caves.")]
        public bool ColdFix = true;
        [Toggle(Label = "Unlock pipes & pumps", Tooltip = "If enabled, pipes & air pumps will be unlocked at the start of a new save (they were previously unobtainable).")]
        public bool UnlockAirPumps = true;
        [Toggle(Label = "Pumps generate heat", Tooltip = "If enabled, deployed air pumps will generate heat.")]
        public bool PumpsGenerateHeat = true;
        [Toggle(Label = "Add ice worms (WIP)", Tooltip = "If enabled, ice worms will visit new areas and attack you.")]
        public bool IceWorms = false;
    }
}