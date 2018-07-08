using UnityEngine;
using System.Collections;

/// <summary>
/// GameUIの視点切替ボタンの監視
/// </summary>
public class GameUIVisulPointChangeButtonService
{
    private GameUIController _gameUIController;

    public GameUIVisulPointChangeButtonService(GameUIController gameUIController)
    {
        _gameUIController = gameUIController;
        _gameUIController.VisualPointChangeButton.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        NrcGameManager.ChangeCamera();
    }
}
