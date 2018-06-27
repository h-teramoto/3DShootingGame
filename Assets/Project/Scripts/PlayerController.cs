using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private GameObject _houdai;
    public GameObject Houdai { get { return _houdai; } }

    [SerializeField]
    private GameObject _startPoint;
    public GameObject StartPoint { get { return _startPoint; } }

    [SerializeField]
    private GameObject _laser;
    public GameObject Laser { get { return _laser; } }

    void Start () {
        new PlayerMouseMoveObserver(this).Observe();
        new PlayerShootObserver(this).Observe();
    }
}
