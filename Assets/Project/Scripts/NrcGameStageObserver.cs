using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UniRx;

public class NrcGameStageObserver : INrcObserver
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

    public NrcGameStageObserver(NrcSceneLoader nrcSceneLoader)
    {
        _nrcSceneLoader = nrcSceneLoader;
        _stageDataBase = nrcSceneLoader.StageDataBase;
        _playerController = nrcSceneLoader.PlayerController;
        _gameUIController = nrcSceneLoader.GameUIController;
        _nowStageId = 1;

        //スタート前カウントダウンが終了したときに呼ばれる
        _gameUIController.GameUIBeforeStarEffectService.GameUIBeforeStarEffectEndEvent += () =>
        {
            _playerController.Beginning();
            _stageController.Beginning();
            _gameUIController.GameUICountDownService.StartAsync(_clearTime);
        };

        //ステージタイムリミット終了時に呼ばれる
        _gameUIController.GameUICountDownService.GameUICountDownEndEvent += () =>
        {
            _gameUIController.GameUIStageClearEffectService.StartAsync();
        };

        //ステージクリア表示終了時に呼ばれる
        _gameUIController.GameUIStageClearEffectService.GameUIStageClearEffectEndEvent += () =>
        {
            NextStage();
        };

        //EnemyTargetが全滅したときに呼ばれる
        NrcGameManager.NrcGameEnemyTargetService.EnemyTargetAllDeadEvent += () =>
        {
            _gameUIController.Stop();
            _gameUIController.GameUIGameOverEffectService.StartAsync();
        };

        //ゲームオーバー表示終了時に呼ばれる
        _gameUIController.GameUIGameOverEffectService.GameUIGameOverEndEvent += () =>
        {
            StageLoad(1);
        };
    }


    public void StageLoad(int id)
    {
        _nowStageId = id;
        BeginningAsync();
    }

    public void BeginningAsync()
    {
        _iDisposable = Observable.FromCoroutine(Observable => Coroutine(_nowStageId)).Subscribe();
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
        _nowStageNm = stageModel.StageNm;

        //ステージシーンの読み込み。
        AsyncOperation ope = SceneManager.LoadSceneAsync(stageModel.StageNm, LoadSceneMode.Additive);
        ope.allowSceneActivation = false;
        while (ope.progress < 0.9f)
        {
            yield return null;
        }
        ope.allowSceneActivation = true;
        
        //セットされるまで待つ
        while (_stageController == null) yield return null;
        _stageController.Init();
        
        _playerController.Pause();
        _stageController.Pause();

        //ステージスタート前処理
        _gameUIController.GameUIBeforeStarEffectService.StartAsync(id.ToString());

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

    public void Pause()
    {
        if (_iDisposable != null)
            _iDisposable.Dispose();
    }


}
