using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        public enum GameState
        {
            StartGame,
            InGame,
            Lose,
            Win,
            Menu
        }

        public static GameController Instance { get; private set; }
        public static GameState CurrentGameState;
        
        private GameState _previousGameState;

        public void RestartLevel()
        {
            SaveController.Instance.LoadGameFromFile();
            SceneManager.LoadScene(1);
        }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Debug.Log("[ATTENTION] Multiple " + this + " found!");
                return;
            }

            if (SaveController.Instance.Save == null)
                SaveController.Instance.LoadGameFromFile();
            if (SaveController.Instance.Save == null)
                SaveController.Instance.SaveGameToFile();
            Application.targetFrameRate = 60;
            SceneManager.LoadScene(1);
            SetUpLoadedScene();
        }

        private void Update()
        {
            if (_previousGameState != CurrentGameState)
            {
                _previousGameState = CurrentGameState;
                switch (CurrentGameState)
                {
                    case GameState.StartGame:
                        Time.timeScale = 1;

                        break;
                    case GameState.Menu:
                        Time.timeScale = 0;
                        
                        break;
                    case GameState.Lose:
                        
                        break;
                    case GameState.Win:
                        WinLevel();

                        break;
                    case GameState.InGame:

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void SetUpLoadedScene()
        {
            _previousGameState = GameState.StartGame;
            CurrentGameState = GameState.StartGame;
            InitUI();
        }

        private void WinLevel()
        {
            SaveController.Instance.SaveGameToFile();
        }

        private void InitUI()
        {

        }
    }
}