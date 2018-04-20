using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour {

    [Header("Attributes")]
    public float speed = 70f;
    public int damage = 10;
    public int explosionRadius = 0;

    [Header("Internal Only")]
    private Transform target;
    public GameObject impactEffect;

    public void Seek(Transform _target) {
        target = _target;
    }

    void Start() {
    }

    public void TrackTarget() {
        if (target == null) {
            Destroy(gameObject);
            return;
        }

        // Get direction of bullet to go.
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        if (direction.magnitude <= distanceThisFrame) {
            HitTarget();
            return;
        }

        // Didnt hit - keep moving
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }


    // Hit the target
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

    void Damage(Transform enemy) {
        Enemy e = enemy.GetComponent<Enemy>();
        e.TakeDamage(damage);
        // Debug.Log("enemy: " + e);
    }


    // AE Damage
    void Explode() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders) {
            if (collider.tag == "Enemy") {
                // Its an enemy - destroy it
                Damage(collider.transform);
            } else {
                // Not an enemy
            }
        }

    }

}
