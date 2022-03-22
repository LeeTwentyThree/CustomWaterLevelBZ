using SMLHelper.V2.Json;
using SMLHelper.V2.Options.Attributes;
using SMLHelper.V2.Json.Attributes;
using System;

namespace CustomWaterLevelBZ
{
    [FileName("WaterLevelData")]
    [Serializable]
    public class WaterLevelData : SaveDataCache
    {
        public float WaterLevel;
        public float TimeLastChange;
    }
}