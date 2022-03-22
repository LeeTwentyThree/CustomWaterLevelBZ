using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomWaterLevelBZ
{
    internal class VFXConstructionHelper : MonoBehaviour
    {
        public VFXConstructing constructing;
        public float defaultOffset;

        private void Start()
        {
            defaultOffset = constructing.heightOffset;
        }

        private void Update()
        {
            constructing.heightOffset = defaultOffset + Mod.WaterLevel;
        }
    }
}
