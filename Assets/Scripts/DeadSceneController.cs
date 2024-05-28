using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeadSceneController : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;
    public Button restartButton;
    public Button menuButton;

    void Start()
    {
        int currentScore = GameManager.instance.GetCurrentScore();
        int highScore = GameManager.instance.GetHighScore();

        currentScoreText.text = "Score: " + currentScore;
        highScoreText.text = "High Score: " + highScore;

        // Add listeners to the buttons
        restartButton.onClick.AddListener(RestartGame);
        menuButton.onClick.AddListener(GoToMenu);
    }

    void RestartGame()
    {
        // Load the Snake3D scene
        SceneManager.LoadScene("Snake3D");
    }

    void GoToMenu()
    {
        // Load the Menu scene
        SceneManager.LoadScene("Menu");
    }
}
