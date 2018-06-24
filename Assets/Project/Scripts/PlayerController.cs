using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    void Start () {

        new PlayerMouseMoveObserver(this).Observe();
        new PlayerShootObserver(this).Observe();
    }
	
}
