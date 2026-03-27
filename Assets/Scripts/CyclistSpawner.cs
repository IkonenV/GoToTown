using UnityEngine;

public class CyclistSpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject easyEnemyPrefab;
    public GameObject mediumEnemyPrefab;
    public GameObject hardEnemyPrefab;

    [Header("Spawn Settings")]
    public float initialSpawnRate = 3.0f;
    public float minSpawnRate = 0.5f;
    public float difficultyScalingSpeed = 0.01f; // How fast the spawn rate increases

    private float timer;
    private float gameTime;

    void Update()
    {
        gameTime += Time.deltaTime;

        // Calculate current spawn interval (gets faster over time)
        float currentInterval = Mathf.Max(minSpawnRate, initialSpawnRate - (gameTime * difficultyScalingSpeed));

        timer += Time.deltaTime;

        if (timer >= currentInterval)
        {
            SpawnEnemy();
            timer = 0;
        }
    }

    void SpawnEnemy()
    {
        // Define weights that change over time
        // Easy enemy weight decreases, but stays at a minimum of 10
        float easyWeight = Mathf.Max(10f, 100f - (gameTime * 0.5f));

        // Medium enemy starts appearing after 30s and peaks later
        float mediumWeight = gameTime > 30f ? Mathf.Min(50f, (gameTime - 30f) * 0.8f) : 0f;

        // Hard enemy starts appearing after 60s
        float hardWeight = gameTime > 60f ? Mathf.Min(40f, (gameTime - 60f) * 0.5f) : 0f;

        float totalWeight = easyWeight + mediumWeight + hardWeight;
        float randomValue = Random.Range(0, totalWeight);

        GameObject prefabToSpawn;

        if (randomValue < easyWeight)
        {
            prefabToSpawn = easyEnemyPrefab;
        }
        else if (randomValue < easyWeight + mediumWeight)
        {
            prefabToSpawn = mediumEnemyPrefab;
        }
        else
        {
            prefabToSpawn = hardEnemyPrefab;
        }

        Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
    }
}
