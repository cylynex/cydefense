using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUI : MonoBehaviour {

    public GameObject popupUI;

    private Node target;


    public void SetTarget(Node incomingTarget) {
        // this.target = incomingTarget;
        target = incomingTarget;
        transform.position = target.GetBuildPosition();

        // Show the UI
        Debug.Log("show UI");
        popupUI.SetActive(true);

    }


    // Hide UI
    public void Hide() {
        Debug.Log("hide UI");
        popupUI.SetActive(false);
    }

}
