using UnityEngine;
using System.Collections;

public class EnemyTargetActionObserver : INrcObserver
{
    private EnemyTargetController _enemyTargetController;

    private IEnemyTargetActionObserver _iEnemyTargetActionObserver;

    public EnemyTargetActionObserver(EnemyTargetController enemyTargetController)
    {
        _enemyTargetController = enemyTargetController;
        _iEnemyTargetActionObserver = EnemyTargetActionCreator.GetInstance(enemyTargetController.EnemyTargetModel.ActionId);
        _iEnemyTargetActionObserver.Init(enemyTargetController);
    }

    public void BeginningAsync()
    {
        _iEnemyTargetActionObserver.BeginningAsync();
    }

    public void Pause()
    {
        _iEnemyTargetActionObserver.Pause();
    }

}
