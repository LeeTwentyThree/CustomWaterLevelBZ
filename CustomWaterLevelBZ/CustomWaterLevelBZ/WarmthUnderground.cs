using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomWaterLevelBZ
{
    internal class WarmthUnderground : MonoBehaviour
    {
        private static LayerMask layerMask = LayerMask.GetMask("Default", "TerrainCollider");

        private BodyTemperature bodyTemperature;

        private float maxDistance = 25f;

        private float heatPerSecond = 5f;

        private void Start()
        {
            bodyTemperature = GetComponent<BodyTemperature>();
        }

        private void Update()
        {
            float playerY = transform.position.y;
            if (playerY > 0f) // dont interfere with vanilla areas
            {
                return;
            }
            if (playerY < Mod.WaterLevel)
            {
                return;
            }
            if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, maxDistance, layerMask, QueryTriggerInteraction.Ignore))
            {
                bodyTemperature.AddCold(-heatPerSecond * Time.deltaTime);
            }
        }
    }
}
