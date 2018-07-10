using UnityEngine;
using System.Collections;
using System;

[Serializable]
[CreateAssetMenu(fileName = "Stage", menuName = "Create/stage")]
public class StageModel : ScriptableObject
{
    [SerializeField]
    private int _id;
    public int Id { get { return _id; } }

    //[SerializeField]
    //private GameObject _prefab;
    //public GameObject Prefab { get { return _prefab; } }

    [SerializeField]
    private string _stageNm;
    public string StageNm { get { return _stageNm; } }

    [SerializeField]
    private int _clearTime;
    public int ClearTime { get { return _clearTime; } }

}
