using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;
using UniRx;

public class EnemyActionCubeObserver : IEnemyActionObserver
{
    private EnemyController _enemyController;
    private NavMeshAgent _navMeshAgent;
    private GameObject _explosion;

    private IDisposable _iDisposable;

    public void Init(EnemyController enemyController)
    {
        _enemyController = enemyController;
        _navMeshAgent = _enemyController.GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = 1;
        _explosion = NrcResourceManager.GetGameObject(ResourceDefine.PREFAB_EXPLOSION) as GameObject;
        _enemyController.EnemyDeadEvent += (e) =>
        {
            Observable.FromCoroutine(observe => Explodison(_enemyController.transform.position)).Subscribe();
            _iDisposable.Dispose();
        };
    }

    public void Action()
    {
        _iDisposable = Observable.FromCoroutine(Coroutine).Subscribe();
    }

    public void Pause()
    {
        if (_iDisposable != null)
            _iDisposable.Dispose();
    }

    private IEnumerator Coroutine()
    {
        EnemyTargetController etc = NrcGameManager.NrcGameEnemyService.GetMostNearEnemyTaget(_enemyController);
        _navMeshAgent.SetDestination(etc.transform.position);

        while (true)
        {
            //_enemyController.transform.Rotate(new Vector3(5, 6, 7));
            yield return null;
        }
    }

    private IEnumerator Explodison(Vector3 position)
    {
        GameObject explosion = GameObject.Instantiate(_explosion);
        explosion.transform.position = position;

        yield return new WaitForSeconds(1);

        GameObject.Destroy(explosion);
    }
}
