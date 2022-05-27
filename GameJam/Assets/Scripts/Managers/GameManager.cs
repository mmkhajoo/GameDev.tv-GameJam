using System;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public Transform playerDefaultParent;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        public void LoseGame()
        {
            //TODO : Show the Lose Panel;
        }

        public void WinGame()
        {
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