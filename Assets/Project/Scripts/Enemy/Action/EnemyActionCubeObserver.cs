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
        _navMeshAgent.speed = 1.5f;
        _explosion = NrcResourceManager.GetGameObject(ResourceDefine.PREFAB_EXPLOSION) as GameObject;
        _enemyController.EnemyDeadEvent += (e) =>
        {
            Observable.FromCoroutine(observe => Explodison(_enemyController.transform.position)).Subscribe();
            _iDisposable.Dispose();
        };

        NrcEventForwarder nrcEventForwarder = enemyController.GetComponent<NrcEventForwarder>();
        nrcEventForwarder.OnTriggerStayEvent += (col) =>
        {

            if(col.gameObject.tag == TagDefine.TAG_ENEMY_TARGET)
            {
                EnemyTargetController enemyTargetController = col.gameObject.GetComponent<EnemyTargetController>();
                enemyTargetController.Damage(1);
            }
        };

        EnemyTargetController etc = NrcGameManager.NrcGameEnemyTargetService.GetMostNearEnemyTaget(_enemyController);
        if (etc != null)
        {
            etc.EnemyTargetDeadEvent += (enemyTarget) =>
            {
                etc = NrcGameManager.NrcGameEnemyTargetService.GetMostNearEnemyTaget(_enemyController);

                if(etc != null)
                    _navMeshAgent.SetDestination(etc.transform.position);
            };

            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(etc.transform.position);
        }

    }

    public void BeginningAsync()
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
