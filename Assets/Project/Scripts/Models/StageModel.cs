﻿using UnityEngine;
using System.Collections;
using System;

[Serializable]
[CreateAssetMenu(fileName = "Stage", menuName = "Create/stage")]
public class StageModel : ScriptableObject
{
    [SerializeField]
    private int _id;
    public int Id { get { return _id; } }

    [SerializeField]
    private GameObject _prefab;
    public GameObject Prefab { get { return _prefab; } }
}
