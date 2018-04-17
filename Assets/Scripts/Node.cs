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
        if (EventSystem.current.IsPointerOverGameObject()) {
            // Already a GO there.
            return;
        }

        if (buildManager.CanBuild == false) {
            // Debug.Log("space already occupied cant build here");
            return;
        }


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

        buildManager.DeselectNode();

        if (EventSystem.current.IsPointerOverGameObject())
            return;


        if (turret != null) {
            // Turret is already there
            Debug.Log("already a turret");
            buildManager.SelectNode(this);
            return;
        }


        if (!buildManager.CanBuild)
            return;


        // Build a Turret
        BuildTurret(buildManager.GetTurretToBuild());

        // Old build call using build manager
        //buildManager.BuildTurretOn(this);

    }


    public Vector3 GetBuildPosition() {
        return transform.position + positionOffset;
    }


    // Relocated build stuff from buildmanager
    void BuildTurret(TurretBlueprint blueprint) {
        if (PlayerStats.money < blueprint.cost) {
            // Not enough money to buy that turret
            return;
        }

        // Have enough money - ok to build.
        PlayerStats.money -= blueprint.cost;
        GameObject tempTurret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        GameObject be = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(be, 2.0f);
        turret = tempTurret;
    }
}
