using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    #region Spawn Settings
    [Header("Spawn Settings")]
    public GameObject spawnPoint;
    [Space]
    public GameObject[] enemyPrefabs;
    public List<GameObject> enemies = new List<GameObject>();
    [Space]
    public float timeBetweenSpawns;
    public int enemyCount;
    int waveEnemiesSpawn;
    bool spawningWave;
    #endregion

    #region Wave Settings
    [Header("Wave Settings")]
    public int amountOfWaves;
    public int timeBetweenWaves;
    public int enemyGainPerWave;
    int currentWave;
    bool noMoreWaves;
    #endregion

    #region UI
    [Header("UI")]
    [Header("Countdown")]
    public GameObject waveCountdownUI; 
    public TMP_Text waveCountdownText; 
    float waveCountdown;

    [Header("Enemy Counter")]
    public TMP_Text enemyCounterText;

    [Header("Current Wave")]
    public TMP_Text currentWaveText;
    #endregion

    public void Update()
    {
        if (!noMoreWaves)
        {
            enemyCounterText.text = enemies.Count.ToString();
            if (enemies.Count == 0 && !spawningWave)
            {
                currentWave++;
                waveCountdown = timeBetweenWaves;

                waveEnemiesSpawn += enemyGainPerWave;
                StartCoroutine(SpawnEnemies(waveEnemiesSpawn));
            }
            if (waveCountdown > 0)
            {
                waveCountdownText.text = waveCountdown.ToString("N0");

                waveCountdown -= Time.deltaTime;
            }
        }
    }
    IEnumerator SpawnEnemies(int enemiesToSpawn)
    {
        if (currentWave <= amountOfWaves)
        {
            waveCountdownUI.SetActive(true);
            spawningWave = true;
            yield return new WaitForSeconds(timeBetweenWaves);

            waveCountdownUI.SetActive(false);
            currentWaveText.text = currentWave.ToString();

            for (int i = 0; i < enemiesToSpawn / enemyPrefabs.Length; i++)
            {
                Debug.Log(enemiesToSpawn);
                for (int e = 0; e < enemyPrefabs.Length; e++)
                {
                    GameObject enemy = Instantiate(enemyPrefabs[e], spawnPoint.transform.position, spawnPoint.transform.rotation);
                    enemies.Add(enemy);
                    yield return new WaitForSeconds(timeBetweenSpawns);
                }
            }
            spawningWave = false;
        }
        else
        {
            noMoreWaves = true;
        }
    }
}
