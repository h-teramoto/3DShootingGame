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

    private GameUIBeforeStartEffectService _gameUIBeforeStarEffectService;
    public GameUIBeforeStartEffectService GameUIBeforeStarEffectService
    {
        get
        {
            if (_gameUIBeforeStarEffectService == null)
            {
                _gameUIBeforeStarEffectService = new GameUIBeforeStartEffectService(this);
            }
            return _gameUIBeforeStarEffectService;
        }
    }

    private GameUIStageClearEffectService _gameUIStageClearEffectService;
    public GameUIStageClearEffectService GameUIStageClearEffectService
    {
        get
        {
            if (_gameUIStageClearEffectService == null)
            {
                _gameUIStageClearEffectService = new GameUIStageClearEffectService(this);
            }
            return _gameUIStageClearEffectService;
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

    private GameUIGameOverEffectService _gameUIGameOverEffectService;
    public GameUIGameOverEffectService GameUIGameOverEffectService
    {
        get
        {
            if(_gameUIGameOverEffectService == null)
            {
                _gameUIGameOverEffectService = new GameUIGameOverEffectService(this);
            }
            return _gameUIGameOverEffectService;
        }
    }

    private GameUIGameClearEffectService _gameUIGameClearEffectService;
    public GameUIGameClearEffectService GameUIGameClearEffectService
    {
        get
        {
            if (_gameUIGameClearEffectService == null)
            {
                _gameUIGameClearEffectService = new GameUIGameClearEffectService(this);
            }
            return _gameUIGameClearEffectService;
        }
    }

    

    public void Stop()
    {
        GameUIBeforeStarEffectService.Pause();
        GameUIStageClearEffectService.Pause();
        GameUICountDownService.Pause();
        GameUIGameOverEffectService.Pause();
        GameUIGameClearEffectService.Pause();
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
