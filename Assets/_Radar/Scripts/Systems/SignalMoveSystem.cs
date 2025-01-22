using Unity.Entities;
using Radar.Emitter;
using Unity.Transforms;

namespace Radar.Systems
{
    public partial struct SignalMoveSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SignalMoveDataComponent>();
        }

        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (moveData,transform) in SystemAPI.Query<RefRO<SignalMoveDataComponent>, RefRW<LocalTransform>>())
            {
                transform.ValueRW.Position += moveData.ValueRO.direction * moveData.ValueRO.speed * deltaTime;
            }
        }
    }
}
