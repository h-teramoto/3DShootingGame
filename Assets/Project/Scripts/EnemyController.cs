using UnityEngine;
using System.Collections;
using UniRx;

public class EnemyController : MonoBehaviour
{
    private int _hp;
    public int Hp { get { return _hp; }}

    private int _maxHp;
    public int MaxHp { get { return _maxHp; }}

    private EnemyDamageService _enemyDamageService;

    private EnemyHpGaugeObserver _enemyHpGaugeObserver;

    public delegate void EnemyDeadDelegate(EnemyController enemyController);
    public EnemyDeadDelegate EnemyDeadEvent = delegate { };

    private GameObject _explosion;

    private EnemyModel _enemyModel;


    private IEnumerator Explodison(Vector3 position)
    {
        GameObject explosion = GameObject.Instantiate(_explosion);
        explosion.transform.position = position;

        yield return new WaitForSeconds(1);

        GameObject.Destroy(explosion);
    }

    void Start()
    {
        _hp = 1000;
        _maxHp = 1000;
        _enemyDamageService = new EnemyDamageService(this);
        _enemyHpGaugeObserver = new EnemyHpGaugeObserver(this, _enemyDamageService);
        _enemyHpGaugeObserver.Display();
        _explosion = NrcResourceManager.GetGameObject(ResourceDefine.PREFAB_EXPLOSION) as GameObject;

        _enemyDamageService.EnemyHpChangeEvent += (hp) =>
        {
            _hp = hp;
            if (hp == 0)
            {
                EnemyDeadEvent(this);
                Observable.FromCoroutine(observe => Explodison(this.gameObject.transform.position)).Subscribe();
                Destroy(this.gameObject);
            }
        };

        
    }

    public void Damage(int point)
    {
        _enemyDamageService.Damage(point);
    }

    void Update()
    {
        transform.Rotate(new Vector3(2, 1, 5));
    }

}
