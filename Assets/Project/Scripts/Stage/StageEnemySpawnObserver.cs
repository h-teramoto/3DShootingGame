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

    private BoxCollider _areaBoxCollider;

    public delegate void SpawnEnemyDelegate(EnemyController enemyController);
    public SpawnEnemyDelegate spawnEnemyEvent = delegate { };


    public StageEnemySpawnObserver(StageEnemySpawnController stageEnemySpawnController)
    {
        _stageEnemySpawnController = stageEnemySpawnController;
        _areaBoxCollider = stageEnemySpawnController.GetComponent<BoxCollider>();
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

    private Vector3 GetRandamSpawnPosition()
    {
        float xsize = _areaBoxCollider.size.x;
        float ysize = _areaBoxCollider.size.y;
        float zsize = _areaBoxCollider.size.z;
        float x = _stageEnemySpawnController.transform.position.x;
        float y = _stageEnemySpawnController.transform.position.y;
        float z = _stageEnemySpawnController.transform.position.z;
        return new Vector3(UnityEngine.Random.Range(x - (xsize / 2), x + (xsize / 2)),
            UnityEngine.Random.Range(y - (ysize / 2), y + (ysize / 2)),
            UnityEngine.Random.Range(z - (zsize / 2), z + (zsize / 2)));
    }

    private void Spawn()
    {
        GameObject enemy = GameObject.Instantiate(_stageEnemySpawnController.EnemyModel.Prefab, 
            NrcGameManager.NrcGameStageService.GetNowStageController().transform);

        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.transform.position = this.GetRandamSpawnPosition();
        enemyController.Init(_stageEnemySpawnController.EnemyModel);

        spawnEnemyEvent(enemyController);
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
