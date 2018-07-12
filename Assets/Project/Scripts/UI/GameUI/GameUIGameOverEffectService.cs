﻿using UnityEngine;
using System.Collections;
using System;
using TMPro;
using UniRx;
using UnityEngine.SceneManagement;

public class GameUIGameOverEffectService
{
    private GameUIController _gameUIController;

    private IDisposable _iDisposable;

    private TextMeshProUGUI _beforeStartEffectText;

    public delegate void GameUIGameOverEndDelegate();
    public GameUIGameOverEndDelegate GameUIGameOverEndEvent = delegate { };

    public GameUIGameOverEffectService(GameUIController gameUIController)
    {
        _gameUIController = gameUIController;
        _beforeStartEffectText = gameUIController.BeforeStartEffectText;
    }

    public void StartAsync()
    {
        _iDisposable = Observable.FromCoroutine(Coroutine).Subscribe();
    }

    private IEnumerator Coroutine()
    {
        _beforeStartEffectText.text = "Game Over";
        _beforeStartEffectText.fontSize = 50;
        yield return new WaitForSeconds(2);
        GameUIGameOverEndEvent();
        yield return null;
    }

    public void Pause()
    {
        if (_iDisposable != null)
            _iDisposable.Dispose();
    }
}
