using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

[UpdateInGroup(typeof(SimulationSystemGroup))]
public class SpawnerSystem : SystemBase {
    BeginInitializationEntityCommandBufferSystem _ecbs;

    protected override void OnCreate() {
        _ecbs = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate() {
        var commandBuffer = _ecbs.CreateCommandBuffer().AsParallelWriter();

        var r = new Random((uint) UnityEngine.Random.Range(1, 100000));
        Entities
            .WithName("SpawnerSystem")
            .WithBurst(FloatMode.Default, FloatPrecision.Standard, true)
            .WithAll<Spawn>()
            .ForEach((Entity entity, int entityInQueryIndex, in Spawner spawner, in LocalToWorld location) => {
                for (var i = 0; i < spawner.Amount; i++) {
                    var instance = commandBuffer.Instantiate(entityInQueryIndex, spawner.Prefab);

                    // Place the instantiated in a grid with some noise
                    var position = math.transform(location.Value,
                        r.NextFloat3() * 100 - new float3(50, -20, 50));
                    commandBuffer.SetComponent(entityInQueryIndex, instance, new Translation { Value = position });
                }

                commandBuffer.RemoveComponent<Spawn>(entityInQueryIndex, entity);
            }).ScheduleParallel();
        _ecbs.AddJobHandleForProducer(Dependency);
    }
}