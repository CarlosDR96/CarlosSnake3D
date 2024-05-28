using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeControllerGyroscope : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float bodySpeed;
    [SerializeField] private float steerSpeed;
    [SerializeField] private GameObject bodyPrefab;

    private int gap = 10;
    private List<GameObject> bodyParts = new List<GameObject>();
    private List<Vector3> positionHistory = new List<Vector3>();
    private bool appleEaten = false;

    void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            GrowSnake();
        }

        InvokeRepeating("UpdatePositionHistory", 0f, 0.01f);
    }

    void Update()
    {
        // Mueve hacia adelante
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        // Maneja el movimiento con el giroscopio
        float steerDirection = Input.acceleration.x; // Utiliza el eje X de la aceleración del dispositivo
        transform.Rotate(Vector3.up * steerDirection * steerSpeed * Time.deltaTime);

        int index = 0;
        foreach (GameObject body in bodyParts)
        {
            Vector3 point = positionHistory[Math.Min(index * gap, positionHistory.Count - 1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * bodySpeed * Time.deltaTime;

            body.transform.LookAt(point);

            index++;
        }

        CheckSelfCollision();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GrowSnake();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Apple") && !appleEaten)
        {
            appleEaten = true;
            EatApple();
            Destroy(other.gameObject);
        }

        // Colisión con muro
        if (other.CompareTag("Wall"))
        {
            LoseGame();
        }
    }

    void CheckSelfCollision()
    {
        // Obtener la posición de la cabeza del snake
        Vector3 headPosition = transform.position;

        // Iterar a través de las partes del cuerpo
        for (int i = 1; i < bodyParts.Count; i++)
        {
            // Verificar la distancia entre la cabeza y las partes del cuerpo
            float distance = Vector3.Distance(headPosition, bodyParts[i].transform.position);

            // Si la distancia es menor que cierto umbral (ajusta según sea necesario), el snake ha chocado consigo mismo
            if (distance < 0.1f)
            {
                // Llamada a la función de perder el juego
                LoseGame();
            }
        }
    }

    void LoseGame()
    {
        // Aquí puedes implementar la lógica para indicar que el jugador ha perdido el juego
        Debug.Log("Game Over");
        GameManager.instance.GameOver();
        // Por ejemplo, puedes desactivar el script SnakeController, mostrar un mensaje de Game Over, etc.
        enabled = false;
    }

    void EatApple()
    {
        // Verifica si el script está activo antes de ejecutar GrowSnake
        if (enabled)
        {
            GrowSnake();
        }

        // Llama a SpawnNewApple del GameController para generar una nueva manzana
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        if (gameControllerObject != null)
        {
            GameController gameController = gameControllerObject.GetComponent<GameController>();

            if (gameController != null)
            {
                gameController.SpawnNewApple();
            }
        }

        // Añade puntuación al comer una manzana
        GameManager.instance.AddScore(1); // Incrementa la puntuación en 1 punto, ajusta según sea necesario

        // Restablece el booleano después de un tiempo para permitir una nueva colisión
        StartCoroutine(ResetAppleEaten());
    }

    IEnumerator ResetAppleEaten()
    {
        yield return new WaitForSeconds(0.1f);
        appleEaten = false;
    }

    void UpdatePositionHistory()
    {
        // Añadir la posición actual al inicio de la lista
        positionHistory.Insert(0, transform.position);

        // Si la lista excede el número máximo de posiciones, elimina la última
        if (positionHistory.Count > 500)
        {
            positionHistory.RemoveAt(positionHistory.Count - 1);
        }
    }

    private void GrowSnake()
    {
        GameObject body = Instantiate(bodyPrefab);
        bodyParts.Add(body);
    }
}
