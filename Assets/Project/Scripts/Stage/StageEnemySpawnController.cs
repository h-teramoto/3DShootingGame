using UnityEngine;
using System.Collections;

public class StageEnemySpawnController : MonoBehaviour, INrcController
{
    //出現率
    [SerializeField]
    private float _spawnProbability;
    public float SpawnProbability { get { return _spawnProbability; } }

    //出現するENEMY
    [SerializeField]
    private EnemyModel _enemyModel;
    public EnemyModel EnemyModel { get { return _enemyModel; } }

    //最大出現数
    [SerializeField]
    private int _maxSpawnCount;
    public int MaxSpawnCount { get { return _maxSpawnCount; } }

    //一度に出現する数
    [SerializeField]
    private int _numOfOccurrencesAtOnce;
    public int NumOfOccurrencesAtOnce { get { return _numOfOccurrencesAtOnce; } }

    //スポーンする時間間隔
    [SerializeField]
    private int _spawnTimespan;
    public int SpawnTimespan { get { return _spawnTimespan; } }

    //管理
    private StageEnemySpawnObserver _stageEnemySpawnObserver;
    public StageEnemySpawnObserver StageEnemySpawnObserver {
        get
        {
            if(_stageEnemySpawnObserver == null)
            {
                _stageEnemySpawnObserver = new StageEnemySpawnObserver(this);
            }
            return _stageEnemySpawnObserver;
        }
    }

    public void Pause()
    {
        StageEnemySpawnObserver.Pause();
    }
    
    public void Beginning()
    {
        StageEnemySpawnObserver.BeginningAsync();
    }
}
