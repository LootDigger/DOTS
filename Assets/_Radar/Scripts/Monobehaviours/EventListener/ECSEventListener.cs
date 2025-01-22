using System;
using Radar.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Radar.Controllers
{
    public class ECSEventListener : MonoBehaviour
    {
        [SerializeField] private ScriptableObject _eventCondition;
        [SerializeField] private bool _desiredCondition = true;
        [SerializeField] private bool _isDisposable;
        
        private IECSBooleanProvider _booleanProvider;
        private bool _isDisposed;
        
        public UnityEvent OnWinConditionTriggered;

        private void Awake() => InitProvider();

        private void Update() => ConditionsCheckRoutine();
        
        private void InitProvider()
        {
            if (_eventCondition == null)
            {
                Debug.LogError("No Unity Event Condition Provided");
                return;
            }
            _booleanProvider = _eventCondition as IECSBooleanProvider;
            
            if (_booleanProvider == null)
            {
                Debug.LogError("Can't cast to IECSBooleanProvider");
                return;
            }
            _booleanProvider.Init();
        }
        
        private void ConditionsCheckRoutine()
        {
            if(_isDisposed || _booleanProvider.GetValue() != _desiredCondition) return;
            if (_isDisposable)
            {
                _isDisposed = true;
            }

            if (OnWinConditionTriggered != null)
            {
                OnWinConditionTriggered.Invoke();
            }
        }
    }
}