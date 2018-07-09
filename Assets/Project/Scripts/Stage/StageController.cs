using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageController : MonoBehaviour
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

    public void Init()
    {
    
        _playerController = NrcGameManager.GetPlayerController();
        _playerController.transform.position = _playerPoint.transform.position;

        foreach (StageEnemySpawnController sesc in _stageEnemySpawnControllerList)
        {
            sesc.spawnEnemyEvent += (e) =>
            {

            };
        }
    }

    public void Pause()
    {
        StageEnemyTargetObserver.Destroy();

        foreach (StageEnemySpawnController sesc in _stageEnemySpawnControllerList)
        {
            sesc.Pause();
        }
    }

    public void Restart()
    {
        StageEnemyTargetObserver.ObserveAsync();

        foreach (StageEnemySpawnController sesc in _stageEnemySpawnControllerList)
        {
            sesc.Restart();
        }
    }
}
