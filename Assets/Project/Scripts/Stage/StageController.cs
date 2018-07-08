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
    private List<EnemyTargetController> _enemyTargetControllerList;
    public List<EnemyTargetController> EnemyTargetControllerList { get { return _enemyTargetControllerList; } }

    private PlayerController _playerController;

    public delegate void StageClearDelegate(StageController stageController);
    public StageClearDelegate stageClearEvent = delegate { };



    // Use this for initialization
    void Start()
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
        foreach (StageEnemySpawnController sesc in _stageEnemySpawnControllerList)
        {
            sesc.Pause();
        }
    }

    public void Restart()
    {
        foreach (StageEnemySpawnController sesc in _stageEnemySpawnControllerList)
        {
            sesc.Restart();
        }
    }
}
