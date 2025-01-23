using Radar.Emitters;
using Radar.Obstacles;
using Radar.Receiver;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Rendering;
using Radar.Commands;

namespace Radar.Systems
{
    public partial struct ObstacleCollisionSystem : ISystem
    {
        private CollisionFilter _collisionFilter;
        
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            _collisionFilter = new CollisionFilter
            {
                BelongsTo = ~0u, 
                CollidesWith = ~0u,
                GroupIndex = 0
            };
            
            state.RequireForUpdate<SignalMoveDataComponent>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var collisionWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>().PhysicsWorld.CollisionWorld;
            
            // iterate through all signals
            foreach (var signalDataAspect in SystemAPI.Query<SignalDataAspect>())
            {
                NativeList<DistanceHit> hits = new(Allocator.Temp);
                collisionWorld.OverlapSphere(signalDataAspect.localTransform.ValueRO.Position, signalDataAspect.localTransform.ValueRO.Scale, ref hits, _collisionFilter);
                // check all collisions
                foreach (var hit in hits)
                {
                    // we assume the hit entity is obstacle of any kind
                    var obstacle = hit.Entity;
                    if (SystemAPI.HasComponent<AbsorbObstacleDataComponent>(obstacle))
                    {
                        CommandExecuter.ExecuteCommandNonManaged(new AbsorbEntityCommand(signalDataAspect.entity, ecb));
                    }
                    else if(SystemAPI.HasComponent<ReflectorObstacleDataComponent>(obstacle))
                    {
                        CommandExecuter.ExecuteCommandNonManaged(new ReflectEntityCommand(hit, signalDataAspect));
                    }
                    else if (SystemAPI.HasComponent<SignalReceiverDataComponent>(obstacle))
                    {
                        CommandExecuter.ExecuteCommandNonManaged(new ReceiveSignalCommand(
                            SystemAPI.GetComponentRW<SignalReceiverDataComponent>(obstacle), 
                            SystemAPI.GetComponentRW<URPMaterialPropertyBaseColor>(obstacle)));
                    }
                }
            }
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}