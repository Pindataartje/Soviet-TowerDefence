using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkerAi : MonoBehaviour
{
    SphereCollider detectArea;
    WaveSpawner waveSpawner;
    GameObject checkpointParent;
    public List<GameObject> targetsInArea = new List<GameObject>();
    GameObject targetedEnemy;
    [Header("Stats")]
    public int damage;
    public float fireSpeed;
    public float radius;

    bool isShooting;
    
    void Start()
    {
        detectArea = GetComponent<SphereCollider>();
        detectArea.radius = radius;
        checkpointParent = GameObject.FindGameObjectWithTag("SpawnPoint");
        waveSpawner = checkpointParent.GetComponentInParent<WaveSpawner>();
    }
    private void Update()
    {
        if(targetsInArea.Count > 0)
        {
            targetedEnemy = targetsInArea[0];
            transform.LookAt(targetedEnemy.transform.position);
            if(!isShooting)
            {
                StartCoroutine(FireRate(fireSpeed));
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enemy entered");
        if (other.tag == "Enemy")
        {
            Debug.Log("Target found");
            targetsInArea.Add(other.gameObject);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            targetsInArea.Remove(other.gameObject);
        }
    }
    IEnumerator FireRate(float speed)
    {
        isShooting = true;
        ShootBullet();
        Debug.Log("shooting");
        yield return new WaitForSeconds(speed);
        isShooting = false;
    }
    public void ShootBullet()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, radius))
        {
            if(hit.transform.tag == "Enemy")
            {
                Debug.Log("Hit enemy");
                hit.transform.GetComponent<EnemyBehavior>().EnemyTakeDamage(damage);
            }
        }
    }
}
