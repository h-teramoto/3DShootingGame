using UnityEngine;
using System.Collections;

public class NrcGameManager
{
    public static NrcGameManager Instance = new NrcGameManager();

    public NrcSceneLoader _nrcSceneLoader;
    public NrcSceneLoader NrcSceneLoader { get { return _nrcSceneLoader; } }

    private NrcGameCameraChangeService _nrcGameCameraChangeService;

    private NrcGameStageChangeService _nrcGameStageChangeService;

    private StageController _nowStageController;

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="nrcSceneLoader"></param>
    public static void Init(NrcSceneLoader nrcSceneLoader)
    {
        Instance._nrcSceneLoader = nrcSceneLoader;
        Instance._nrcGameCameraChangeService = new NrcGameCameraChangeService(nrcSceneLoader);
        Instance._nrcGameStageChangeService = new NrcGameStageChangeService(nrcSceneLoader);

        //ステージの読み込み 
         StageLoad(1);
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
        Instance._nrcGameCameraChangeService.Change();
    }

    /// <summary>
    /// 現在アクティブなカメラの取得
    /// </summary>
    /// <returns>現在アクティブなカメラ</returns>
    public static Camera GetActiveCamera()
    {
        return Instance._nrcGameCameraChangeService.GetActiveCamera();
    } 

    /// <summary>
    /// ステージをロードする
    /// </summary>
    /// <param name="id"></param>
    public static void StageLoad(int id)
    {
        Instance._nowStageController =
            Instance._nrcGameStageChangeService.StageLoad(id);


    }
}
