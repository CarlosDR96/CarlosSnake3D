using TMPro;
using UnityEngine;

public class Snake3DController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.SetScoreText(scoreText);
            GameManager.instance.ResetScore(); // Reset the score when the scene starts
        }

        // Aquí puedes agregar cualquier otra inicialización necesaria
    }
}
