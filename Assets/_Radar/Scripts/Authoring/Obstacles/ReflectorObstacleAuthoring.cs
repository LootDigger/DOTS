using Unity.Entities;
using UnityEngine;


namespace Radar.Obstacles
{
    public struct ReflectorObstacleDataComponent : IComponentData { }

    public class ReflectorObstacleAuthoring : MonoBehaviour
    {
        public class ReflectorObstacleBaker : Baker<ReflectorObstacleAuthoring>
        {
            public override void Bake(ReflectorObstacleAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<ReflectorObstacleDataComponent>(entity);
            }
        }
    }
}