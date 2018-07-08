using UnityEngine;
using System.Collections;
using System;
using TMPro;

public class GameUIScoreService 
{
    private GameUIController _gameUIController;

    private IDisposable _iDisposable;

    private TextMeshProUGUI _scoreText;

    private int _score;

    public GameUIScoreService(GameUIController gameUIController)
    {
        _gameUIController = gameUIController;
        _scoreText = gameUIController.ScoreText;
        _score = 0;
    }

    public void ScoreUpdate(int score)
    {
        _score = score;
        _scoreText.text = _score.ToString();
    }



}
