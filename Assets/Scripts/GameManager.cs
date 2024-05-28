using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI scoreText;
    private int score;
    private int highScore;
    public GameObject joystick; // Referencia pública al objeto del joystick

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Snake3D")
        {
            SetupControls();
        }
    }

    private void SetupControls()
    {
        bool gyroscopeEnabled = PlayerPrefs.GetInt("GyroscopeEnabled", 0) == 1;
        
        SnakeController snakeController = FindObjectOfType<SnakeController>();
        SnakeControllerGyroscope snakeControllerGyroscope = FindObjectOfType<SnakeControllerGyroscope>();

        if (gyroscopeEnabled)
        {
            if (snakeController != null) snakeController.enabled = false;
            if (snakeControllerGyroscope != null) snakeControllerGyroscope.enabled = true;
            if (joystick != null) joystick.SetActive(false);
        }
        else
        {
            if (snakeController != null) snakeController.enabled = true;
            if (snakeControllerGyroscope != null) snakeControllerGyroscope.enabled = false;
            if (joystick != null) joystick.SetActive(true);
        }
    }

    // Method to increment the score
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    // Method to update the score text
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    // Method to handle game over
    public void GameOver()
    {
        // Check if the current score is higher than the high score
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        // Load the Dead scene
        SceneManager.LoadScene("Dead");
    }

    // Method to get the current score
    public int GetCurrentScore()
    {
        return score;
    }

    // Method to get the high score
    public int GetHighScore()
    {
        return highScore;
    }

    // Method to set scoreText (used when reloading scenes)
    public void SetScoreText(TextMeshProUGUI newScoreText)
    {
        scoreText = newScoreText;
        UpdateScoreText();
    }

    // Method to reset score
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }
}
