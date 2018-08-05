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
                //Vector3 initForward = enemyController.transform.position - enemyTargetController.transform.position;
                ////enemyTargetController.transform.LookAt(enemyController.transform);
                //Ray ray = new Ray(enemyTargetController.transform.position, initForward.normalized);
                //RaycastHit hit;
                //Debug.DrawRay(ray.origin, ray.direction * 30.0f, Color.red, 0.0f);
                //if (Physics.Raycast(ray, out hit, 1000.0f))
                //{
                //    //enemyTargetController.transform.LookAt(initForward);
                //    if (hit.collider.tag == "Stage")
                //    {
                //        continue;
                //    }
                //}
                //else
                //{
                //    enemyTargetController.transform.LookAt(initForward);
                //    continue;
                //}
                

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
