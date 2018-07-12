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
}
