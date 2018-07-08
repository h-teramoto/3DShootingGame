using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class EnemyActionCreator
{
    public static IEnemyActionObserver GetInstance(EnemyActionDefine.ACTION_ID id)
    {
        IEnemyActionObserver result = null;
        switch (id)
        {
            case EnemyActionDefine.ACTION_ID.CUBE : result =new  EnemyActionCubeObserver(); break;
        }
        return result;
    }


}
