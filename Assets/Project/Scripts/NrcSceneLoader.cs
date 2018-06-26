using UnityEngine;
using System.Collections;

public class NrcSceneLoader : MonoBehaviour
{
    [SerializeField]
    private Camera _mainCamera;
    public Camera MainCamera { get { return _mainCamera; } }

    [SerializeField]
    private Camera _firstPersonCamera;
    public Camera FirstPersonCamera { get { return _firstPersonCamera; } }

    [SerializeField]
    private PlayerController _playerController;
    public PlayerController PlayerController { get { return _playerController; } }

    // Use this for initialization
    void Awake()
    {
        NrcGameManager.Init(this);
    }

}
