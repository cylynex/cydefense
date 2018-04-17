using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour {

    private Transform target;

    [Header("Attributes")]
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Setup Fields Only")]
    public float turnSpeed = 10f;
    public string enemyTag = "Enemy";

    public GameObject level1BulletPrefab;
    public Transform firePoint;


    // rotation part
    public Transform partToRotate;

	void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}


    void Update() {
        // if no target, just exit
        if (target == null) {
            return;
        }

        // END - SOURCE -> to get the direction we should face and lock target
        Vector3 directonToFace = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directonToFace);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        // Fire Control
        if (fireCountdown <= 0) {
            Shoot();
            fireCountdown = 1 / fireRate;
        }

        fireCountdown -= Time.deltaTime;

    }
	

    void UpdateTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        // shortest distance / nearest enemy
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies) {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)  {
            // set target
            target = nearestEnemy.transform;

        } else {
            target = null;
        }
    }


    // gizmos to see range etc
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }


    // SHOOT!
    void Shoot() {
        GameObject bulletGO = (GameObject)Instantiate(level1BulletPrefab, firePoint.position, firePoint.rotation);
        BlasterBullet bullet = bulletGO.GetComponent<BlasterBullet>();
        if (bullet != null) {
            bullet.Seek(target);
        }
    }
}
