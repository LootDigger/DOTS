using Radar.Systems;
using Radar.Utils;
using Unity.Entities;
using UnityEngine;

namespace Radar.Scriptables
{
    [CreateAssetMenu(fileName = "GameWinCondition", menuName = "Configs/GameEventConditions/GameWinCondition")]
    public class GameWinConditionProvider : ScriptableObject, IECSBooleanProvider
    {
        private Entity _winListenerEntity;
        private EntityManager _entityManager;

        public void Init()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            _winListenerEntity = _entityManager.CreateEntity();
            _entityManager.AddComponentData(_winListenerEntity, new GameWinConditionStateDataComponent()
            {
                areConditionMatched = false
            });
        }

        public bool GetValue()
        {
            var conditionStateData =
                _entityManager.GetComponentData<GameWinConditionStateDataComponent>(_winListenerEntity);
            return conditionStateData.areConditionMatched;
        }
    }
}