using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomWaterLevelBZ
{
    internal class WaterMove : MonoBehaviour
    {
        public static WaterMove main;

        public float waterLevel;

        private void Awake()
        {
            main = this;
        }

        public bool InUse
        {
            get
            {
                return Mod.config.AutomaticChange;
            }
        }
    }
}
