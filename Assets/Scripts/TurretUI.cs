using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretUI : MonoBehaviour {

    public GameObject popupUI;
    public Text upgradeCost;
    public Button upgradeButton;
    public Text turretNameLevel;
    public Text turretSellValue;

    private Node target;


    public void SetTarget(Node incomingTarget) {

        // this.target = incomingTarget;
        target = incomingTarget;
        transform.position = target.GetBuildPosition();

        // set the name regardless of other options
        string turretName = incomingTarget.turretBluePrint.turretName;
        if (incomingTarget.isUpgraded == false) {
            turretName += " Level 1";
        } else {
            turretName += "Level 2";
        }

        // Set the sell values
        string sellValue = incomingTarget.turretBluePrint.sellValue.ToString();
        turretSellValue.text = sellValue;

        turretNameLevel.text = turretName;

        // Show the UI
        if (target.isUpgraded == false) {
            upgradeCost.text = incomingTarget.turretBluePrint.upgradeCost.ToString();
            upgradeButton.interactable = true;
            popupUI.SetActive(true);
        } else {
            // Max upgrade already done
            upgradeCost.text = "Already Upgraded";
            upgradeButton.interactable = false;
            popupUI.SetActive(true);
        }
    }


    // Hide UI
    public void Hide() {
        popupUI.SetActive(false);
    }


    public void Upgrade() {
        Debug.Log("upgrade turret");
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();

    }

}
