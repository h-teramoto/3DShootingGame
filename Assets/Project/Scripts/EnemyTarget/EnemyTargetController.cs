using UnityEngine;
using System.Collections;

public class EnemyTargetController : MonoBehaviour
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

    private EnemyTargetModel _enemyTargetModel;
    public EnemyTargetModel EnemyTargetModel { get { return _enemyTargetModel; } }


    public void Init(EnemyTargetModel enemyTargetModel)
    {
        _enemyTargetModel = enemyTargetModel;

        _hp = EnemyTargetModel.Hp;

        _maxHp = _hp;

        EnemyTargetHpGaugeObserver.DisplayAsync();

    }

    public void Damage(int point)
    {
        EnemyTargetDamageService.Damage(point);
    }

    public void Pause()
    {

    }

    public void Restart()
    {

    }
}
