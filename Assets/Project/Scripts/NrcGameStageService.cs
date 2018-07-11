using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UniRx;

public class NrcGameStageService
{
    private NrcSceneLoader _nrcSceneLoader;

    private StageDataBase _stageDataBase;

    private PlayerController _playerController;

    private GameUIController _gameUIController;

    private int _nowStageId;

    private string _nowStageNm;

    private int _clearTime;

    private IDisposable _iDisposable;

    private StageController _stageController;

    //public delegate void StageChangeDelegate(StageController stageController);
    //public StageChangeDelegate stageChangeEvent = delegate { };


    public NrcGameStageService(NrcSceneLoader nrcSceneLoader)
    {
        _nrcSceneLoader = nrcSceneLoader;
        _stageDataBase = nrcSceneLoader.StageDataBase;
        _playerController = nrcSceneLoader.PlayerController;
        _gameUIController = nrcSceneLoader.GameUIController;
        _nowStageId = 1;

        _gameUIController.GameUIBeforeStarEffectService.GameUIBeforeStarEffectEndEvent += () =>
        {
            _playerController.Beginning();
            _stageController.Beginning();
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
        _iDisposable = Observable.FromCoroutine(Observable => Coroutine(id)).Subscribe();
    }

    private IEnumerator Coroutine(int id)
    {
        if (_nowStageNm != null)
        {
            SceneManager.UnloadSceneAsync(_nowStageNm);
            _stageController = null;
        }

        StageModel stageModel = _stageDataBase.GetStageById(id);
        _clearTime = stageModel.ClearTime;

        Debug.Log(stageModel.StageNm);
        AsyncOperation ope = SceneManager.LoadSceneAsync(stageModel.StageNm, LoadSceneMode.Additive);
        ope.allowSceneActivation = false;
        while (ope.progress < 0.9f)
        {
            yield return null;
        }
        ope.allowSceneActivation = true;

        while (_stageController == null)
        {
            yield return null;
        }
        
        _stageController.Init();
        
        _playerController.Pause();
        _stageController.Pause();

        //ステージスタート前処理
        _gameUIController.GameUIBeforeStarEffectService.StartAsync(id.ToString());

        _nowStageNm = stageModel.StageNm;
        _nowStageId = id;

        yield return null;
    }

    public int NextStage()
    {
        _playerController.Pause();
        _stageController.Pause();

        _nowStageId++;
        StageLoad(_nowStageId);
        return _nowStageId;
    }


    public void SetNowStageController(StageController stageController)
    {
        _stageController = stageController;
    }

    public StageController GetNowStageController()
    {
        return _stageController;
    }

}
