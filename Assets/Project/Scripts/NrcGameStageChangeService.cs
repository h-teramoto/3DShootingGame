using UnityEngine;
using System.Collections;

public class NrcGameStageChangeService
{
    private NrcSceneLoader _nrcSceneLoader;

    private StageDataBase _stageDataBase;

    private PlayerController _playerController;

    private int _nowStageId;

    private GameObject _stageGameObject; 

    public NrcGameStageChangeService(NrcSceneLoader nrcSceneLoader)
    {
        _nrcSceneLoader = nrcSceneLoader;
        _stageDataBase = nrcSceneLoader.StageDataBase;
        _playerController = nrcSceneLoader.PlayerController;
        _nowStageId = 1; 
    }

    public void StageLoad(int id)
    {
        if (_stageGameObject != null) GameObject.Destroy(_stageGameObject);

        StageModel stageModel = _stageDataBase.GetStageById(id);
        _stageGameObject = GameObject.Instantiate(stageModel.Prefab);

        _nowStageId = id;

    }

    public int NextStage()
    {
        _nowStageId++;
        StageLoad(_nowStageId);
        return _nowStageId;
    }

    





}
