using UnityEngine;
using System.Collections;
using UniRx;

public class EnemyTargetCupsuleActionObserver : IEnemyTargetActionObserver
{
    private GameObject _explosion;

    private EnemyTargetController _enemyTargetController;

    public void Init(EnemyTargetController enemyTargetController)
    {
        _enemyTargetController = enemyTargetController;
        _explosion = NrcResourceManager.GetGameObject(ResourceDefine.PREFAB_EXPLOSION) as GameObject;

        _enemyTargetController.EnemyTargetDeadEvent += (etc) =>
        {
            Observable.FromCoroutine(observe => Explodison(_enemyTargetController.transform.position)).Subscribe();
        };
    }

    public void BeginningAsync()
    {

    }

    public void Pause()
    {
        
    }

    private IEnumerator Explodison(Vector3 position)
    {
        GameObject explosion = GameObject.Instantiate(_explosion);
        explosion.transform.position = position;

        yield return new WaitForSeconds(1);

        GameObject.Destroy(explosion);
    }
}
