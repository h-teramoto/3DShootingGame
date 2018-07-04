using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 
/// </summary>
public class GameUIController : MonoBehaviour
{
    [SerializeField]
    private Button _visualPointChangeButton;
    public Button VisualPointChangeButton { get { return _visualPointChangeButton; } }

    [SerializeField]
    private TextMeshProUGUI _beforeStartEffectText;
    public TextMeshProUGUI BeforeStartEffectText { get { return _beforeStartEffectText; } }

    [SerializeField]
    private TextMeshProUGUI _countDownText;
    public TextMeshProUGUI CountDownText { get { return _countDownText; } }

    [SerializeField]
    private TextMeshProUGUI _scoreText;
    public TextMeshProUGUI ScoreText { get { return _scoreText; } }

    public delegate void GameUIBeforeStarEffectEndDelegate();
    public GameUIBeforeStarEffectEndDelegate GameUIBeforeStarEffectEndEvent = delegate { };

    private GameUIBeforeStarEffectService _gameUIBeforeStarEffectService;
    public GameUIBeforeStarEffectService GameUIBeforeStarEffectService
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

    private GameUICountDownService _gameUICountDownService;
    public GameUICountDownService GameUICountDownService {
        get {
            if(_gameUICountDownService == null)
            {
                _gameUICountDownService = new GameUICountDownService(this);
            }
            return _gameUICountDownService;
        }
    }

    private GameUIScoreService _gameUIScoreService;
    public GameUIScoreService GameUIScoreService
    {
        get
        {
            if (_gameUIScoreService == null)
            {
                _gameUIScoreService = new GameUIScoreService(this);
            }
            return _gameUIScoreService;
        }
    }

    

    /// <summary>
    /// スコアの更新
    /// </summary>
    /// <param name="score"></param>
    //public void ScoreUpdate(int score)
    //{
    //    Debug.Log("得点:" + score);
    //}

    void Start()
    {
        GameUIVisulPointChangeButtonService _gameUIVisulPointChangeButtonObserver = new GameUIVisulPointChangeButtonService(this);
    }
}
