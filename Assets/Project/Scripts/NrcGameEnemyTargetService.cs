using UnityEngine;
using System.Collections;

public class NrcGameEnemyTargetService 
{
    private NrcSceneLoader _nrcSceneLoader;

    public delegate void EnemyTargetAllDeadDelegate();
    public EnemyTargetAllDeadDelegate EnemyTargetAllDeadEvent = delegate { };

    public NrcGameEnemyTargetService(NrcSceneLoader nrcSceneLoader)
    {
        _nrcSceneLoader = nrcSceneLoader;
        
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

        foreach (EnemyTargetController etc in NrcGameManager.NrcGameStageObserver.GetNowStageController().StageEnemyTargetObserver.EnemyTargetControllerList)
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
