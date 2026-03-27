using System.Collections;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    public Transform[] spawnPositions;
    public GameObject plane;
    public GameObject warning;

    [Header("Settings")]
    public float warningOffset = 2.0f;
    public float warningDuration = 1.0f;

    [Header("Random Spawn Timing")]
    public float minSpawnDelay = 1.5f;
    public float maxSpawnDelay = 4.0f;

    private float nextSpawnTime;

    void Start()
    {
        // Set the very first spawn time
        SetNextSpawn();
    }

    void Update()
    {
        // Check if it's time to spawn
        if (Time.time >= nextSpawnTime)
        {
            StartSpawnProcess();
            SetNextSpawn(); // Pick a new random time for the next one
        }
    }

    void SetNextSpawn()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnDelay, maxSpawnDelay);
    }

    void StartSpawnProcess()
    {
        if (spawnPositions.Length == 0) return;
        int index = Random.Range(0, spawnPositions.Length);
        StartCoroutine(SpawnSequence(index));
    }

    IEnumerator SpawnSequence(int index)
    {
        Vector3 spawnPos = spawnPositions[index].position;
        Vector3 warningPos = new Vector3(spawnPos.x - warningOffset, spawnPos.y, spawnPos.z);

        GameObject warnIcon = Instantiate(warning, warningPos, Quaternion.identity);
        yield return new WaitForSeconds(warningDuration);

        Destroy(warnIcon);
        Instantiate(plane, spawnPos, Quaternion.identity);
    }
}
