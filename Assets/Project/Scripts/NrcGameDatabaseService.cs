using UnityEngine;
using System.Collections;

public class NrcGameDatabaseService
{
    private NrcSceneLoader _nrcSceneLoader;

    public NrcGameDatabaseService(NrcSceneLoader nrcSceneLoader)
    {
        _nrcSceneLoader = nrcSceneLoader;
    }

    public EnemyTargetModel GetEnemyTargetModelById(int id)
    {
        return _nrcSceneLoader.EnemyTargetDataBase.GetEnemyTargetById(id);
    }

}
