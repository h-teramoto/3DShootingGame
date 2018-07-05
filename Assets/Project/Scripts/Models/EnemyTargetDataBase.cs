using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
[CreateAssetMenu(fileName = "EnemyTargetDataBase", menuName = "Create/enemyTargetDataBase")]
public class EnemyTargetDataBase : ScriptableObject
{

    [SerializeField]
    private List<EnemyTargetModel> list = new List<EnemyTargetModel>();

    public EnemyTargetModel GetEnemyTargetById(int id)
    {
        return list.Find(target => target.Id == id);
    }

}
