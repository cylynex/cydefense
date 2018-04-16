using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    BuildManager buildManager;
    public TurretBlueprint[] turrets;

    void Start() {
        buildManager = BuildManager.instance;
    }

    public void SelectBlaster() {
        //buildManager.SetTurretToBuild(buildManager.turretPrefabs[0]);
        Debug.Log("pick blaster");
        buildManager.SetTurretToBuild(turrets[0]);
    }

    public void SelectRocketLauncher() {
        buildManager.SetTurretToBuild(turrets[1]);
    }

    public void SelectLaser() {
        buildManager.SetTurretToBuild(turrets[2]);
    }


}
