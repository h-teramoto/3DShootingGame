using UnityEngine;
using System.Collections;

public class NrcGameStageService
{
    private NrcSceneLoader _nrcSceneLoader;

    private StageDataBase _stageDataBase;

    private PlayerController _playerController;

    private GameUIController _gameUIController;

    private int _nowStageId;
    private int _clearTime;

    private GameObject _stageGameObject;
    private StageController _stageController;

    public delegate void StageChangeDelegate(StageController stageController);
    public StageChangeDelegate stageChangeEvent = delegate { };


    public NrcGameStageService(NrcSceneLoader nrcSceneLoader)
    {
        _nrcSceneLoader = nrcSceneLoader;
        _stageDataBase = nrcSceneLoader.StageDataBase;
        _playerController = nrcSceneLoader.PlayerController;
        _gameUIController = nrcSceneLoader.GameUIController;
        _nowStageId = 1;

        _gameUIController.GameUIBeforeStarEffectService.GameUIBeforeStarEffectEndEvent += () =>
        {
            _playerController.Restart();
            _stageController.Restart();
            _gameUIController.GameUICountDownService.StartAsync(_clearTime);
        };

        _gameUIController.GameUICountDownService.GameUICountDownEndEvent += () =>
        {
            _gameUIController.GameUIStageClearEffectService.StartAsync();
        };

        _gameUIController.GameUIStageClearEffectService.GameUIStageClearEffectEndEvent += () =>
        {
            NextStage();
        };
    }

    public void StageLoad(int id)
    {
        
        if (_stageGameObject != null)
        {
            GameObject.Destroy(_stageGameObject);
        }

        StageModel stageModel = _stageDataBase.GetStageById(id);
        _clearTime = stageModel.ClearTime;
        _stageGameObject = GameObject.Instantiate(stageModel.Prefab);
        _stageController = _stageGameObject.GetComponent<StageController>();
        stageChangeEvent(_stageController);

        _playerController.Pause();
        _stageController.Pause();

        //ステージスタート前処理
        _gameUIController.GameUIBeforeStarEffectService.StartAsync(id.ToString());


        _nowStageId = id;
    }

    public int NextStage()
    {
        _playerController.Pause();
        _stageController.Pause();

        _nowStageId++;
        StageLoad(_nowStageId);
        return _nowStageId;
    }

    public StageController GetNowStageController()
    {
        return _stageController;
    }

}
