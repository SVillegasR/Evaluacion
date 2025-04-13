using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemigoPrefab;
    public Vector3[] spawnPoints; // Lugares posibles para respawn

    public void SpawnEnemigo()
    {
        int index = Random.Range(0, spawnPoints.Length); // Elegir un punto al azar
        Instantiate(enemigoPrefab, spawnPoints[index], Quaternion.identity);
    }
}
