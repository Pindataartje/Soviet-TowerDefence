using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class TowerAi : MonoBehaviour
{
    CapsuleCollider detectArea;
    public List<GameObject> targetsInArea = new List<GameObject>();

    [Header("Particles")]
    public GameObject particleParent;
    public ParticleSystem bullet;
    public GameObject radiusVisual;

    [Header("Stats")]
    public string towerName;
    public int cost;
    public float damage;
    public float fireSpeed;
    public float radius;
    [Space]
    public bool isBarbedWire;
    public float speedDecrease;
    [Space]
    public bool isMortar;
    public float mortarBulletAirTime;
    public ParticleSystem mortarBarrelParticle;
    public GameObject mortarBullet;
    bool mortarShooting;

    [Header("Runtime Only")]
    public bool activeTower;
    bool isShooting;
    
    void Start()
    {
        SetRadius();
        StartCoroutine(WaitForActivating(2));
    }
    #region Update
    private void Update()
    {
        if (!activeTower) return;

        if(targetsInArea.Count > 0)
        {
            if (targetsInArea[0] == null)
            {
                targetsInArea.RemoveAt(0);
            }
            if(!isBarbedWire && !isMortar && !isShooting)
            {
                StartCoroutine(FireRate(fireSpeed));
            }
            if (isMortar && !mortarShooting)
            {
                StartCoroutine(MortarBehavior(mortarBulletAirTime, fireSpeed));
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

    IEnumerator MortarBehavior(float airTime, float nextShot)
    {
        mortarShooting = true;

        mortarBarrelParticle.Play();
        mortarBullet.SetActive(true);

        yield return new WaitForSeconds(airTime);
        mortarBullet.SetActive(false);

        foreach(GameObject enemy in targetsInArea)
        {
            EnemyBehavior enemyBehavior = enemy.GetComponent<EnemyBehavior>();
            enemyBehavior.EnemyTakeDamage(damage);
            if (enemyBehavior.enemyHealth <= 0)
            {
                int destroyedEnemyInArea = targetsInArea.IndexOf(enemyBehavior.gameObject);
                targetsInArea.RemoveAt(destroyedEnemyInArea);
            }
        }

        yield return new WaitForSeconds(nextShot);
        mortarShooting = false;
    }
    #endregion
    #region SetRadius
    public void SetRadius()
    {
        detectArea = GetComponent<CapsuleCollider>();
        detectArea.radius = radius;

        //float detectAreaValue = detectArea.radius * 2;
        //radiusVisual.transform.localScale = new Vector3(detectAreaValue, detectAreaValue, detectAreaValue);
        //radiusVisual.transform.position = detectArea.center;
    }
    #endregion
    public void BulletParticle()
    {
        particleParent.transform.LookAt(targetsInArea[0].transform.position);
        bullet.Play();
    }
    public IEnumerator WaitForActivating(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        activeTower = true;
    }
}
