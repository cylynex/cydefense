using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : BaseTurret {
    
    public int hitPoints = 100;

    void Start() {
        range = 7f;
        InvokeRepeating("UpdateTarget", 0f, 1f);
    }


    void Update() {
        ActiveTrack();

    }

}
