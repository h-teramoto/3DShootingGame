using UnityEngine;
using System.Collections;

public class EnemyDamageService
{
    private EnemyController _enemyController;

    public delegate void EnemyHpChangeDelegate(int hp);
    public EnemyHpChangeDelegate EnemyHpChangeEvent = delegate { };

    public EnemyDamageService(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public void Damage(int point)
    {
        int hp = _enemyController.Hp;
        hp -= point;
        if(hp < 0)
        {
            hp = 0;
        }
        EnemyHpChangeEvent(hp);
    }

}
