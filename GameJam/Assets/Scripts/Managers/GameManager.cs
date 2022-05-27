using System;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        public void LoseGame()
        {
            Debug.Log("You Lost Game.");
            
            //TODO : Show the Lose Panel;
        }

        public void WinGame()
        {
            Debug.Log("You Won Game");
            
            //TODO : Show the Win Panel;
        }

        public void ResetLevel()
        {
            
        }

        public void GoToNextLevel()
        {
            
        }
        
    }
}