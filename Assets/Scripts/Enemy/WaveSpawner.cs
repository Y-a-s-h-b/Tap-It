using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public EnemyController[] enemies;
        public int count;
        public float timeBetweenSpawn;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenSpawns;
    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;

    private bool finishedSpawning;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBetweenSpawns);
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave(int index)
    {
        currentWave = waves[index];
        for (int i = 0; i < currentWave.count; i++)
        {
            if (player == null)
            {
                yield break;
            }
            EnemyController randomEnemy = currentWave.enemies[
                UnityEngine.Random.Range(0, currentWave.enemies.Length)
            ];
            Transform randomSpot = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);

            if (i == currentWave.count - 1)
            {
                finishedSpawning = true;
            }
            else
            {
                finishedSpawning = false;
            }

            yield return new WaitForSeconds(currentWave.timeBetweenSpawn);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (finishedSpawning == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            finishedSpawning = false;
            if (currentWaveIndex + 1 < waves.Length)
            {
                currentWaveIndex++;
                StartCoroutine(StartNextWave(currentWaveIndex));
            }
            else
            {
                Debug.Log("Game Over");
            }
        }
    }
}
