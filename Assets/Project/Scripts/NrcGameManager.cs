using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NrcGameManager
{
    public static NrcGameManager Instance = new NrcGameManager();

    private NrcSceneLoader _nrcSceneLoader;
    public NrcSceneLoader NrcSceneLoader { get { return _nrcSceneLoader; } }

    private NrcGameCameraService _nrcGameCameraService;
    public static NrcGameCameraService NrcGameCameraService {
        get {
            if(Instance._nrcGameCameraService == null)
            {
                Instance._nrcGameCameraService = new NrcGameCameraService(Instance.NrcSceneLoader);
            }
            return Instance._nrcGameCameraService;
        }
    }

    private NrcGameStageObserver _nrcGameStageService;
    public static NrcGameStageObserver NrcGameStageService {
        get {
            if(Instance._nrcGameStageService == null)
            {
                Instance._nrcGameStageService = new NrcGameStageObserver(Instance.NrcSceneLoader);
            }
            return Instance._nrcGameStageService;
        }
    }

    private NrcGameScoreService _nrcGameScoreService;
    public static NrcGameScoreService NrcGameScoreService {
        get {
            if(Instance._nrcGameScoreService == null)
            {
                Instance._nrcGameScoreService = new NrcGameScoreService(Instance.NrcSceneLoader);
            }
            return Instance._nrcGameScoreService;
        }
    }

    private NrcGameEnemyService _nrcGameEnemyService;
    public static NrcGameEnemyService NrcGameEnemyService {
        get {
            if(Instance._nrcGameEnemyService == null)
            {
                Instance._nrcGameEnemyService = new NrcGameEnemyService(Instance.NrcSceneLoader, NrcGameStageService);
            }
            return Instance._nrcGameEnemyService;
        }
    }

    private NrcGameDatabaseService _nrcGameDatabaseService;
    public static NrcGameDatabaseService NrcGameDatabaseService {
        get {
            if(Instance._nrcGameDatabaseService == null)
            {
                Instance._nrcGameDatabaseService = new NrcGameDatabaseService(Instance.NrcSceneLoader);
            }
            return Instance._nrcGameDatabaseService;
        }
    }

    private NrcGameEnemyTargetService _nrcGameEnemyTargetService;
    public static NrcGameEnemyTargetService NrcGameEnemyTargetService {
        get {
            if(Instance._nrcGameEnemyTargetService == null)
            {
                Instance._nrcGameEnemyTargetService = new NrcGameEnemyTargetService(Instance.NrcSceneLoader);
            }
            return Instance._nrcGameEnemyTargetService;
        }
    }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="nrcSceneLoader"></param>
    public static void Init(NrcSceneLoader nrcSceneLoader)
    {
        Instance._nrcSceneLoader = nrcSceneLoader;
        NrcGameCameraService.Change(NrcGameCameraService.CAMERA_MODE.CAMERA_MODE_MAIN);
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
        NrcGameCameraService.Change();
    }

    /// <summary>
    /// 現在アクティブなカメラの取得
    /// </summary>
    /// <returns>現在アクティブなカメラ</returns>
    public static Camera GetActiveCamera()
    {
        return NrcGameCameraService.GetActiveCamera();
    } 

    /// <summary>
    /// 
    /// </summary>
    public static void Pause()
    {
        StageController sc = NrcGameStageService.GetNowStageController();
        if (sc != null) sc.Pause();

        Instance.NrcSceneLoader.PlayerController.Pause();

        Time.timeScale = 0;
    }


}
