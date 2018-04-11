using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    [Header("Attributes")]
    public Color hoverColor;
    public Color hoverColorBad;
    public Vector3 positionOffset;

    private Renderer rend;
    private Color startColor;
    [Header("Optional")]
    public GameObject turret;

    BuildManager buildManager;

    void Start() {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }


	// hover animation
    void OnMouseEnter() {
        Debug.Log("nouseover");
        if (EventSystem.current.IsPointerOverGameObject()) {
            // Already a GO there.
            return;
        }

        if (!buildManager.CanBuild)
            return;

        if (PlayerStats.money < buildManager.turretToBuild.cost) {
            rend.material.color = hoverColorBad;
        } else {
            rend.material.color = hoverColor;
        }

    }

    void OnMouseExit() {
        rend.material.color = startColor;
    }


    // Click node
    void OnMouseDown() {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (turret != null) {
            // Turret is already there
            return;
        }

        // Build a Turret
        buildManager.BuildTurretOn(this);

    }


    public Vector3 GetBuildPosition() {
        return transform.position + positionOffset;
    }
}
