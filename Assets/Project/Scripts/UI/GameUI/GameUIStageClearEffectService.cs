using UnityEngine;
using System.Collections;
using System;
using TMPro;
using UniRx;

public class GameUIStageClearEffectService
{
    private GameUIController _gameUIController;

    private IDisposable _iDisposable;

    private TextMeshProUGUI _beforeStartEffectText;

    public delegate void GameUIStageClearEffectEndDelegate();
    public GameUIStageClearEffectEndDelegate GameUIStageClearEffectEndEvent = delegate { };

    public GameUIStageClearEffectService(GameUIController gameUIController)
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
        _beforeStartEffectText.text = "STAGE CLEAR";
        _beforeStartEffectText.fontSize = 50;
        yield return new WaitForSeconds(1);
        GameUIStageClearEffectEndEvent();
        yield return null;
    }

    public void Pause()
    {
        if (_iDisposable != null)
            _iDisposable.Dispose();
    }
}
