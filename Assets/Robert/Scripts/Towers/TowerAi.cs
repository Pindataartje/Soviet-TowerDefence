using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class TowerAi : MonoBehaviour
{
    SphereCollider detectArea;
    public List<GameObject> targetsInArea = new List<GameObject>();
    public GameObject particleParent;
    public ParticleSystem bullet;

    [Header("Stats")]
    public int cost;
    public float damage;
    public float fireSpeed;
    public float radius;
    [Space]
    public bool isBarbedWire;
    public float speedDecrease;


    bool isShooting;
    
    void Start()
    {
        SetRadius();
    }
    #region Update
    private void Update()
    {
        if(targetsInArea.Count > 0)
        {
            if (targetsInArea[0] == null)
            {
                targetsInArea.RemoveAt(0);
            }
            if(!isBarbedWire)
            {
                if (!isShooting)
                {
                    StartCoroutine(FireRate(fireSpeed));
                }
            }
        }
    }
    #endregion
    #region DetectEnemy
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enemy entered");
        if (other.tag == "Enemy")
        {
            Debug.Log("Target found");
            targetsInArea.Add(other.gameObject);
        }
        if (isBarbedWire)
        {
            foreach (GameObject target in targetsInArea)
            {
                NavMeshAgent enemy = target.GetComponent<NavMeshAgent>();
                enemy.speed -= speedDecrease;
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            targetsInArea.Remove(other.gameObject);
        }
        if (isBarbedWire)
        {
            NavMeshAgent enemy = other.GetComponent<NavMeshAgent>();
            enemy.speed += speedDecrease;
        }
    }
    #endregion
    #region FireRate
    IEnumerator FireRate(float speed)
    {
        isShooting = true;

        BulletParticle();
        EnemyBehavior enemy = targetsInArea[0].GetComponent<EnemyBehavior>();
        enemy.EnemyTakeDamage(damage);
        if (enemy.enemyHealth <= 0)
        {
            int destroyedEnemyInArea = targetsInArea.IndexOf(enemy.gameObject);
            targetsInArea.RemoveAt(destroyedEnemyInArea);
        }
        Debug.Log("shooting");

        yield return new WaitForSeconds(speed);
        isShooting = false;
    }
    #endregion
    #region SetRadius
    public void SetRadius()
    {
        detectArea = GetComponent<SphereCollider>();
        detectArea.radius = radius;
    }
    #endregion
    public void BulletParticle()
    {
        particleParent.transform.LookAt(targetsInArea[0].transform.position);
        bullet.Play();
    }
}
