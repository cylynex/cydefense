using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterBullet : MonoBehaviour {

    [Header("Attributes")]
    public float speed = 70f;

    [Header("Internal Only")]
    private Transform target;
    public GameObject impactEffect;

    public void Seek(Transform _target) {
        target = _target;
    }

	void Start () {
		
	}
	
	void Update () {
        if (target == null) {
            Destroy(gameObject);
            return;
        }

        Debug.Log("starting move calcs");
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
        Destroy(effectInstance, 2.0f);
        Destroy(gameObject);
        Destroy(target.gameObject);
    }
}
