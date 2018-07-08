using UnityEngine;
using System.Collections;
using System;

[Serializable]
[CreateAssetMenu(fileName = "EnemyTarget", menuName = "Create/enemyTarget")]
public class EnemyTargetModel : ScriptableObject
{
    [SerializeField]
    private int _id;
    public int Id { get { return _id; } }

    [SerializeField]
    private int _hp;
    public int Hp { get { return _hp; } }

    [SerializeField]
    private GameObject _prefab;
    public GameObject Prefab { get { return _prefab; } }
}
