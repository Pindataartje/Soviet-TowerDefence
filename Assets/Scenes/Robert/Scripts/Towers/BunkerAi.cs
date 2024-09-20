using System.Collections;
using System.Collections.Generic;
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
    private void Update()
    {
        if(targetsInArea.Count > 0)
        {
            targetedEnemy = targetsInArea[0];
            shootingPoint.transform.LookAt(targetedEnemy.transform.position);
            if(!isShooting)
            {
                StartCoroutine(FireRate(fireSpeed));
            }
        }
        for (int i = 0; i < targetsInArea.Count; i++)
        {
            if (targetsInArea[i] == null)
            {
                targetsInArea.RemoveAt(i);
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
                if(enemy.enemyHealth <= 0)
                {
                    int destroyedEnemyInArea = targetsInArea.IndexOf(hit.transform.gameObject);
                    targetsInArea.RemoveAt(destroyedEnemyInArea);
                }
            }
        }
    }
    public void SetRadius()
    {
        detectArea = GetComponent<SphereCollider>();
        detectArea.radius = radius;
        //detectArea = GetComponentInChildren<BoxCollider>();

        //float centerX = -radius / 2;

        //Vector3 startCenterRadius = detectArea.center;
        //detectArea.center = new Vector3(centerX - 2, startCenterRadius.y, startCenterRadius.z);

        //Vector3 startsizeRadius = detectArea.size;
        //detectArea.size = new Vector3(radius, radius, radius * 1.5f);
    }
}
