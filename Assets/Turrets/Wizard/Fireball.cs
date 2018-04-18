using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    [Header("Attributes")]
    public float speed = 20f;
    public float explosionRadius = 0f;
    public int damage = 50;

    [Header("Internal Only")]
    private Transform target;
    public GameObject impactEffect;

    void Update () {
        if (target == null) {
            Destroy(gameObject);
            return;
        }

        // Make it mooooove
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        if (direction.magnitude <= distanceThisFrame) {
            // HIT!
            HitTarget();
            return;
        }

        // Didn't hit, move along
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

	}


    public void Seek(Transform _target) {
        target = _target;
    }


    // Hit
    void HitTarget() {
        GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 5.0f); 

        // Explosion
        if (explosionRadius > 0f) {
            Explode();
        } else {
            Damage(target);
        }

        Destroy(gameObject);
    }

    // AE Damage
    void Explode() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius); 
        foreach(Collider collider in colliders) {
            if (collider.tag == "Enemy") {
                // Its an enemy - destroy it
                Damage(collider.transform);
            } else {
                // Not an enemy
            }
        }

    }

    void Damage(Transform enemy) {
        Enemy e = enemy.GetComponent<Enemy>();
        e.TakeDamage(damage);
    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
