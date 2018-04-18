using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    [Header("Attributes")]
    //public GameObject[] turretPrefabs;
    public GameObject buildEffect;

    [Header("Internal")]
    public static BuildManager instance;

    public TurretBlueprint turretToBuild;
    private Node selectedNode;

    // The Turret UI class
    public TurretUI turretUI;

    // Setup singleton
    void Awake() {
        instance = this;
    }

    void Start() {
        //turretToBuild = standardTurretPrefab;
    }

    /*
    public GameObject GetTurretToBuild() {
        return turretToBuild;
    }
    */

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.money >= turretToBuild.cost; } }


    // Deprecated function for turret building.  Working code is now right in node.
    public void BuildTurretOn(Node node) {
        //Instantiate(turretToBuild.prefab, node.transform.position + node.positionOffset, node.transform.rotation);

        if (PlayerStats.money < turretToBuild.cost) {
            // Not enough money to buy that turret
            return;
        }

        // Have enough money - ok to build.
        PlayerStats.money -= turretToBuild.cost;
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        GameObject be = (GameObject)Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(be, 2.0f);
        node.turret = turret;
    }

    public void SelectNode(Node node) {
        Debug.Log("selecting node");
        // hide UI if already there
        if (selectedNode == node) {
            DeselectNode();
            return;
        }


        // Selected a node to work on
        selectedNode = node;
        turretToBuild = null;

        // display the upgrade window
        turretUI.SetTarget(node);

    }

    public void SetTurretToBuild(TurretBlueprint turretBP) {
        turretToBuild = turretBP;
        Debug.Log("deselect node 2");
        DeselectNode();
    }


    public TurretBlueprint GetTurretToBuild() {
        return turretToBuild;
    }


    // Unselect node and hide the upgrade UI
    public void DeselectNode() {
        Debug.Log("deselect node 1");
        selectedNode = null;
        turretUI.Hide();
    }

}
