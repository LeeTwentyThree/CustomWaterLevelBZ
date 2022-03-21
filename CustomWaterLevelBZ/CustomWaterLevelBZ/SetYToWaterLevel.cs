using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomWaterLevelBZ
{
    internal class SetYToWaterLevel : MonoBehaviour
    {
        public void Update()
        {
            transform.position = new Vector3(0f, Mod.WaterLevel, 0f);
        }
    }
}
