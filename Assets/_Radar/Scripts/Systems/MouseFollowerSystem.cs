using Radar.Emitters;
using Rader.Services;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Radar.Systems
{
    public partial struct MouseFollowerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<CusrorPositionDataComponent>();
        }

        public void OnUpdate(ref SystemState state)
        {
            if (SystemAPI.TryGetSingleton<CusrorPositionDataComponent>(out var cursorWorldPosition))
            {
                foreach (var (localTransform,signalEmitter) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<MouseFollowerDataComponent>>())
                {
                    float3 direction = new float3(
                        cursorWorldPosition.WorldCursorPosition.x - localTransform.ValueRO.Position.x,
                        0f,
                        cursorWorldPosition.WorldCursorPosition.z - localTransform.ValueRO.Position.z
                        );
                    direction = math.normalize(direction);
                    
                    if (math.any(math.isnan(direction)))
                    {
                        continue;
                    }
                    
                    float angle = math.degrees(math.atan2(direction.x, direction.z));
                    
                    quaternion currentRotation = localTransform.ValueRO.Rotation;
                    quaternion targetRotation = quaternion.Euler(0f, math.radians(angle), 0f);
                    quaternion newRotation = math.slerp(currentRotation, targetRotation, signalEmitter.ValueRO.RotationSpeed * SystemAPI.Time.DeltaTime);
                    
                    localTransform.ValueRW.Rotation = newRotation;
                }
            }
        }
    }
}
