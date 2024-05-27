using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;  // Prefab do Obstacle
    public GameObject followingObstaclePrefab;  // Prefab do FollowingObstacle

    public float spawnRate;
    public float maxXpos;

    // Defina a probabilidade (entre 0 e 1) de spawnar um FollowingObstacle
    [Range(0, 1)]
    public float followingObstacleSpawnChance = 0.3f;  // 30% de chance de spawnar um FollowingObstacle

    void Start()
    {
        StartSpawning();
    }

    public void Spawn()
    {
        float randomX = Random.Range(-maxXpos, maxXpos);
        Vector2 spawnPos = new Vector2(randomX, transform.position.y);

        // Gera um n�mero aleat�rio entre 0 e 1
        float randomValue = Random.Range(0f, 1f);

        // Decide qual prefab instanciar com base na chance definida
        GameObject selectedObstacle = (randomValue < followingObstacleSpawnChance) ? followingObstaclePrefab : obstaclePrefab;

        Instantiate(selectedObstacle, spawnPos, Quaternion.identity);
    }

    public void StartSpawning()
    {
        InvokeRepeating("Spawn", 1f, spawnRate);
    }

    public void StopSpawning()
    {
        CancelInvoke("Spawn");
    }
}
