using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Radar.Extensions
{
    public static class LocalTransformExtensions
    {
        public static float3 Forward(this LocalTransform localTransform)
        {
            return math.mul(localTransform.Rotation, new float3(0, 0, 1));
        }
        
        public static float3 Right(this LocalTransform localTransform)
        {
            return math.mul(localTransform.Rotation, new float3(1, 0, 0));
        }
        
        public static float3 Up(this LocalTransform localTransform)
        {
            return math.mul(localTransform.Rotation, new float3(0, 1, 0));
        }
    }

    public static class ColorToFloat4
    {
        public static float4 ToFloat4(this Color color)
        {
            return new float4(color.r, color.g, color.b, color.a);
        }
    }
}