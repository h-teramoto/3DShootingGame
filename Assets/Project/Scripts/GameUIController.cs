using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class GameUIController : MonoBehaviour
{
    [SerializeField]
    private Button _visualPointChangeButton;
    public Button VisualPointChangeButton { get { return _visualPointChangeButton; } }

    [SerializeField]
    private TextMeshProUGUI _beforeStartEffectText;
    public TextMeshProUGUI BeforeStartEffectText { get { return _beforeStartEffectText; } }

    public delegate void GameUIBeforeStarEffectEndDelegate();
    public GameUIBeforeStarEffectEndDelegate GameUIBeforeStarEffectEndEvent = delegate { };

    private GameUIBeforeStarEffectService _gameUIBeforeStarEffectService;
    private GameUIBeforeStarEffectService GameUIBeforeStarEffectService
    {
        get
        {
            if (_gameUIBeforeStarEffectService == null)
            {
                _gameUIBeforeStarEffectService = new GameUIBeforeStarEffectService(this);
            }
            return _gameUIBeforeStarEffectService;
        }
    }

    /// <summary>
    /// ステージ開始前のエフェクト開始
    /// 終了後　GameUIBeforeStarEffectEndEvent　が発火する
    /// </summary>
    /// <param name="stageNo"></param>
    public void BeforeStartEffect(string stageNo)
    {
        GameUIBeforeStarEffectService.StartAsync(stageNo);
        GameUIBeforeStarEffectService.GameUIBeforeStarEffectEndEvent += () =>
        {
            this.GameUIBeforeStarEffectEndEvent();
        };
    }

    /// <summary>
    /// ステージタイマーの開始
    /// </summary>
    public void StageTimerStart()
    {

    }

    /// <summary>
    /// ステージクリア時のエフェクト開始
    /// </summary>
    public void StageClearEffect()
    {

    }

    /// <summary>
    /// スコアの更新
    /// </summary>
    /// <param name="score"></param>
    public void ScoreUpdate(int score)
    {
        Debug.Log("得点:" + score);
    }

    void Start()
    {
        GameUIVisulPointChangeButtonService _gameUIVisulPointChangeButtonObserver = new GameUIVisulPointChangeButtonService(this);
    }
}
