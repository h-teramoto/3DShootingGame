using UnityEngine;
using System.Collections;
using System;

[Serializable]
[CreateAssetMenu(fileName = "Stage", menuName = "Create/stage")]
public class StageModel : ScriptableObject
{
    [SerializeField]
    private int _id;
    public int Id { get { return Id; } }

    [SerializeField]
    private GameObject _prefab;
    public GameObject Prefab { get { return _prefab; } }

    [SerializeField]
    private Vector3 _playerStartPosition;
    public Vector3 PlayerStartPosition { get { return _playerStartPosition; } }
}
