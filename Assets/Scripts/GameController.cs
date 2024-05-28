using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class GameController : MonoBehaviour
{
    public GameObject applePrefab;
    public int numberOfApples = 3;

    void Start()
    {
        SpawnInitialApples();
    }

    private void SpawnInitialApples()
    {
        for (int i = 0; i < numberOfApples; i++)
        {
            SpawnNewApple();
        }
    }

    public void SpawnNewApple()
    {
        float randomX = Random.Range(-10f, 10f); // Ajusta el rango según tus necesidades
        float randomZ = Random.Range(-10f, 10f);

        Vector3 spawnPosition = new Vector3(randomX, 0.5f, randomZ); // Ajusta la altura según tus necesidades

        Instantiate(applePrefab, spawnPosition, Quaternion.identity);
    }
}