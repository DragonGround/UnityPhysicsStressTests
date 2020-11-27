using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct Sphere : IComponentData {
}

public class SphereAuthoring : MonoBehaviour, IConvertGameObjectToEntity {
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
        dstManager.AddComponentData<Sphere>(entity, new Sphere());
    }
}