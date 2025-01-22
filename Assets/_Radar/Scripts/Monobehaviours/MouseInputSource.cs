using UnityEngine;

namespace Radar.Services
{
    public interface IInputSource
    {
        public void Init();
        public void Update();
        public Vector3 CursorWorldPosition { get; set; }
    }
    
    public class MouseInputSource : IInputSource
    {
       private Camera _camera;
       public Vector3 CursorWorldPosition { get; set; }

       public void Init() => _camera = Camera.main;
       public void Update() => RaycastRoutine();

       private void RaycastRoutine()
       {
           var mousePosition = Input.mousePosition;
           Ray ray = _camera.ScreenPointToRay(mousePosition);
           RaycastHit hit;

           if (Physics.Raycast(ray, out hit))
           {
               CursorWorldPosition = hit.point;
           }
       }
    }
}