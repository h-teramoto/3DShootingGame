using UnityEngine;
using System.Collections;

public class EnemyTargetActionCreator 
{
    public static IEnemyTargetActionObserver GetInstance(EnemyTargetActionDefine.ACTION_ID id)
    {
        IEnemyTargetActionObserver result = null;
        switch (id)
        {
            case EnemyTargetActionDefine.ACTION_ID.CAPSULE: result = new EnemyTargetCupsuleActionObserver(); break;
        }
        return result;
    }


}
