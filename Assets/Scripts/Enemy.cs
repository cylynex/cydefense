using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Attributes")]
    public float speed = 5f;
    public float turnSpeed = 25f;

    private Transform target;
    private int wayPointIndex = 0;
    private Utility utility;

	// Use this for initialization
	void Start () {
        target = Waypoints.points[0];
        //utility = new Utility();
	}
	
	// Update is called once per frame
	void Update () {

        // Turn
        Vector3 directonToFace = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directonToFace);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

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
