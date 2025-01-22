using Radar.Receiver;
using Unity.Burst;
using Unity.Entities;

namespace Radar.Systems
{
    public struct GameWinConditionStateDataComponent : IComponentData
    {
        public bool areConditionMatched;
    }
    
    [BurstCompile]
    public partial struct GameWinConditionCheckerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SignalReceiverDataComponent>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            if(!SystemAPI.TryGetSingletonRW<GameWinConditionStateDataComponent>(out var winState)) return;
            
            foreach (var signalReceiver in SystemAPI.Query<SignalReceiverDataComponent>())
            {
                if (signalReceiver.receivedSignal != signalReceiver.desiredReceivedSignal)
                {
                    winState.ValueRW.areConditionMatched = false;
                    return;
                }
            }
            winState.ValueRW.areConditionMatched = true;
        }
    }
}