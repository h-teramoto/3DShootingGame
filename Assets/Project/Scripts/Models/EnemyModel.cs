using UnityEngine;
using System.Collections;
using System;

[Serializable]
[CreateAssetMenu(fileName ="Enemy",menuName ="Create/enemy")]
public class EnemyModel : ScriptableObject
{
    [SerializeField]
    private int _id;
    public int Id { get { return Id; } }

    [SerializeField]
    private int _hp;
    public int Hp { get { return _hp; } }

    [SerializeField]
    private int _score;
    public int Score { get { return _score; } }

    [SerializeField]
    private GameObject _prefab;
    public GameObject Prefab { get { return _prefab; } }
    

}
