using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UniRx;

/// <summary>
/// ステージ制御サービス
/// </summary>
public class NrcGameStageObserver : INrcObserver
{
    private NrcSceneLoader _nrcSceneLoader;

    private StageDataBase _stageDataBase;

    private PlayerController _playerController;

    private GameUIController _gameUIController;

    private int _nowStageId;
    private string _nowStageNm;
    private StageController _nowStageController;

    private int _clearTime;

    private IDisposable _iDisposable;

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
            CharacterStart();
            _gameUIController.GameUICountDownService.StartAsync(_clearTime);
        };

        //ステージタイムリミット終了時に呼ばれる
        _gameUIController.GameUICountDownService.GameUICountDownEndEvent += () =>
        {
            CharacterPause();
            _gameUIController.GameUIStageClearEffectService.StartAsync();
        };

        //ステージクリア表示終了時に呼ばれる
        _gameUIController.GameUIStageClearEffectService.GameUIStageClearEffectEndEvent += () =>
        {
            CharacterPause();
            NextStage();
        };

        //EnemyTargetが全滅したときに呼ばれる
        NrcGameManager.NrcGameEnemyTargetService.EnemyTargetAllDeadEvent += () =>
        {
            CharacterPause();
            _gameUIController.Stop();
            _gameUIController.GameUIGameOverEffectService.StartAsync();
        };

        //ゲームオーバー表示終了時に呼ばれる
        _gameUIController.GameUIGameOverEffectService.GameUIGameOverEndEvent += () =>
        {
            CharacterPause();
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
        //すでにステージがあれば消す
        if (_nowStageNm != null)
        {
            AsyncOperation ope = SceneManager.UnloadSceneAsync(_nowStageNm);
            ope.allowSceneActivation = false;
            while (ope.progress < 0.9f) yield return null;
            ope.allowSceneActivation = true;

            _nowStageController = null;
        }

        //ステージ情報モデルの取得
        StageModel stageModel = _stageDataBase.GetStageById(id);
        _clearTime = stageModel.ClearTime;
        _nowStageNm = stageModel.StageNm;

        //ステージシーンの読み込み。
        { 
            AsyncOperation ope = SceneManager.LoadSceneAsync(stageModel.StageNm, LoadSceneMode.Additive);
            ope.allowSceneActivation = false;
            while (ope.progress < 0.9f) yield return null;
            ope.allowSceneActivation = true;
        }

        //セットされるまで待つ
        while (_nowStageController == null) yield return null;
        _nowStageController.Init();

        CharacterPause();

        //ステージスタート前処理
        _gameUIController.GameUIBeforeStarEffectService.StartAsync(id.ToString());

        yield return null;
    }

    public int NextStage()
    {
        CharacterPause();

        _nowStageId++;

        StageLoad(_nowStageId);

        return _nowStageId;
    }

    private void CharacterPause()
    {
        _playerController.Pause();
        _nowStageController.Pause();
    }

    private void CharacterStart()
    {
        _playerController.Beginning();
        _nowStageController.Beginning();
    }

    public void SetNowStageController(StageController stageController)
    {
        _nowStageController = stageController;
    }

    public StageController GetNowStageController()
    {
        return _nowStageController;
    }

    public void Pause()
    {
        if (_iDisposable != null)
            _iDisposable.Dispose();
    }


}
