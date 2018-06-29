using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyActionObserver
{
    private EnemyController _enemyController;

    private IEnemyActionObserver _iEnemyActionObserver;

    public EnemyActionObserver(EnemyController enemyController)
    {
        _enemyController = enemyController;
        _iEnemyActionObserver = EnemyActionCreator.GetInstance(_enemyController.EnemyModel.ActionId);
        _iEnemyActionObserver.Init(_enemyController);
    }

    public void Observe()
    {
        _iEnemyActionObserver.Action();
    }

    public void Pause()
    {
        _iEnemyActionObserver.Pause();
    }
}
