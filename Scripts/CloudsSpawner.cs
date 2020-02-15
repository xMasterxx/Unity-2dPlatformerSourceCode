using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsSpawner : MonoBehaviour
{
    public GameObject[] cloudPrefabs;
    public float spawnPosX = 95;
    private readonly float startDelay = 3;
    private readonly float spawnRate = 6f;
    public float firstRange = 11f;
    public float secondRange = 15.6f;

    void Start()
    {
        InvokeRepeating("SpawnCloud", startDelay, spawnRate);
    }

    void SpawnCloud()
    {

        var cloudIndex = Random.Range(0, cloudPrefabs.Length);
        var spawnPos = new Vector2(spawnPosX, Random.Range(firstRange, secondRange));
        Instantiate(cloudPrefabs[cloudIndex], spawnPos, cloudPrefabs[cloudIndex].transform.rotation);
    }
}
