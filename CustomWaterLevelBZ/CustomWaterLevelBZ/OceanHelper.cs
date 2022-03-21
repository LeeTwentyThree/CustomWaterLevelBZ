using UnityEngine;

namespace CustomWaterLevelBZ
{
    internal class OceanHelper : MonoBehaviour
    {
        public Ocean ocean;

        private void Update()
        {
            ocean.transform.position = new Vector3(0f, Mod.WaterLevel, 0f);
            ocean.defaultOceanLevel = Mod.WaterLevel;
        }
    }
}
