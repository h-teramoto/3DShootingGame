using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StageController : MonoBehaviour, INrcController
{
    [SerializeField]
    private GameObject _playerPoint;

    [SerializeField]
    private List<StageEnemySpawnController> _stageEnemySpawnControllerList;

    [SerializeField]
    private List<EnemyTargetPointController> _enemyTargetPointControllerList;
    public List<EnemyTargetPointController> EnemyTargetPointControllerList { get { return _enemyTargetPointControllerList; } }

    private PlayerController _playerController;

    public delegate void StageClearDelegate(StageController stageController);
    public StageClearDelegate stageClearEvent = delegate { };

    private StageEnemyTargetObserver _stageEnemyTargetObserver;
    public StageEnemyTargetObserver StageEnemyTargetObserver
    {
        get
        {
            if(_stageEnemyTargetObserver == null)
            {
                _stageEnemyTargetObserver = new StageEnemyTargetObserver(this);
            }
            return _stageEnemyTargetObserver;
        }
    }

    public void Awake()
    {
        NrcGameManager.NrcGameStageService.SetNowStageController(this);
    }

    public void Init()
    {
        _playerController = NrcGameManager.GetPlayerController();
        _playerController.transform.position = _playerPoint.transform.position;        
    }

    public void Pause()
    {
        StageEnemyTargetObserver.Pause();
        foreach (StageEnemySpawnController sesc in _stageEnemySpawnControllerList)
        {
            sesc.Pause();
        }
    }

    public void Beginning()
    {
        StageEnemyTargetObserver.BeginningAsync();
        foreach (StageEnemySpawnController sesc in _stageEnemySpawnControllerList)
        {
            sesc.Beginning();
        }
    }
}
