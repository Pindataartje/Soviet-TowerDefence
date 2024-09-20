using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BunkerAi : MonoBehaviour
{
    SphereCollider detectArea;
    public List<GameObject> targetsInArea = new List<GameObject>();
    GameObject targetedEnemy;

    public GameObject shootingPoint;
    [Header("Stats")]
    public int damage;
    public float fireSpeed;
    public float radius;

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
            else
            {
                targetedEnemy = targetsInArea[0];
                shootingPoint.transform.LookAt(targetedEnemy.transform.position);
            }
            if(!isShooting)
            {
                StartCoroutine(FireRate(fireSpeed));
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
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            targetsInArea.Remove(other.gameObject);
        }
    }
    #endregion
    #region FireRate
    IEnumerator FireRate(float speed)
    {
        isShooting = true;
        ShootBullet();
        Debug.Log("shooting");
        yield return new WaitForSeconds(speed);
        isShooting = false;
    }
    #endregion
    #region ShootBullet
    public void ShootBullet()
    {
        Debug.Log("Shooting Bullet");
        RaycastHit hit;
        if(Physics.Raycast(shootingPoint.transform.position, shootingPoint.transform.forward, out hit, radius))
        {
            Debug.Log("raycast hit something");
            if (hit.transform.tag == "Enemy")
            {
                Debug.Log("Hit enemy");
                EnemyBehavior enemy = hit.transform.GetComponent<EnemyBehavior>();
                enemy.EnemyTakeDamage(damage);
                if (enemy.enemyHealth <= 0)
                {
                    int destroyedEnemyInArea = targetsInArea.IndexOf(hit.transform.gameObject);
                    targetsInArea.RemoveAt(destroyedEnemyInArea);
                }
            }
        }
    }
    #endregion
    #region SetRadius
    public void SetRadius()
    {
        detectArea = GetComponent<SphereCollider>();
        detectArea.radius = radius;
    }
    #endregion
}
