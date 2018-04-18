using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    [Header("Attributes")]
    public float startSpeed = 5f;
    public float speed;
    public float turnSpeed = 25f;
    public float startHitPoints = 100f;
    public float hitPoints;
    public int moneyGain = 10;
    public GameObject deathEffect;

    [Header("Internal")]
    public Image hpBar;

    private Transform target;
    private int wayPointIndex = 0;
    private Utility utility;
    private PlayerStats playerstats;
    private bool isSlowed = false;

	// Use this for initialization
	void Start () {
        target = Waypoints.points[0];
        playerstats = FindObjectOfType<PlayerStats>();
        speed = startSpeed;
        hitPoints = startHitPoints;
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

        speed = startSpeed;
	}


    // Get next waypoint
    void GetNextWaypoint() {
        if (wayPointIndex >= Waypoints.points.Length - 1) {
            // got to the end
            Destroy(gameObject);

            // Take a player point (abstract method using object) - DO NOT DELETE reference item
            // PlayerStats.lives--;
            // playerstats.SetPlayerStat(playerstats.livesUIObject);

            // Update lives / check for end (contained)
            playerstats.SubtractLife();

            return;

        } else {
            // go to next WP
            wayPointIndex++;
            target = Waypoints.points[wayPointIndex];

        }
    }


    // Hurt enemy
    public void TakeDamage(float damage) {
        hitPoints -= damage;
        if (hitPoints <= 0) {
            Die();
        }

        // Get % hp remaining
        float percentHitPoints = (hitPoints / startHitPoints);
        hpBar.fillAmount = percentHitPoints;

    }


    // Slow Enemy
    public void Slow(float slowAmount) {
        speed = startSpeed * (1f - slowAmount);
    }


    void Die() {
        PlayerStats.money += moneyGain;
        playerstats.UpdateMoney();

        Destroy(gameObject);
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2.0f); 

    }
}
