using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour {

	// face target
    public void FaceTarget(Transform target, float turnSpeed) {
        /*
        Vector3 directonToFace = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directonToFace);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        return transform.rotation;
        */
    }
}
