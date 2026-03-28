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

    [Header("Spawn Timing")]
    public float mediumStartSecond = 10f; // Mediums start appearing at 10s
    public float hardStartSecond = 30f;   // Hards start appearing at 30s

    void SpawnEnemy()
    {
        // 1. Easy enemy stays very relevant. 
        // It starts at 100 and never drops below 40 (instead of 10).
        float easyWeight = Mathf.Max(40f, 100f - (gameTime * 0.3f));

        // 2. Medium enemy starts earlier and caps higher (70) so it 
        // stays more common than the Hard enemy.
        float mediumWeight = gameTime > mediumStartSecond
            ? Mathf.Min(70f, (gameTime - mediumStartSecond) * 1.5f)
            : 0f;

        // 3. Hard enemy starts appearing but is capped at 30.
        // This ensures they are "special" threats, not the entire army.
        float hardWeight = gameTime > hardStartSecond
            ? Mathf.Min(30f, (gameTime - hardStartSecond) * 0.5f)
            : 0f;

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
