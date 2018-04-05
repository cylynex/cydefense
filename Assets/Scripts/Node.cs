using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    [Header("Attributes")]
    public Color hoverColor;
    public Vector3 positionOffset;

    private Renderer rend;
    private Color startColor;
    private GameObject turret;

    BuildManager buildManager;

    void Start() {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }

	// hover animation
    void OnMouseEnter() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            // Already a GO there.
            return;
        }
        rend.material.color = hoverColor;
    }

    void OnMouseExit() {
        rend.material.color = startColor;
    }


    // Click node
    void OnMouseDown() {

        if (buildManager.GetTurretToBuild() == null) {
            Debug.Log("cant build without picking a turret");
            return;
        }

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
