using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    [Header("Inspect Only")]
    public Transform target;
    public Enemy targetEnemy;

    [Header("Attributes")]
    public float range = 20f;
    public float fireRate = 10f;
    public float turnSpeed = 1f;
    public string enemyTag = "Enemy";
    public ParticleSystem impactEffect;
    public int damageOverTime = 5;
    public float slowAmount = 0.5f;
    public float aeRadius = 0f;

    private float fireCountDown = 0f;

    [Header("Laser Stuff")]
    public LineRenderer linerenderer;

    [Header("Heal Stuff")]
    public float healAmount = 0.1f;

    public GameObject laserPrefab;

    [Header("Internal")]
    public Transform firePoint;
    public Transform partToRotate;
    private Animator anim;

	void Start () {
        InvokeRepeating("UpdateTarget", 0f, 1f);
        anim = GetComponent<Animator>();
	}
	
	void Update () {
		
        // Do nothing if no target
        if (target == null) {

            // Disengage laser
            linerenderer.enabled = false;

            // Disable particle effect
            impactEffect.Stop();

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
        Shoot();
       
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
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
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

        // Determine if its AE or not and act appropriately
        if (aeRadius > 0f) {
            LaserMultipleTarget(target);
        } else {
            LaserSingleTarget(); 
        }


        // Do a Heal
        //Heal();

        // enable laser
        if (linerenderer.enabled == false) {
            linerenderer.enabled = true;

            // enable impact particles
            impactEffect.Play();

        }

        // set start and end of beam
        linerenderer.SetPosition(0, firePoint.position);
        linerenderer.SetPosition(1, target.position);

        // get direction to face the particles back at laser
        Vector3 dir = firePoint.position - target.position;
        impactEffect.transform.position = target.position + dir.normalized * 0.5f; // *.5f
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);

        /*
        GameObject bulletGO = (GameObject)Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        Missile1 bullet = bulletGO.GetComponent<Missile1>();
        if (bullet != null) {
            bullet.Seek(target);
        }
        */
    }


    // Single Target Laser Effect
    void LaserSingleTarget() {
        
        // Do the actual work
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);

        // Do a slow
        targetEnemy.Slow(slowAmount);
    }


    // Multiple Target Effect
    void LaserMultipleTarget(Transform target) {
        Debug.Log("hitting many now but doing nothing yet cause no script");

        Collider[] colliders = Physics.OverlapSphere(target.transform.position, aeRadius);
        foreach (Collider collider in colliders) {
            if (collider.tag == "Enemy") {
                // Its an enemy - dmg it
                Debug.Log("found an enemy");
                targetEnemy = collider.GetComponent<Enemy>();
                LaserSingleTarget();
            } else {
                // Not an enemy
            }
        }

    }

    // Heal Enemy
    public void Heal() {
        if (targetEnemy.hitPoints < targetEnemy.startHitPoints) {
            targetEnemy.hitPoints = targetEnemy.hitPoints + healAmount;
            Debug.Log("adding: " + targetEnemy.hitPoints + " + " + healAmount);
        }
    }

}
