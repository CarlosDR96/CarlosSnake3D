using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public Button gameButton;
    public Toggle gyroscopeToggle;

    void Start()
    {
        // Obtener la puntuación más alta de PlayerPrefs
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;

        // Add listeners to the buttons
        if (gameButton != null)
        {
            gameButton.onClick.AddListener(GoToGame);
        }

        // Add listener to the toggle
        if (gyroscopeToggle != null)
        {
            gyroscopeToggle.onValueChanged.AddListener(OnGyroscopeToggleChanged);
            // Set the toggle state based on PlayerPrefs
            gyroscopeToggle.isOn = PlayerPrefs.GetInt("GyroscopeEnabled", 0) == 1;
        }
    }

    void GoToGame()
    {
        // Load the Snake3D scene
        SceneManager.LoadScene("Snake3D");
    }

    void OnGyroscopeToggleChanged(bool isOn)
    {
        // Save the toggle state in PlayerPrefs
        PlayerPrefs.SetInt("GyroscopeEnabled", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}
