using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SMLHelper.V2.Assets;
using System.Collections;

namespace CustomWaterLevelBZ
{
    internal class HatchFixPrefab : Spawnable
    {
        public HatchFixPrefab() : base("HatchFixObject", "Hatch", ".")
        {

        }

        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            var prefab = new GameObject(ClassID);
            prefab.SetActive(false);
            prefab.AddComponent<LargeWorldEntity>().cellLevel = LargeWorldEntity.CellLevel.Near;
            prefab.AddComponent<TechTag>().type = TechType;

            GameObject entrance = new GameObject("Entrance");
            entrance.transform.parent = prefab.transform;
            entrance.transform.localPosition = new Vector3(0f, 0f, 1f);
            entrance.AddComponent<BoxCollider>().size = new Vector3(2f, 2f, 1f);
            entrance.AddComponent<HatchFixTarget>().enter = true;

            GameObject exit = new GameObject("Exit");
            exit.transform.parent = prefab.transform;
            exit.transform.localPosition = new Vector3(0f, 0f, -1f);
            exit.AddComponent<BoxCollider>().size = new Vector3(2f, 2f, 1f);
            exit.AddComponent<HatchFixTarget>().enter = false;

            yield return null;
            gameObject.Set(prefab);
        }

        public override List<SpawnLocation> CoordinatedSpawns => new List<SpawnLocation>()
        {
            new SpawnLocation(new Vector3(552.83f, -202.31f, -1070.81f), Vector3.up * 180), // omega lab
            new SpawnLocation(new Vector3(-254.80f, -126.84f, -249.50f), Vector3.zero), // twisty tech site
            new SpawnLocation(new Vector3(267.75f, -235.27f, -1307.54f), new Vector3(345, 0, 0)), // crashedship 2 (lilypads)
        };
    }
}
