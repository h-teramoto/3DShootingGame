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

    private PlayerMouseMoveObserver _playerMouseMoveObserver;
    private PlayerMouseMoveObserver PlayerMouseMoveObserver
    {
        get
        {
            if(_playerMouseMoveObserver == null)
            {
                _playerMouseMoveObserver = new PlayerMouseMoveObserver(this);
            }
            return _playerMouseMoveObserver;
        }
    }

    private PlayerShootObserver _playerShootObserver;
    private PlayerShootObserver PlayerShootObserver
    {
        get
        {
            if(_playerShootObserver == null)
            {
                _playerShootObserver = new PlayerShootObserver(this);
            }
            return _playerShootObserver;
        }
    }

    private PlayerShootService _playerShootService;
    public PlayerShootService PlayerShootService
    {
        get
        {
            if(_playerShootService == null)
            {
                _playerShootService = new PlayerShootService(this);
            }
            return _playerShootService;
        }
    }

    public void Pause()
    {
        PlayerMouseMoveObserver.Destroy();
        PlayerShootObserver.Destroy();
    }

    public void Restart()
    {
        PlayerMouseMoveObserver.ObserveAsync();
        PlayerShootObserver.ObserveAsync();
    }
}

