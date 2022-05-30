using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        [SerializeField] private GameObject _panel;

        [SerializeField] private TextMeshProUGUI _gemText;

        public int CurrentLevel => PlayerPrefs.GetInt("Level", 1);


        private const String Level = "Level";

        private MapManager _mapManager;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            DontDestroyOnLoad(gameObject);

            if (!PlayerPrefs.HasKey("Level"))
            {
                PlayerPrefs.SetInt("Level", 1);
            }

            LoadLevel();
        }

        private void Start()
        {
            _mapManager = GetComponent<MapManager>();
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                _panel.SetActive(!_panel.activeInHierarchy);
            }
#endif
            
            _gemText.SetText(PlayerPrefs.GetInt("Gems").ToString());
        }

        public void LoseGame()
        {
            Debug.Log("You Lost Game.");

            if (PlayerPrefs.HasKey("Checkpoint"))
            {
                if (PlayerPrefs.HasKey("DeadMan"))
                {
                    _mapManager.DeadManGoNextLevel(() =>
                    {
                        if (Check_Checkpoint())
                            return;

                        Invoke("LoadLevel", 0.5f);
                    });
                }
                else
                {
                    _mapManager.FirstTimeShowMap(() => { Invoke("LoadLevel", 0.5f); });
                }

                return;
            }

            Invoke("LoadLevel", 0.5f);

            //TODO : Show the Lose Panel;
        }

        private bool Check_Checkpoint()
        {
            if (PlayerPrefs.GetInt("DeadMan") == CurrentLevel)
            {
                _mapManager.ResetToLastCheckPoint();

                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Checkpoint"));

                _mapManager.FirstTimeShowMap(() => { Invoke("LoadLevel", 0.5f); });

                return true;
            }

            return false;
        }

        public void WinGame()
        {
            Debug.Log("You Won Game");

            if (PlayerPrefs.HasKey("Checkpoint"))
            {
                _mapManager.PlayerSetPosition();
            }

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
            PlayerPrefs.SetInt(Level, PlayerPrefs.GetInt(Level) + 1);
        }

        private void DecreaseLevel()
        {
            PlayerPrefs.SetInt(Level, PlayerPrefs.GetInt(Level) - 1);
        }

        private void LoadLevel()
        {
            int level = PlayerPrefs.GetInt(Level, 1);

            if (level > 3)
            {
                PlayerPrefs.SetInt(Level, 3);
                level = 3;
            }
            else if (level < 1)
            {
                PlayerPrefs.SetInt(Level, 1);
                level = 1;
            }

            SceneManager.LoadScene(level);
        }
    }
}