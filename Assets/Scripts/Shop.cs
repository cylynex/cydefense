using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    BuildManager buildManager;


    void Start() {
        buildManager = BuildManager.instance;
    }

    public void PurchaseBlaster() {
        Debug.Log("buy blaster");
        buildManager.SetTurretToBuild(buildManager.turretPrefabs[0]);
    }

    public void PurchaseRocketLauncher() {
        Debug.Log("buy rocket launcher");
        buildManager.SetTurretToBuild(buildManager.turretPrefabs[1]);
    }


}
