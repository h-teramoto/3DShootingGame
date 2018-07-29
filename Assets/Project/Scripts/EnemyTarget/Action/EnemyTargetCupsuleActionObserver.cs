using UnityEngine;
using System.Collections;
using UniRx;
using System;

public class EnemyTargetCupsuleActionObserver : IEnemyTargetActionObserver
{
    private GameObject _explosion;
    private GameObject _bullet;

    private EnemyTargetController _enemyTargetController;

    private IDisposable _iDisposable;

    public void Init(EnemyTargetController enemyTargetController)
    {
        _enemyTargetController = enemyTargetController;
        _explosion = NrcResourceManager.GetGameObject(ResourceDefine.PREFAB_EXPLOSION) as GameObject;
        _bullet = NrcResourceManager.GetGameObject(ResourceDefine.PREFAB_BULLET) as GameObject;

        _enemyTargetController.EnemyTargetDeadBeforeEvent += (etc) =>
        {
            Observable.FromCoroutine(observe => Explodison(_enemyTargetController.transform.position)).Subscribe();

        };
    }

    public void BeginningAsync()
    {
        _iDisposable = Observable.FromCoroutine(Coroutine).Subscribe();
    }

    private IEnumerator Coroutine()
    {
        while (true)
        {
            if (_enemyTargetController == null) yield  break;

            EnemyController enemyController = NrcGameManager.NrcGameEnemyService.GetMostNearEnemy(_enemyTargetController);
            if(enemyController != null)
            {
                float distance = (enemyController.transform.position - _enemyTargetController.transform.position).sqrMagnitude;

                Observable.FromCoroutine(Observable => ShootCoroutine(enemyController)).Subscribe();

                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
        }
    }

    private IEnumerator ShootCoroutine(EnemyController ec)
    {
        GameObject bullet = GameObject.Instantiate(_bullet);
        bullet.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        bullet.transform.position = _enemyTargetController.transform.position + new Vector3(0,1,0);

        NrcEventForwarder forwarder = bullet.GetComponent<NrcEventForwarder>();
        forwarder.OnTriggerEnterEvent += (col) =>
        {
            if (col.gameObject.tag == TagDefine.TAG_ENEMY)
            {
                EnemyController enemyController =
                    col.gameObject.GetComponent<EnemyController>();
                enemyController.Damage(5);

                GameObject.Destroy(bullet);
            }
        };

        Vector3 force;
        bullet.transform.LookAt(ec.transform);
        force = bullet.transform.forward * 100f;
        bullet.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);

        yield return new WaitForSeconds(2);

        GameObject.Destroy(bullet);

        yield return null;
    }


    public void Pause()
    {
        if (_iDisposable != null)
            _iDisposable.Dispose();
    }

    private IEnumerator Explodison(Vector3 position)
    {
        GameObject explosion = GameObject.Instantiate(_explosion);
        explosion.transform.position = position;

        yield return new WaitForSeconds(1);

        GameObject.Destroy(explosion);
    }
}
