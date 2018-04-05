using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    [Header("Attributes")]
    public Color hoverColor;
    public Vector3 positionOffset;

    private Renderer rend;
    private Color startColor;
    private GameObject turret;

    void Start() {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

	// hover animation
    void OnMouseEnter() {
        rend.material.color = hoverColor;
    }

    void OnMouseExit() {
        rend.material.color = startColor;
    }


    // Click node
    void OnMouseDown() {
        if (turret != null) {
            // Turret is already there
            Debug.Log("space already occupied");
            return;
        }

        // Build a Turret
        GameObject turreToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turreToBuild, transform.position + positionOffset, transform.rotation);

    }
}
