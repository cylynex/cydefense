using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    [Header("Attributes")]
    public GameObject standardTurretPrefab;

    [Header("Internal")]
    public static BuildManager instance;

    private GameObject turretToBuild;

    void Awake() {
        instance = this;
    }

    void Start() {
        turretToBuild = standardTurretPrefab;
    }

    public GameObject GetTurretToBuild() {
        return turretToBuild;
    }

}
