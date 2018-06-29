using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface  IEnemyActionObserver
{
    void Action();

    void Pause();

    void Init(EnemyController enemyController);
}

