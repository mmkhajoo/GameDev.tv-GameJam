using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [SerializeField] private GameObject _panel;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            
            DontDestroyOnLoad(gameObject);

            if (!PlayerPrefs.HasKey("Level"))
            {
                PlayerPrefs.SetInt("Level",1);
            }
            
            LoadLevel();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                _panel.SetActive(!_panel.activeInHierarchy);
            }
        }

        public void LoseGame()
        {
            Debug.Log("You Lost Game.");
              
            Invoke("LoadLevel",0.5f);
            
            //TODO : Show the Lose Panel;
        }

        public void WinGame()
        {
            Debug.Log("You Won Game");
            
            IncreaseLevel();
            
            LoadLevel();
            
            //TODO : Show the Win Panel;
        }

        public void ResetLevel()
        {
            LoadLevel();
        }

        public void GoToNextLevel()
        {
            IncreaseLevel();
            
            LoadLevel();
        }

        public void GoPreviousLevel()
        {
            DecreaseLevel();
            
            LoadLevel();
        }

        private void IncreaseLevel()
        {
            PlayerPrefs.SetInt("Level",PlayerPrefs.GetInt("Level") + 1);
        }

        private void DecreaseLevel()
        {
            PlayerPrefs.SetInt("Level",PlayerPrefs.GetInt("Level") - 1);
        }

        private void LoadLevel()
        {
            int level = PlayerPrefs.GetInt("Level",1);

            if (level > 3)
            {
                PlayerPrefs.SetInt("Level", 3);
                level = 3;
            }
            else if(level < 1)
            {
                PlayerPrefs.SetInt("Level", 1);
                level = 1;
            }
            
            SceneManager.LoadScene("Level " + level);
        }
        
    }
}