using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour {

    public Transform target;
    public float range = 15f;
    public string enemyTag = "Enemy";

	void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}


    void Update() {
        // if no target, just exit
        if (target == null) {
            return;
        }
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
}
