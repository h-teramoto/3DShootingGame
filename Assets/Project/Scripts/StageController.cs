using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageController : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPoint;

    [SerializeField]
    private List<StageEnemySpawnController> _stageEnemySpawnControllerList;

    private PlayerController _playerController;

    public delegate void StageClearDelegate(StageController stageController);
    public StageClearDelegate stageClearEvent = delegate { };

    // Use this for initialization
    void Start()
    {
        _playerController = NrcGameManager.GetPlayerController();
        _playerController.transform.position = _playerPoint.transform.position;

        //EnemyのSpawn制御
        foreach(StageEnemySpawnController sesc in _stageEnemySpawnControllerList)
        {

        }
        //

   }

    // Update is called once per frame
    void Update()
    {

    }
}
