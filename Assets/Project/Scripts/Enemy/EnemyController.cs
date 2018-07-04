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
    private EnemyActionObserver _enemyMoveObserver;

    public delegate void EnemyDeadDelegate(EnemyController enemyController);
    public EnemyDeadDelegate EnemyDeadEvent = delegate { };

    private EnemyModel _enemyModel;
    public EnemyModel EnemyModel { get { return _enemyModel; } }

    public void Init(EnemyModel enemyModel)
    {
        _enemyModel = enemyModel;

        _hp = enemyModel.Hp;

        _maxHp = _hp;

        _enemyDamageService = new EnemyDamageService(this);
        _enemyDamageService.EnemyHpChangeEvent += (hp) =>
        {
            _hp = hp;
            if (hp == 0)
            {
                EnemyDeadEvent(this);
                NrcGameManager.NrcGameScoreService.ScoreUp(enemyModel.Score);

                Destroy(this.gameObject);
            }
        };

        _enemyHpGaugeObserver = new EnemyHpGaugeObserver(this, _enemyDamageService);
        _enemyHpGaugeObserver.DisplayAsync();
        
        _enemyMoveObserver = new EnemyActionObserver(this);
        _enemyMoveObserver.Observe();
    }


    public void Damage(int point)
    {
        _enemyDamageService.Damage(point);
    }

    public void Pause()
    {

    }

    public void Restart()
    {

    }
}
