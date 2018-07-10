using UnityEngine;
using System.Collections;

public interface IEnemyTargetActionObserver { 

    void BeginningAsync();

    void Pause();

    void Init(EnemyTargetController enemyTargetController);
}
