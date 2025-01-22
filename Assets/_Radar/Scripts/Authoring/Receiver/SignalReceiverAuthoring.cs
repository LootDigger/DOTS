using Radar.Extensions;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;

namespace Radar.Receiver
{
    public struct SignalReceiverDataComponent : IComponentData
    {
        public bool receivedSignal;
        public bool desiredReceivedSignal;
        public float4 normalColor;
        public float4 receivingSignalColor;
    }
    
    public class SignalReceiverAuthoring : MonoBehaviour
    {
        public SignalReceiverConfig _signalReceiverConfig;
        
        public class ReceiverBaker : Baker<SignalReceiverAuthoring>
        {
            public override void Bake(SignalReceiverAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                if (authoring._signalReceiverConfig == null)
                {
                    Debug.LogError("Signal Receiver Config isn't assigned on " + authoring.gameObject.name);
                    return;
                }
                
                AddComponent(entity, new SignalReceiverDataComponent()
                {
                    receivedSignal = authoring._signalReceiverConfig.defaultReceivingSignalState,
                    desiredReceivedSignal = authoring._signalReceiverConfig.desiredReceivingSignalState,
                    normalColor = authoring._signalReceiverConfig.normalColor.ToFloat4(),
                    receivingSignalColor = authoring._signalReceiverConfig.receivingSignalColor.ToFloat4()
                });
                
                AddComponent(entity, new URPMaterialPropertyBaseColor()
                {
                    Value = authoring._signalReceiverConfig.normalColor.ToFloat4()
                });
            }
        }
    }
}