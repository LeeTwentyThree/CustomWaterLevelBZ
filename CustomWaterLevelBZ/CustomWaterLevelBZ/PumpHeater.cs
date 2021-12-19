using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CustomWaterLevelBZ
{
    internal class PumpHeater : MonoBehaviour
    {
        private float heatPerSecond = 3f;

        private float heatRadius = 2f;

        private float damageRadius = 1f;

        private float heatDamage = 0.5f;

        private float heatDamageInterval = 1f;

        private float timeLastHeatDamage;

        private BodyTemperature bodyTemperature;

        private void Start()
        {
            bodyTemperature = Player.main.gameObject.GetComponent<BodyTemperature>();
        }

        private void Update()
        {
            if (Vector3.Distance(Player.main.transform.position, transform.position) < heatRadius)
            {
                bodyTemperature.AddCold(-heatPerSecond * Time.deltaTime);
            }
            if (Time.time > timeLastHeatDamage + heatDamageInterval && Vector3.Distance(Player.main.transform.position, transform.position) < damageRadius)
            {
                Player.main.liveMixin.TakeDamage(heatDamage, transform.position, DamageType.Heat);
                timeLastHeatDamage = Time.time;
            }
        }
    }
}
