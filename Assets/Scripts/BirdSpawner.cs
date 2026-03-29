using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    [Header("Settings")]
    public GameObject birdPrefab;
    public float spawnInterval = 2.0f;
    public float minY = -4.0f;
    public float maxY = 4.0f;
    public float scoreFrom;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnBird();
            timer = 0;
        }
    }

    void SpawnBird()
    {
        // Calculate a random Y position within your range
        float randomY = Random.Range(minY, maxY);

        int side = Random.Range(0, 2);
        if (side == 0)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, randomY, transform.position.y);
            Instantiate(birdPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Vector3 spawnPos = new Vector3(transform.position.x - 23f, randomY, transform.position.y);
            GameObject bird = Instantiate(birdPrefab, spawnPos, Quaternion.identity);
            CollectBird collect = bird.GetComponent<CollectBird>();
            collect.goingLeft = false;
        }
    }
}
