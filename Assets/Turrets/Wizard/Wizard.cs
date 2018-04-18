using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour {

    [Header("Attributes")]
    public Transform target;
    public float range = 30f;
    public float fireRate = 10f;
    public float turnSpeed = 5f;
    public string enemyTag = "Enemy";

    private float fireCountDown = 0f;

    public GameObject fireballPrefab;

    [Header("Internal")]
    public Transform firePoint;
    public Transform partToRotate;
    //private Animator anim;

	void Start () {
        InvokeRepeating("UpdateTarget", 0f, 3f);
        //anim = GetComponent<Animator>();
	}
	
	void Update () {
		
        // Do nothing if no target
        if (target == null) {
            // Engage idle animation
            //anim.SetBool("isAttacking", false);
            return;
        }

        // Disengage idle animation
        //anim.SetBool("isAttacking", true);

        // Face target
        Vector3 directionToFace = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToFace);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;

        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        // Fire Control
        if (fireCountDown <= 0) {
            Shoot();
            fireCountDown = 1 / fireRate;
        }

        fireCountDown -= Time.deltaTime;

	}


    // Update the target
    void UpdateTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        // find shortest distance / nearest enemy
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies) {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range) {
            // establish target
            target = nearestEnemy.transform;
        } else {
            target = null;
        }
    }


    // gizmos to see range etc
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }


    void Shoot() {
        GameObject fireballGO = (GameObject)Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
        Fireball fireball = fireballGO.GetComponent<Fireball>();
        if (fireball != null) {
            fireball.Seek(target);
        }
    }
}
