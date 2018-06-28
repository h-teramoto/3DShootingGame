using UnityEngine;
using System.Collections;
using System;
using UniRx;
using System.Collections.Generic;

public class StageEnemySpawnObserver
{
    private StageEnemySpawnController _stageEnemySpawnController;

    private List<EnemyController> _enemyList = new List<EnemyController>();

    private IDisposable _iDisposable;

    public StageEnemySpawnObserver(StageEnemySpawnController stageEnemySpawnController)
    {
        _stageEnemySpawnController = stageEnemySpawnController;
    }

    public void ObserveAsync()
    {    
        _iDisposable = Observable.FromCoroutine(Coroutine).Subscribe();
    }

    public void Destroy()
    {
        if (_iDisposable != null)
            _iDisposable.Dispose();
    }

    private void Spawn()
    {
        GameObject enemy = GameObject.Instantiate(_stageEnemySpawnController.EnemyModel.Prefab);
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        _enemyList.Add(enemyController);

        enemyController.EnemyDeadEvent += (e) =>
        {
            _enemyList.Remove(e);
        };
    }

    private IEnumerator Coroutine()
    {
        while (true)
        {
            if (_enemyList.Count < _stageEnemySpawnController.MaxSpawnCount)
            {
                Spawn();
            }

            yield return new WaitForSeconds(_stageEnemySpawnController.SpawnTimespan);
        }
    }
}
