using UnityEngine;
using System.Collections;

public class NrcGameManager
{
    public static NrcGameManager Instance = new NrcGameManager();

    public NrcSceneLoader _nrcSceneLoader;
    public NrcSceneLoader NrcSceneLoader { get { return _nrcSceneLoader; } }

    private NrcGameCameraChangeService _nrcGameCameraChangeService;
    
    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="nrcSceneLoader"></param>
    public static void Init(NrcSceneLoader nrcSceneLoader)
    {
        Instance._nrcSceneLoader = nrcSceneLoader;
        Instance._nrcGameCameraChangeService = new NrcGameCameraChangeService(Instance._nrcSceneLoader);
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
}
