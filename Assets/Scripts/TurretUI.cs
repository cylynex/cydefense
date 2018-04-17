using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUI : MonoBehaviour {

    private Node target;


    public void SetTarget(Node incomingTarget) {
        // this.target = incomingTarget;
        target = incomingTarget;
        transform.position = target.GetBuildPosition();
    }

}
