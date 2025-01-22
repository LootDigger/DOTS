using Rader.Services;
using Unity.Entities;
using UnityEngine;

namespace Radar.Services
{
    public class InputManager : MonoBehaviour
    {
        private IInputSource _currentInputSource;
        private EntityManager _entityManager;
        private Entity _cursorPositionKeeper;

        private void Awake()
        {
            SetupInputSource();
            GetEntityManager();
            CreateCursorPositionKeeperEntity();
        }

        private void Update()
        {
            UpdateInputSource();
            UpdateCursorPositionComponent();
        }

        private void SetupInputSource()
        {
            #if UNITY_EDITOR || UNITY_STANDALONE
            _currentInputSource = new MouseInputSource(); 
            #endif
            
            _currentInputSource.Init();
        }
        
        private void GetEntityManager() => _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        private void CreateCursorPositionKeeperEntity()
        {
            _cursorPositionKeeper = _entityManager.CreateEntity();
            _entityManager.AddComponentData(_cursorPositionKeeper, new CusrorPositionDataComponent());
        }

        private void UpdateInputSource() => _currentInputSource.Update();

        private void UpdateCursorPositionComponent()
        {
            if (_currentInputSource == null)
            {
                Debug.LogError("Input Source for current platform isn't set");
                return;
            }
            _entityManager.SetComponentData(_cursorPositionKeeper, new CusrorPositionDataComponent()
            {
                WorldCursorPosition = _currentInputSource.CursorWorldPosition
            });
        }
    }
}