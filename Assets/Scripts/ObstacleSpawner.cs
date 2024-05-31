using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;  // Prefab do Obstacle
    public GameObject followingObstaclePrefab;  // Prefab do FollowingObstacle

    public float spawnRate = 2.0f;
    public float maxXpos;
    private float initialSpawnRate;
    private int lastScoreCheck = 0;

    private int lvlUpscale = 15;

    // Defina a probabilidade (entre 0 e 1) de spawnar um FollowingObstacle
    [Range(0, 1)]
    public float followingObstacleSpawnChance = 0.3f;  // 30% de chance de spawnar um FollowingObstacle

    void Start()
    {
        initialSpawnRate = spawnRate;
        StartSpawning();
    }

    public void UpdateSpawnRate(int score)
    {
        if (score / lvlUpscale > lastScoreCheck / lvlUpscale)
        {
            spawnRate = Mathf.Max(spawnRate * 0.9f, 0.5f);  // Decrementa o spawnRate em 10%, não menor que 0.5s
            AdjustSpawnRate();
        }
        lastScoreCheck = score;
    }

    public void AdjustSpawnRate()
    {
        CancelInvoke("Spawn");
        InvokeRepeating("Spawn", 0.1f, spawnRate);
    }

    public void Spawn()
    {
        float randomX = Random.Range(-maxXpos, maxXpos);
        Vector2 spawnPos = new Vector2(randomX, transform.position.y);
        float randomValue = Random.Range(0f, 1f);
        GameObject selectedObstacle = Random.value < 0.3f ? followingObstaclePrefab : obstaclePrefab;
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
