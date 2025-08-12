using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]
public struct SpawnData
{
    public GameObject EnemyToSpawn;
    public int NumberToSpawn;
    public float TimeBeforeSpawn;
    public Transform SpawnPoint;
    public Transform EndPoint;
}

[System.Serializable]
public struct WaveData
{
    public List<SpawnData> EnemyData;
    public float TimeBeforeWaves;
}
public class WaveManager : MonoBehaviour
{
    public List<WaveData> LevelWaveData;

    private void Start()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        StartCoroutine(StartWave());
    }

    IEnumerator SpawnEnemies(SpawnData spawnData)
    {
        for (int i = 0; i < spawnData.NumberToSpawn; i++)
        {
            SpawnEnemy(spawnData.EnemyToSpawn, spawnData.SpawnPoint, spawnData.EndPoint);
            yield return new WaitForSeconds(spawnData.TimeBeforeSpawn);
        }
    }
    IEnumerator StartWave()
    {
        foreach (WaveData currentWave in LevelWaveData)
        {
            yield return new WaitForSeconds(currentWave.TimeBeforeWaves);
            
            foreach (SpawnData currentEnemyToSpawn in currentWave.EnemyData)
            {
                yield return StartCoroutine(SpawnEnemies(currentEnemyToSpawn));
            }

        }

    }
    public void SpawnEnemy(GameObject enemyPrefab, Transform spawnPoint, Transform endPoint)
    {
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Enemy enemy = enemyInstance.GetComponent<Enemy>();
        enemy.Initialized(endPoint);
    }
}
