using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    [Header("Attributes")]
    //public GameObject standardTurretPrefab;
    //public GameObject rocketLauncherPrefab;
    public GameObject[] turretPrefabs;

    [Header("Internal")]
    public static BuildManager instance;

    private GameObject turretToBuild;

    // Setup singleton
    void Awake() {
        instance = this;
    }

    void Start() {
        //turretToBuild = standardTurretPrefab;
    }

    public GameObject GetTurretToBuild() {
        return turretToBuild;
    }


    public void SetTurretToBuild(GameObject turret) {
        turretToBuild = turret;
    }

}
