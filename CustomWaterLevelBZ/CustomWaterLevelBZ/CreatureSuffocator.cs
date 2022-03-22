using UnityEngine;

namespace CustomWaterLevelBZ
{
    internal class CreatureSuffocator : MonoBehaviour
    {
        public Creature creature;

        private void Update()
        {
            if (transform.position.y > Mod.WaterLevel + 3f)
            {
                creature.liveMixin.TakeDamage(20000f, transform.position);
            }
        }
    }
}
