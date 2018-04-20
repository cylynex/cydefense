using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurret : MonoBehaviour {

    public Transform target;

    [Header("Setup Fields Only")]
    public string enemyTag = "Enemy";

    [Header("Fire Control")]
    public GameObject damagePrefab;
    public Transform firePoint;
    public Transform partToRotate;

    [Header("Attributes")]
    public float turnSpeed = 10f;
    public float range = 15f;
    public float fireRate = 10f;
    public float fireCountdown = 0f;

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
            Debug.Log("turret target is " + target);
        } else {
            target = null;
        }
    }


    public void ActiveTrack() {
        Debug.Log("active tracking");
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


    // gizmos to see range etc
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }


    // SHOOT!
    public void Shoot() {
        GameObject damageGO = (GameObject)Instantiate(damagePrefab, firePoint.position, firePoint.rotation);
        Debug.Log("created " + damageGO);
        MeleeDamage dmg = damageGO.GetComponent<MeleeDamage>();
        if (dmg != null) {
            Debug.Log("seeking target: " + target);
            dmg.Seek(target);
        }
    }
}
