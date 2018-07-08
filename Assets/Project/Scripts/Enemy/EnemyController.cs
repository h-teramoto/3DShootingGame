using UnityEngine;
using System.Collections;
using UniRx;

public class EnemyController : MonoBehaviour
{
    private int _hp;
    public int Hp { get { return _hp; } }

    private int _maxHp;
    public int MaxHp { get { return _maxHp; } }

    private EnemyDamageService _enemyDamageService;
    public EnemyDamageService EnemyDamageService{
        get
        {
            if(_enemyDamageService == null)
            {
                _enemyDamageService = new EnemyDamageService(this);
            }
            return _enemyDamageService;
        }
    }

    private EnemyHpGaugeObserver _enemyHpGaugeObserver;
    public EnemyHpGaugeObserver EnemyHpGaugeObserver
    {
        get
        {
            if(_enemyHpGaugeObserver == null)
            {
                _enemyHpGaugeObserver = new EnemyHpGaugeObserver(this);
            }
            return _enemyHpGaugeObserver;
        }
    }

    private EnemyActionObserver _enemyMoveObserver;
    public EnemyActionObserver EnemyActionObserver
    {
        get
        {
            if(_enemyMoveObserver == null)
            {
                _enemyMoveObserver = new EnemyActionObserver(this);
            }
            return _enemyMoveObserver;
        }
    }


    public delegate void EnemyDeadDelegate(EnemyController enemyController);
    public EnemyDeadDelegate EnemyDeadEvent = delegate { };

    private EnemyModel _enemyModel;
    public EnemyModel EnemyModel { get { return _enemyModel; } }

    public void Init(EnemyModel enemyModel)
    {
        _enemyModel = enemyModel;

        _hp = enemyModel.Hp;

        _maxHp = _hp;

        EnemyDamageService.EnemyHpChangeEvent += (hp) =>
        {
            _hp = hp;
            if (hp == 0)
            {
                EnemyDeadEvent(this);
                NrcGameManager.NrcGameScoreService.ScoreUp(enemyModel.Score);

                Destroy(this.gameObject);
            }
        };
    
        EnemyHpGaugeObserver.DisplayAsync();
        EnemyActionObserver.Observe();
    }


    public void Damage(int point)
    {
        EnemyDamageService.Damage(point);
    }

    public void Pause()
    {

    }

    public void Restart()
    {

    }
}
