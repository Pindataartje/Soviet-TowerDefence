using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{
    NavMeshAgent agent;
    WaveSpawner waveSpawner;
    public GameObject checkpointParent;
    public List<Transform> checkPoints = new List<Transform>();
    int checkpointNumber;
    public Vector3 target;

    [Header("Enemy Stats")]
    public float enemyHealth;
    float currentHealth;
    public int killCash;
    public int BaseDamage;

    [Header("UI")]
    public Slider hpBar;

    GameObject buildmanager;
    BuildMenu buildmenu;

    void Start()
    {
        hpBar.maxValue = enemyHealth;
        hpBar.value = enemyHealth;
        currentHealth = enemyHealth;
        buildmanager = GameObject.FindGameObjectWithTag("BuildManager");
        buildmenu = buildmanager.GetComponent<BuildMenu>();


        agent = GetComponent<NavMeshAgent>();
        checkpointParent = GameObject.FindGameObjectWithTag("SpawnPoint");
        waveSpawner = checkpointParent.GetComponentInParent<WaveSpawner>();

        GetCheckpoints();
        UpdateCheckpoint();
    }
    #region AI Movement
    void GetCheckpoints()
    {
        foreach(Transform checkpoint in checkpointParent.transform)
        {
            checkPoints.Add(checkpoint);
        }
    }
    public void Update()
    {
        if(Vector3.Distance(transform.position, target) < 2)
        {
            UpdateCheckpoint();
            IterateCheckpointNumber();
        }
    }
    private void UpdateCheckpoint()
    {
        target = checkPoints[checkpointNumber].transform.position;
        agent.SetDestination(target);
        Debug.Log(Vector3.Distance(transform.position, target));

        Debug.Log("Updating Checkpoint");
    }
    private void IterateCheckpointNumber()
    {
        checkpointNumber++;
        if(checkpointNumber == checkPoints.Count)
        {
            waveSpawner.currentBaseHP -= BaseDamage;
            int thisGameobjectIndex = waveSpawner.enemies.IndexOf(gameObject);
            waveSpawner.enemies.RemoveAt(thisGameobjectIndex);
            Destroy(gameObject);
        }
    }
    #endregion
    public void EnemyTakeDamage(float damage)
    {
        currentHealth -= damage;
        hpBar.value = currentHealth;
        if(currentHealth <= 0)
        {
            buildmenu.currency += killCash;
            int thisGameobjectIndex = waveSpawner.enemies.IndexOf(gameObject);
            waveSpawner.enemies.RemoveAt(thisGameobjectIndex);
            Destroy(gameObject);
        }
    }
}
