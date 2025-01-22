using UnityEngine;

namespace Radar.Controllers
{
    public class GameManager : MonoBehaviour
    {
       [SerializeField] private GameObject _gameViewGameObject;
       private IGameView _gameView;

       private void Awake() => InitGameView();

       private void InitGameView()
       {
           _gameView = _gameViewGameObject.GetComponent<IGameView>();
       }

       public void OnGameWinConditionMatched()
       {
           _gameView.ShowWinScreen(); 
       }
    }
}