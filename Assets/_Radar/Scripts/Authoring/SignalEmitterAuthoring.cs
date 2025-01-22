using Radar.Extensions;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace Radar.Emitters
{
   public struct SignalEmitterDataComponent : IComponentData
   {
      public Entity SignalParticlePrefab;
      public float4 WaveColor;
      
      public int WaveParticleCount;
      public float WaveSpreadAngle;
      public float WaveSpawnDistance;
      
      public float ElapsedTime;
      public float SpawnInterval;

      public float SignalSpeed;
   }
   
   public class SignalEmitterAuthoring : MonoBehaviour
   {
      [SerializeField]
      private SignalEmitterConfig _emitterConfig;
      
      [SerializeField]
      private SignalMovementConfig _signalMoveConfig;
      
      public class EmitterBaker : Baker<SignalEmitterAuthoring>
      {
         public override void Bake(SignalEmitterAuthoring authoring)
         {
            if (authoring._emitterConfig == null)
            {
               Debug.LogError("Signal Emitter Config isn't assigned on " + authoring.gameObject.name);
               return;
            }
            
            if (authoring._signalMoveConfig == null)
            {
               Debug.LogError("Signal Movement Config isn't assigned on " + authoring.gameObject.name);
               return;
            }
            
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            SignalEmitterDataComponent emitterData = new()
            {
               SignalParticlePrefab = GetEntity(authoring._emitterConfig.SignalParticlePrefab, TransformUsageFlags.Dynamic),
               WaveParticleCount = authoring._emitterConfig.WaveParticleCount,
               WaveSpreadAngle = authoring._emitterConfig.WaveSpreadAngle,
               WaveColor = authoring._emitterConfig.WaveParticlesColor.ToFloat4(),
               WaveSpawnDistance = authoring._emitterConfig.WaveSpawnDistance,
               SpawnInterval = authoring._emitterConfig.SpawnInterval,
               SignalSpeed = authoring._signalMoveConfig.Speed
            };
            AddComponent(entity, emitterData);
         }
      }
   }
}