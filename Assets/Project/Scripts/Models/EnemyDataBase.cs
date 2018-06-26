using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
[CreateAssetMenu(fileName = "EnemyDataBase", menuName = "Create/enemyDataBase")]
public class EnemyDataBase : ScriptableObject
{
    [SerializeField]
    private List<EnemyModel> list = new List<EnemyModel>();

    public EnemyModel GetEnemyById(int id)
    {
        return list.Find(target => target.Id == id);
    }
}
