using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
[CreateAssetMenu(fileName = "StageDataBase", menuName = "Create/stageDataBase")]
public class StageDataBase : ScriptableObject
{
    [SerializeField]
    private List<StageModel> list = new List<StageModel>();

    public StageModel GetStageById(int id)
    {
        return list.Find(target => target.Id == id);
    }
}
