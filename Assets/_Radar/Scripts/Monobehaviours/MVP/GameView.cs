using Radar.UI;
using UnityEngine;

namespace Radar.Controllers
{
    public interface IGameView
    {
        public void ShowWinScreen();
        public void HideWinScreen();
    }

    public class GameView : MonoBehaviour, IGameView
    {
        [SerializeField] private BaseScreen _winScreen;
        private BaseScreen _currentScreen;

        void Start() => HideWinScreen();
        
        public void ShowScreen(BaseScreen screen, bool isAdditive = false)
        {
            if(!isAdditive && _currentScreen != null)
                _currentScreen.Hide();
            
            _currentScreen = screen;
            _currentScreen.Show();
        }

        public void HideScreen(BaseScreen screen) => screen.Hide();

        public void ShowWinScreen() => ShowScreen(_winScreen);

        public void HideWinScreen() => HideScreen(_winScreen);
    }
}