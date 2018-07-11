using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NrcGameManager
{
    public static NrcGameManager Instance = new NrcGameManager();

    private NrcSceneLoader _nrcSceneLoader;
    private NrcSceneLoader NrcSceneLoader { get { return _nrcSceneLoader; } }

    private NrcGameCameraService _nrcGameCameraService;
    public static NrcGameCameraService NrcGameCameraService { get { return Instance._nrcGameCameraService; } }

    private NrcGameStageService _nrcGameStageService;
    public static NrcGameStageService NrcGameStageService { get { return Instance._nrcGameStageService; } }

    private NrcGameScoreService _nrcGameScoreService;
    public static NrcGameScoreService NrcGameScoreService { get { return Instance._nrcGameScoreService; } }

    private NrcGameEnemyService _nrcGameEnemyService;
    public static NrcGameEnemyService NrcGameEnemyService { get { return Instance._nrcGameEnemyService; } }

    private NrcGameDatabaseService _nrcGameDatabaseService;
    public static NrcGameDatabaseService NrcGameDatabaseService { get { return Instance._nrcGameDatabaseService; } }


    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="nrcSceneLoader"></param>
    public static void Init(NrcSceneLoader nrcSceneLoader)
    {
        Instance._nrcSceneLoader = nrcSceneLoader;
        Instance._nrcGameCameraService = new NrcGameCameraService(nrcSceneLoader);
        Instance._nrcGameStageService = new NrcGameStageService(nrcSceneLoader);
        Instance._nrcGameScoreService = new NrcGameScoreService(nrcSceneLoader);
        Instance._nrcGameEnemyService = new NrcGameEnemyService(nrcSceneLoader, Instance._nrcGameStageService);
        Instance._nrcGameDatabaseService = new NrcGameDatabaseService(nrcSceneLoader);

        //ステージの読み込み 
        NrcGameStageService.StageLoad(1);
    }

    public static PlayerController GetPlayerController()
    {
        return Instance._nrcSceneLoader.PlayerController;
    }

    /// <summary>
    /// カメラ視点の切り替え
    /// </summary>
    public static void ChangeCamera()
    {
        Instance._nrcGameCameraService.Change();
    }

    /// <summary>
    /// 現在アクティブなカメラの取得
    /// </summary>
    /// <returns>現在アクティブなカメラ</returns>
    public static Camera GetActiveCamera()
    {
        return Instance._nrcGameCameraService.GetActiveCamera();
    } 

    /// <summary>
    /// 
    /// </summary>
    public static void Pause()
    {
        StageController sc = NrcGameStageService.GetNowStageController();
        if (sc != null) sc.Pause();

        Instance._nrcSceneLoader.PlayerController.Pause();

        Time.timeScale = 0;
    }

    public static void GameOver()
    {
        Instance.NrcSceneLoader.GameUIController.GameUIGameOverEffectService.StartAsync();
    }
}
