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

    public void BeforeStartEffect(string stageNo)
    {
        GameUIBeforeStarEffectService.StartAsync(stageNo);
        GameUIBeforeStarEffectService.GameUIBeforeStarEffectEndEvent += () =>
        {
            this.GameUIBeforeStarEffectEndEvent();
        };
    }


    void Start()
    {
        GameUIVisulPointChangeButtonObserver _gameUIVisulPointChangeButtonObserver = new GameUIVisulPointChangeButtonObserver(this);
        _gameUIVisulPointChangeButtonObserver.Observe();
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}
