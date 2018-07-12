using UnityEngine;
using System.Collections;

public class NrcGameEnemyService
{
    private NrcSceneLoader _nrcSceneLoader;

    private NrcGameStageObserver _nrcGameStageService;

    public NrcGameEnemyService(NrcSceneLoader nrcSceneLoader, NrcGameStageObserver nrcGameStageService)
    {
        _nrcSceneLoader = nrcSceneLoader;
        _nrcGameStageService = nrcGameStageService;

    }

    /// <summary>
    /// 最もenemyControllerから近いEnemyTargetControllerを返す
    /// </summary>
    /// <param name="enemyController"></param>
    /// <returns></returns>
    public EnemyTargetController GetMostNearEnemyTaget(EnemyController enemyController)
    {
        EnemyTargetController result = null;
        float tempDistance = float.MaxValue;

        foreach (EnemyTargetController etc in _nrcGameStageService.GetNowStageController().StageEnemyTargetObserver.EnemyTargetControllerList)
        {
            
            float distance = (etc.transform.position - enemyController.transform.position).sqrMagnitude;
            if (tempDistance > distance)
            {
                tempDistance = distance;
                result = etc;
            }
        }

        return result;
    }


}
