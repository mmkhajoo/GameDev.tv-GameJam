using UnityEngine;

namespace Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        public void StartGame()
        {
            GameManager.instance.LoadLevel();
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}