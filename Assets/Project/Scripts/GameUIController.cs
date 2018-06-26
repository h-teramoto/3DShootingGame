using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField]
    private Button _visualPointChangeButton;
    public Button VisualPointChangeButton { get { return _visualPointChangeButton; } }

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
