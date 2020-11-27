using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct Spawner : IComponentData {
    public int Amount;
    public Entity Prefab;
}

public struct Spawn : IComponentData {
}

public class SpawnerAuthoring : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity {
    public int Amount;
    public GameObject Prefab;

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) {
        referencedPrefabs.Add(Prefab);
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
        var spawnerData = new Spawner {
            Prefab = conversionSystem.GetPrimaryEntity(Prefab),
            Amount = Amount
        };
        dstManager.AddComponentData(entity, spawnerData);
    }
}