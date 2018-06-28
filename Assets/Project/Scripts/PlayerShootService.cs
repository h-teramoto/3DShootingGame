using UnityEngine;
using System.Collections;
using UniRx;
using System;

public class PlayerShootService 
{
    private PlayerController _playerController;

    private GameObject _bullet;

    private IDisposable _iDisposable;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="playerController"></param>
    public PlayerShootService(PlayerController playerController)
    {
        _playerController = playerController;
        _bullet = NrcResourceManager.GetGameObject(ResourceDefine.PREFAB_BULLET) as GameObject;
    }

    public void Shoot()
    {
        _iDisposable = Observable.FromCoroutine(Coroutine).Subscribe();
    }

    public void Destroy()
    {
        _iDisposable.Dispose();
    }

    private IEnumerator Coroutine()
    {
        GameObject bullet =  GameObject.Instantiate(_bullet);
        bullet.transform.position = _playerController.StartPoint.transform.position;

        NrcEventForwarder forwarder = bullet.GetComponent<NrcEventForwarder>();
        forwarder.OnTriggerEnterEvent += (col) =>
        {
            if(col.gameObject.tag == TagDefine.TAG_ENEMY) {

                EnemyController enemyController =
                    col.gameObject.GetComponent<EnemyController>();
                enemyController.Damage(20);

                GameObject.Destroy(bullet);               
            }
        };

        Vector3 force;
        force = _playerController.Houdai.transform.forward * 30f;
        bullet.GetComponent<Rigidbody>().AddForce(force,ForceMode.VelocityChange);

        yield return new WaitForSeconds(1);

        GameObject.Destroy(bullet);

        yield return null;
    }



}
