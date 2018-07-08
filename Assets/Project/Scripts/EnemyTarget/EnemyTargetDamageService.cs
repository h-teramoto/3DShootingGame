using UnityEngine;
using System.Collections;

public class EnemyTargetDamageService
{
    private EnemyTargetController _enemyTargetController;

    public delegate void EnemyTargetHpChangeDelegate(int hp);
    public EnemyTargetHpChangeDelegate EnemyTargetHpChangeEvent = delegate { };

    public EnemyTargetDamageService(EnemyTargetController enemyTargetController)
    {
        _enemyTargetController = enemyTargetController;
    }

    public void Damage(int point)
    {
        int hp = _enemyTargetController.Hp;
        hp -= point;
        if (hp < 0)
        {
            hp = 0;
        }
        EnemyTargetHpChangeEvent(hp);
    }
}
