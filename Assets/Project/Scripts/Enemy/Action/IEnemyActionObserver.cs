using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface  IEnemyActionObserver : INrcObserver
{
    void Init(EnemyController enemyController);
}

