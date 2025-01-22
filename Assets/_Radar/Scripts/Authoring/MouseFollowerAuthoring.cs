using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Rader.Services
{
    public struct MouseFollowerDataComponent : IComponentData
    {
        public float RotationSpeed;
    }
    
    public struct CusrorPositionDataComponent : IComponentData
    {
        public float3 WorldCursorPosition;
    }

    public class MouseFollowerAuthoring : MonoBehaviour
    {
        public float RotationSpeed = 50f;

        public class MouseFollowerBaker : Baker<MouseFollowerAuthoring>
        {
            public override void Bake(MouseFollowerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new MouseFollowerDataComponent()
                {
                    RotationSpeed = authoring.RotationSpeed
                });
            }
        }
    }
}