using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;

namespace CustomWaterLevelBZ
{
    [Menu("Custom Water Level")]
    public class WaterLevelConfig : ConfigFile
    {
        [Slider(Label = "Water level", Tooltip = "The level of the water, in meters, relative to the default water level.", DefaultValue = 0, Min = -1250, Max = 500, Step = 5)]
        public float WaterLevel = 0f;
        [Toggle(Label = "Suffocate fish", Tooltip = "Whether fish should suffocate when above water or not.")]
        public bool SuffocateFish = true;
        [Toggle(Label = "Remove ALL shoals of fish", Tooltip = "Whether ALL shoals of fish (regardless of Y position) should be removed from the mod.")]
        public bool RemoveSchoolsOfFish = false;
    }
}