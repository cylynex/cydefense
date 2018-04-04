using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 25f;

    private Transform target;
    private int wayPointIndex = 0;

	// Use this for initialization
	void Start () {
        target = Waypoints.points[0];
	}
	
	// Update is called once per frame
	void Update () {

        // Pick direction and move
        Vector3 direction = target.transform.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f) {
            GetNextWaypoint();
        }
	}


    // Get next waypoint
    void GetNextWaypoint() {
        if (wayPointIndex >= Waypoints.points.Length - 1) {
            // got to the end
            Destroy(gameObject);
            return;
        } else {
            // go to next WP
            wayPointIndex++;
            target = Waypoints.points[wayPointIndex];
        }
    }
}
