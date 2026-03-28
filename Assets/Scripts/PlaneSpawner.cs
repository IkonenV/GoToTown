using System.Collections;
using UnityEngine;

public class PlaneSpawner : MonoBehaviour
{
    public Transform[] spawnPositions;
    public GameObject plane;
    public GameObject warning;

    [Header("Settings")]
    public float warningOffset = 2.0f;
    public float minWarningDuration = 0.2f;
    public float maxWarningDuration = 1.0f;

    [Header("Random Spawn Timing")]
    public float minSpawnDelay = 1.5f;
    public float maxSpawnDelay = 4.0f;

    [Header("Double Spawn Settings")]
    [Range(0, 1)]
    public float doubleSpawnChance = 0.2f; // 20% chance for two planes

    private float nextSpawnTime;

    void Start()
    {
        SetNextSpawn();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            StartSpawnProcess();
            SetNextSpawn();
        }
    }

    void SetNextSpawn()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnDelay, maxSpawnDelay);
    }

    void StartSpawnProcess()
    {
        if (spawnPositions.Length == 0) return;

        // Roll the dice: should we spawn two?
        bool isDoubleSpawn = Random.value < doubleSpawnChance && spawnPositions.Length >= 2;

        if (isDoubleSpawn)
        {
            // Pick two UNIQUE indices
            int firstIndex = Random.Range(0, spawnPositions.Length);
            int secondIndex = Random.Range(0, spawnPositions.Length);

            // Make sure the second index isn't the same as the first
            while (secondIndex == firstIndex)
            {
                secondIndex = Random.Range(0, spawnPositions.Length);
            }

            StartCoroutine(SpawnSequence(firstIndex));
            StartCoroutine(SpawnSequence(secondIndex));
        }
        else
        {
            // Just the normal single spawn
            int index = Random.Range(0, spawnPositions.Length);
            StartCoroutine(SpawnSequence(index));
        }
    }

    IEnumerator SpawnSequence(int index)
    {
        Vector3 spawnPos = spawnPositions[index].position;
        Vector3 warningPos = new Vector3(spawnPos.x - warningOffset, spawnPos.y, spawnPos.z);
        float warningDuration = Random.Range(minWarningDuration, maxWarningDuration);

        GameObject warnIcon = Instantiate(warning, warningPos, Quaternion.identity);
        yield return new WaitForSeconds(warningDuration);

        Destroy(warnIcon);
        Instantiate(plane, spawnPos, Quaternion.identity);
    }
}
