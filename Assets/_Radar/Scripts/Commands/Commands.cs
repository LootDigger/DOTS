using Radar.Emitter;
using Radar.Receiver;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Rendering;

namespace Radar.Commands
{
    public interface ICommand
    {
        void Execute();
    }

    public struct AbsorbEntityCommand : ICommand
    {
        private EntityCommandBuffer _ecb;
        private readonly Entity _entity;

        public AbsorbEntityCommand(Entity entity, EntityCommandBuffer ecb)
        {
            _entity = entity;
            _ecb = ecb;
        }
        
        public void Execute() => _ecb.DestroyEntity(_entity);
    }
    
    public struct ReflectEntityCommand : ICommand
    {
        private readonly DistanceHit _distanceHit;
        private readonly SignalDataAspect _signalDataAspect;

        public ReflectEntityCommand(DistanceHit distanceHit, SignalDataAspect signalDataAspect)
        {
            _distanceHit = distanceHit;
            _signalDataAspect = signalDataAspect;
        }
        
        public void Execute()
        {
            float3 surfaceNormal = _distanceHit.SurfaceNormal;
            _signalDataAspect.moveComponent.ValueRW.direction = math.reflect(_signalDataAspect.moveComponent.ValueRO.direction, surfaceNormal);
        }
    }
    
    public struct ReceiveSignalCommand  : ICommand
    {
        private readonly RefRW<SignalReceiverDataComponent> _receiverDataComponent;
        private readonly RefRW<URPMaterialPropertyBaseColor> _materialDataComponent;
        
        public ReceiveSignalCommand(RefRW<SignalReceiverDataComponent> receiverDataComponent, RefRW<URPMaterialPropertyBaseColor> materialDataComponent)
        {
            _receiverDataComponent = receiverDataComponent;
            _materialDataComponent = materialDataComponent;
        }
        
        public void Execute()
        {
            _receiverDataComponent.ValueRW.receivedSignal = true;
            _materialDataComponent.ValueRW.Value = _receiverDataComponent.ValueRO.receivingSignalColor;
        }
    }
}