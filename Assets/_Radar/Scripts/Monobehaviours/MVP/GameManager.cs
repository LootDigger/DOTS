using Radar.Services;
using UnityEngine;

namespace Radar.Controllers
{
    public class GameManager : MonoBehaviour
    {
       private IGameView _gameView;

       private void Awake() => ConnectGameView();

       private void ConnectGameView()
       {
           _gameView = ServiceLocator.GetService<IGameView>();
       }

       public void OnGameWinConditionMatched()
       {
           _gameView.ShowWinScreen(); 
       }
    }
}