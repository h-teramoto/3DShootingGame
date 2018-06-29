using UnityEngine;
using System.Collections;

public class NrcGameScoreService
{
    private NrcSceneLoader _nrcSceneLoader;

    private int _score = 0;

    private GameUIController _gameUIController;

    public NrcGameScoreService(NrcSceneLoader nrcSceneLoader)
    {
        _nrcSceneLoader = nrcSceneLoader;
        _gameUIController =  nrcSceneLoader.GameUIController;
    }

    public void ScoreUp(int score)
    {
        _score += score;
        _gameUIController.ScoreUpdate(_score);
    }

    public int GetScore()
    {
        return _score;
    }

}
