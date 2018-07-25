using UnityEngine;
using System.Collections;

public class EnemyTargetController : MonoBehaviour, INrcController
{
    private int _hp;
    public int Hp { get { return _hp; } }

    private int _maxHp;
    public int MaxHp { get { return _maxHp; } }

    private EnemyTargetDamageService _enemyTargetDamageService;
    public EnemyTargetDamageService EnemyTargetDamageService
    {
        get
        {
            if (_enemyTargetDamageService == null)
            {
                _enemyTargetDamageService = new EnemyTargetDamageService(this);
            }
            return _enemyTargetDamageService;
        }
    }

    private EnemyTargetHpGaugeObserver _enemyTargetHpGaugeObserver;
    public EnemyTargetHpGaugeObserver EnemyTargetHpGaugeObserver
    {
        get
        {
            if (_enemyTargetHpGaugeObserver == null)
            {
                _enemyTargetHpGaugeObserver = new EnemyTargetHpGaugeObserver(this);
            }
            return _enemyTargetHpGaugeObserver;
        }
    }

    private EnemyTargetActionObserver _enemyTargetActionObserver;
    public EnemyTargetActionObserver EnemyTargetActionObserver
    {
        get
        {
            if(_enemyTargetActionObserver == null)
            {
                _enemyTargetActionObserver = new EnemyTargetActionObserver(this);
            }
            return _enemyTargetActionObserver;
        }
    }

    private EnemyTargetModel _enemyTargetModel;
    public EnemyTargetModel EnemyTargetModel { get { return _enemyTargetModel; } }

    public delegate void EnemyTargetDeadDelegate(EnemyTargetController enemyTargetController);
    public EnemyTargetDeadDelegate EnemyTargetDeadEvent = delegate { };

    public void Init(EnemyTargetModel enemyTargetModel)
    {
        _enemyTargetModel = enemyTargetModel;

        _hp = EnemyTargetModel.Hp;

        _maxHp = _hp;

        EnemyTargetDamageService.EnemyTargetHpChangeEvent += (hp) =>
        {
            _hp = hp;
            if (hp == 0)
            {
                if(this.gameObject != null)
                {
                    EnemyTargetDeadEvent(this);
                    Destroy(this.gameObject);
                }

            }
        };

    }

    public void Damage(int point)
    {
        EnemyTargetDamageService.Damage(point);
    }

    public void Pause()
    {
        EnemyTargetHpGaugeObserver.Pause();
        EnemyTargetActionObserver.Pause();
    }

    public void Beginning()
    {
        EnemyTargetHpGaugeObserver.BeginningAsync();
        EnemyTargetActionObserver.BeginningAsync();
    }
}
