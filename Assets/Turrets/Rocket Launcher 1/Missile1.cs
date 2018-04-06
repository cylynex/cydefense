using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile1 : MonoBehaviour {

    [Header("Attributes")]
    public float speed = 35f;

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

	}


    public void Seek(Transform _target) {
        target = _target;
    }


    // Hit
    void HitTarget() {
        Debug.Log("direct hit");
        //GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        //Destroy(effectInstance, 3.0f); 
        Destroy(gameObject);
        Destroy(target.gameObject);
    }
}
