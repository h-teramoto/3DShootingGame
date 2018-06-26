using UnityEngine;
using System.Collections;

/// <summary>
/// GameUIの視点切替ボタンの監視
/// </summary>
public class GameUIVisulPointChangeButtonObserver
{
    private GameUIController _gameUIController;

    public GameUIVisulPointChangeButtonObserver(GameUIController gameUIController)
    {
        _gameUIController = gameUIController;
    }

    public void Observe()
    {
        _gameUIController.VisualPointChangeButton.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        NrcGameManager.ChangeCamera();
    }
}
