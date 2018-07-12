using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    /// enemyTargetから一番近いEnemyを返す
    /// </summary>
    /// <param name="enemyTargetController"></param>
    /// <returns></returns>
    public EnemyController GetMostNearEnemy(EnemyTargetController enemyTargetController)
    {
        EnemyController result = null;
        float tempDistance = float.MaxValue;

        List<StageEnemySpawnController> stageEnemySpawnControllerList =
            _nrcGameStageService.GetNowStageController().StageEnemySpawnControllerList;

        foreach(StageEnemySpawnController stageEnemySpawnController in stageEnemySpawnControllerList)
        {
            List<EnemyController> enemyControllerList =
                stageEnemySpawnController.StageEnemySpawnObserver.EnemyList;

            foreach(EnemyController enemyController in enemyControllerList)
            {
                float distance = (enemyController.transform.position - enemyTargetController.transform.position).sqrMagnitude;
                if (tempDistance > distance)
                {
                    tempDistance = distance;
                    result = enemyController;
                }
            }

        }

        return result;
    }

}
