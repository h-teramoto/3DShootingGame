using UnityEngine;
using System.Collections;
using System;
using TMPro;
using UniRx;

public class GameUIGameClearEffectService 
{

    private GameUIController _gameUIController;

    private IDisposable _iDisposable;

    private TextMeshProUGUI _beforeStartEffectText;

    public delegate void GameUIGameClearEndDelegate();
    public GameUIGameClearEndDelegate GameUIGameClearEndEvent = delegate { };


    public GameUIGameClearEffectService(GameUIController gameUIController)
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
        _beforeStartEffectText.text = "Game Clear Congratulation";
        _beforeStartEffectText.fontSize = 50;
        yield return new WaitForSeconds(5);
        GameUIGameClearEndEvent();
        yield return null;
    }

    public void Pause()
    {
        if (_iDisposable != null)
            _iDisposable.Dispose();
    }
}
