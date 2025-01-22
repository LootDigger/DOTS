using Unity.Entities;
using UnityEngine;

namespace Radar.Obstacles
{
    public struct AbsorbObstacleDataComponent : IComponentData { }
    
    public class AbsorbObstacleAuthoring : MonoBehaviour
    {
        public class AbsorbObstacleBaker : Baker<AbsorbObstacleAuthoring>
        {
            public override void Bake(AbsorbObstacleAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<AbsorbObstacleDataComponent>(entity);
            }
        }
    }
}