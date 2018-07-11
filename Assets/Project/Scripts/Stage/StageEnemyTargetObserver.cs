﻿using UnityEngine;
using System.Collections;
using System;
using UniRx;
using System.Collections.Generic;

public class StageEnemyTargetObserver : INrcObserver
{
    private StageController _stageController;

    private IDisposable _iDisposable;

    private List<EnemyTargetController> _enemyTargetControllerList = new List<EnemyTargetController>();
    public List<EnemyTargetController> EnemyTargetControllerList { get { return _enemyTargetControllerList; } }

    public StageEnemyTargetObserver(StageController stageController)
    {
        _stageController = stageController;
        _stageController = NrcGameManager.NrcGameStageService.GetNowStageController();

        foreach (EnemyTargetPointController enemyTargetPointController in _stageController.EnemyTargetPointControllerList)
        {
            EnemyTargetModel enemyTargetModel = NrcGameManager.NrcGameDatabaseService.GetEnemyTargetModelById(enemyTargetPointController.Id);

            GameObject enemyTarget = GameObject.Instantiate(enemyTargetModel.Prefab,
                NrcGameManager.NrcGameStageService.GetNowStageController().transform);

            EnemyTargetController enemyTargetController = enemyTarget.GetComponent<EnemyTargetController>();
            enemyTargetController.transform.position = enemyTargetPointController.transform.position;
            enemyTargetController.Init(enemyTargetModel);
            enemyTargetController.EnemyTargetDeadEvent += (etc) =>
            {
                _enemyTargetControllerList.Remove(etc);
                if(_enemyTargetControllerList.Count < 1)
                {
                    NrcGameManager.GameOver();
                }
            };

            _enemyTargetControllerList.Add(enemyTargetController);
        }
    }

    public void BeginningAsync()
    {
        foreach(EnemyTargetController enemyTargetController in _enemyTargetControllerList)
        {
            enemyTargetController.Beginning();
        }

        _iDisposable = Observable.FromCoroutine(Coroutine).Subscribe();
    }

    public void Pause()
    {
        foreach (EnemyTargetController enemyTargetController in _enemyTargetControllerList)
        {
            enemyTargetController.Pause();
        }

        if (_iDisposable != null)
            _iDisposable.Dispose();
    }

    private IEnumerator Coroutine()
    {
        while (true)
        {
            yield return null;
        }
    }
}
