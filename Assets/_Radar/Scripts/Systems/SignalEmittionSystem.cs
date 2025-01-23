using Radar.Emitters;
using Radar.Emitters;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

namespace Radar.Systems
{
    public partial struct SignalEmittionSystem : ISystem
    {
        #region LIFE_CYCLE
        public void OnCreate(ref SystemState state) => state.RequireForUpdate<SignalEmitterDataComponent>();

        public void OnUpdate(ref SystemState state)
        {
            EmitterActivityLoopCheck(ref state);
            EmitterLoopRoutine(ref state);
        }

        #endregion
        
        #region ROUTINE

        private void EmitterActivityLoopCheck(ref SystemState state)
        {
            foreach (var gameWinCondition in SystemAPI.Query<RefRO<GameWinConditionStateDataComponent>>())
            {
                if(!gameWinCondition.ValueRO.areConditionMatched) return;
                state.Enabled = false;
            }
        }
        
        private void EmitterLoopRoutine(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            float deltaTime = SystemAPI.Time.DeltaTime;
            
            foreach (var (emitterData, transform) 
                     in SystemAPI.Query<RefRW<SignalEmitterDataComponent>,RefRO<LocalTransform>>())
            {
                UpdateTimer(emitterData, deltaTime);
                if (emitterData.ValueRO.ElapsedTime < emitterData.ValueRO.SpawnInterval) continue;
                SpawnParticles(emitterData, transform, ecb);
            }
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }

        private void SpawnParticles(RefRW<SignalEmitterDataComponent> emitterData, RefRO<LocalTransform> emitterTransform, EntityCommandBuffer ecb)
        {
            if (emitterData.ValueRO.SignalParticlePrefab == Entity.Null)
            {
                Debug.LogError("SignalParticlePrefab is null!");
                return;
            }

            ResetTimer(emitterData);
            
            int particleCount = emitterData.ValueRW.WaveParticleCount;
            float waveSpreadAngle = emitterData.ValueRO.WaveSpreadAngle;
            
            float angleStep = particleCount > 1
                ? waveSpreadAngle / (particleCount - 1)
                : 0f;
            float startAngle = -waveSpreadAngle / 2;

            float3 forwardVector = emitterTransform.ValueRO.Forward();

            for (int i = 0; i < particleCount; i++)
            {
                Entity entity = ecb.Instantiate(emitterData.ValueRO.SignalParticlePrefab);
                
                float entityAngle = math.radians(startAngle + angleStep * i);
                quaternion rotation = quaternion.AxisAngle(new float3(0,1,0),entityAngle);
                
                float3 direction = math.mul(rotation, forwardVector);
                direction.y = 0;
                direction = math.normalize(direction);
                
                float3 spawnPosition = emitterTransform.ValueRO.Position + direction * emitterData.ValueRO.WaveSpawnDistance;
                
                ecb.AddComponent(entity, new LocalTransform
                {
                    Position = spawnPosition,
                    Rotation = quaternion.identity,
                    Scale = 1f
                });
                
                ecb.AddComponent(entity, new URPMaterialPropertyBaseColor
                {
                    Value = emitterData.ValueRO.WaveColor
                });
                
                ecb.AddComponent(entity, new SignalMoveDataComponent()
                {
                    speed = emitterData.ValueRO.SignalSpeed,
                    direction = direction
                });
            }
        }
        
        #endregion

        #region  HELPERS
        private void UpdateTimer(RefRW<SignalEmitterDataComponent> emitterData, float deltaTime)
        {
            emitterData.ValueRW.ElapsedTime += deltaTime;
        }

        private void ResetTimer(RefRW<SignalEmitterDataComponent> emitterData)
        {
            emitterData.ValueRW.ElapsedTime = 0f;
        }
        #endregion
    }
}