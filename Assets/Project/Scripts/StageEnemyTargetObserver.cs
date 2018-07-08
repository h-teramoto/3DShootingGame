using UnityEngine;
using System.Collections;
using System;
using UniRx;
using System.Collections.Generic;

public class StageEnemyTargetObserver
{
    private StageController _stageController;

    private IDisposable _iDisposable;

    private List<EnemyTargetController> _enemyTargetControllerList = new List<EnemyTargetController>();
    public List<EnemyTargetController> EnemyTargetControllerList { get { return _enemyTargetControllerList; } }

    public StageEnemyTargetObserver(StageController stageController)
    {
        _stageController = stageController;
         

    }

    public void ObserveAsync()
    {
        _enemyTargetControllerList = new List<EnemyTargetController>();
        _stageController = NrcGameManager.NrcGameStageService.GetNowStageController();
        _iDisposable = Observable.FromCoroutine(Coroutine).Subscribe();
    }

    public void Destroy()
    {
        if (_iDisposable != null)
            _iDisposable.Dispose();
    }

    private IEnumerator Coroutine()
    {
        Debug.Log(_stageController.EnemyTargetPointControllerList.Count);

        foreach (EnemyTargetPointController enemyTargetPointController in _stageController.EnemyTargetPointControllerList)
        {
            Debug.Log("EnemyTargetPointController");

            EnemyTargetModel enemyTargetModel = NrcGameManager.NrcGameDatabaseService.GetEnemyTargetModelById(enemyTargetPointController.Id);

            GameObject enemyTarget = GameObject.Instantiate(enemyTargetModel.Prefab,
                NrcGameManager.NrcGameStageService.GetNowStageController().transform);

            EnemyTargetController enemyTargetController = enemyTarget.GetComponent<EnemyTargetController>();
            enemyTargetController.transform.position = enemyTargetPointController.transform.position;
            enemyTargetController.Init(enemyTargetModel);

            _enemyTargetControllerList.Add(enemyTargetController);
        }

        yield return null;
    }
}
