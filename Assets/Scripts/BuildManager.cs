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

    public void SetTurretToBuild(TurretBlueprint turretBP) {
        turretToBuild = turretBP;
    }


    public void BuildTurretOn(Node node) {
        //Instantiate(turretToBuild.prefab, node.transform.position + node.positionOffset, node.transform.rotation);
        Debug.Log("player has this much money: " + PlayerStats.money);

        if (PlayerStats.money < turretToBuild.cost) {
            Debug.Log("Not enough money to buy that turret");
            return;
        }

        // Have enough money - ok to build.
        PlayerStats.money -= turretToBuild.cost;
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        GameObject be = (GameObject)Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(be, 2.0f);
        node.turret = turret;
    }

}
