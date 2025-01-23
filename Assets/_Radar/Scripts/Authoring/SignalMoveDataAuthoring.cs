using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Radar.Emitters
{
    public struct SignalMoveDataComponent : IComponentData
    {
        public float speed;
        public float3 direction;
    }
    
    public readonly partial struct SignalDataAspect : IAspect
    {
        public readonly Entity entity;
        public readonly RefRW<SignalMoveDataComponent> moveComponent;
        public readonly RefRW<LocalTransform> localTransform;
    }
    
    public class SignalMoveDataAuthoring : MonoBehaviour
    {
        public float _speed;

        public class SignalMoverBaker : Baker<SignalMoveDataAuthoring>
        {
            public override void Bake(SignalMoveDataAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                SignalMoveDataComponent emitterData = new()
                {
                    speed = authoring._speed
                };
            
                AddComponent(entity, emitterData);
            }
        }
    }
}