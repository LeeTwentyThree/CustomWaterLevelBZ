using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomWaterLevelBZ
{
    internal class WorldForcesHelper : MonoBehaviour
    {
        public WorldForces worldForces;
        public float defaultWaterDepth;

        private void Start()
        {
            defaultWaterDepth = worldForces.waterDepth;
        }

        private void Update()
        {
            worldForces.waterDepth = defaultWaterDepth + Mod.WaterLevel;
        }
    }
}
