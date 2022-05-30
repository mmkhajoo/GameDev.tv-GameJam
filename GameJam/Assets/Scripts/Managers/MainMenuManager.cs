using UnityEngine;

namespace Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        public void StartGame()
        {
            GameManager.instance.GoToNextLevel();
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}