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

    [SerializeField]
    private GameUIController _gameUIController;
    public GameUIController GameUIController { get { return _gameUIController; } }

    [SerializeField]
    private EnemyDataBase _enemyDataBase;
    public EnemyDataBase EnemyDataBase { get { return _enemyDataBase; } }

    [SerializeField]
    private StageDataBase _stageDataBase;
    public StageDataBase StageDataBase { get { return _stageDataBase; } }

    [SerializeField]
    private EnemyTargetDataBase _enemyTargetDataBase;
    public EnemyTargetDataBase EnemyTargetDataBase { get { return _enemyTargetDataBase; } }


    void Awake()
    {
        NrcGameManager.Init(this);
    }
}
