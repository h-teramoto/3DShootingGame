using UnityEngine;
using System.Collections;
using UniRx;

public class PlayerShootService 
{
    private PlayerController _playerController;

    private GameObject _bullet;

    private GameObject _explosion;

    private GameObject _startPoint;

    private GameObject _houdai;

    public PlayerShootService(PlayerController playerController)
    {
        _playerController = playerController;
        _bullet = Resources.Load("Prefabs/Bullet") as GameObject;
        _explosion = Resources.Load("Prefabs/Explosion") as GameObject;
        _houdai = _playerController.transform.Find("Houdai").gameObject;
        _startPoint = _houdai.transform.Find("Cylinder/StartPoint").gameObject;
    }

    public void Shoot()
    {
        Observable.FromCoroutine(Coroutine).Subscribe();
    }

    private IEnumerator Coroutine()
    {

        GameObject bullet =  GameObject.Instantiate(_bullet);
        bullet.transform.position = _startPoint.transform.position;

        NrcEventForwarder forwarder = bullet.GetComponent<NrcEventForwarder>();
        forwarder.OnTriggerEnterEvent += (col) =>
        {
            if(col.gameObject.tag == TagDefine.TAG_ENEMY) {

                EnemyController enemyController =
                    col.gameObject.GetComponent<EnemyController>();
                enemyController.Damage(0);

                GameObject explosion = GameObject.Instantiate(_explosion);
                explosion.transform.position = col.gameObject.transform.position;

                GameObject.Destroy(bullet);               
            }
        };

        Vector3 force;
        force = _houdai.transform.forward * 30f;
        bullet.GetComponent<Rigidbody>().AddForce(force,ForceMode.VelocityChange);

        yield return new WaitForSeconds(1);

        GameObject.Destroy(bullet);

        yield return null;
    }



}
