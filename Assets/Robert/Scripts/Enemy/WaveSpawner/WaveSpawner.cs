using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public Slider baseHPSlider;
    [Space]
    public int maxBaseHP;
    public int currentBaseHP;

    [Header("Countdown")]
    public TMP_Text waveCountdownText; 
    float waveCountdown;

    [Header("Enemy Counter")]
    public TMP_Text enemyCounterText;

    [Header("Current Wave")]
    public TMP_Text currentWaveText;

    GameObject winScreen;
    GameObject loseScreen;
    #endregion

    private void Start()
    {
        winScreen = GameObject.FindGameObjectWithTag("Win");
        loseScreen = GameObject.FindGameObjectWithTag("Lose");

        winScreen.SetActive(false);
        loseScreen.SetActive(false);

        baseHPSlider.maxValue = maxBaseHP;
        currentBaseHP = maxBaseHP;
    }
    public void Update()
    {
        baseHPSlider.value = currentBaseHP;

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
        if(noMoreWaves && enemies.Count == 0)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        if(currentBaseHP <= 0)
        {
            loseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    IEnumerator SpawnEnemies(int enemiesToSpawn)
    {
        if (currentWave <= amountOfWaves)
        {
            spawningWave = true;
            yield return new WaitForSeconds(timeBetweenWaves);

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
    public void GoBackToMenuTime()
    {
        Time.timeScale = 1f;
    }
}
