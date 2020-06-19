using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        public enum GameState
        {
            StartGame,
            Lose
        }

        public static GameController Instance { get; private set; }
        public static GameState CurrentGameState;
        public CannonsController cannonsController;
        public BallController ballController;
        public GameObject endGamePanel;
        public GameObject scorePanel;

        private GameState _previousGameState;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Debug.Log("[ATTENTION] Multiple " + this + " found!");
        }

        private void Start()
        {
            if (SaveController.Instance.Save == null)
                SaveController.Instance.LoadGameFromFile();
            if (SaveController.Instance.Save == null)
                SaveController.Instance.SaveGameToFile();
            Application.targetFrameRate = 60;
            SceneManager.sceneLoaded += SetUpLoadedScene;
            SceneManager.LoadScene(1);
        }

        private void Update()
        {
            if (CurrentGameState == GameState.Lose)
            {
                if (Input.GetKey(KeyCode.Return))
                {
                    SceneManager.LoadScene(1);
                    CurrentGameState = GameState.StartGame;
                }
            }
            if (_previousGameState != CurrentGameState)
            {
                _previousGameState = CurrentGameState;
                switch (CurrentGameState)
                {
                    case GameState.StartGame:
                        Time.timeScale = 1;
                        break;
                    case GameState.Lose:
                        Time.timeScale = 0;
                        Destroy(ballController);
                        Destroy(cannonsController);
                        scorePanel.SetActive(false);
                        endGamePanel.SetActive(true);
                        endGamePanel.GetComponentInChildren<Text>().text = 
                            "Game over.\nTotal score: " + cannonsController.points + "\n\nPress Enter to restart game.";
                        break;
                }
            }
        }

        private void SetUpLoadedScene(Scene arg0, LoadSceneMode loadSceneMode)
        {
            _previousGameState = GameState.StartGame;
            CurrentGameState = GameState.StartGame;
            InitUI();
        }
        
        private void InitUI()
        {
            scorePanel = GameObject.Find("Score");
            endGamePanel = GameObject.Find("EndGamePanel");
            endGamePanel.SetActive(false);
        }
    }
}