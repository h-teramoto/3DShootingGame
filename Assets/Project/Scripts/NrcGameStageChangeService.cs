using UnityEngine;
using System.Collections;

public class NrcGameStageChangeService
{
    private NrcSceneLoader _nrcSceneLoader;

    private StageDataBase _stageDataBase;

    private PlayerController _playerController;

    private GameUIController _gameUIController;

    private int _nowStageId;

    private GameObject _stageGameObject;


    public NrcGameStageChangeService(NrcSceneLoader nrcSceneLoader)
    {
        _nrcSceneLoader = nrcSceneLoader;
        _stageDataBase = nrcSceneLoader.StageDataBase;
        _playerController = nrcSceneLoader.PlayerController;
        _gameUIController = nrcSceneLoader.GameUIController;
        _nowStageId = 1; 
    }

    public StageController StageLoad(int id)
    {
        if (_stageGameObject != null)
            GameObject.Destroy(_stageGameObject);

        _playerController.Pause();

        //ステージスタート前処理
        _gameUIController.BeforeStartEffect(id.ToString());
        _gameUIController.GameUIBeforeStarEffectEndEvent += () =>
        {
            _playerController.Restart();
        };

        StageModel stageModel = _stageDataBase.GetStageById(id);
        _stageGameObject = GameObject.Instantiate(stageModel.Prefab);

        _nowStageId = id;

        return _stageGameObject.GetComponent<StageController>();

    }

    public int NextStage()
    {
        _nowStageId++;
        StageLoad(_nowStageId);
        return _nowStageId;
    }

    





}
