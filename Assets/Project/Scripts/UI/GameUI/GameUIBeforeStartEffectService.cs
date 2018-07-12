using UnityEngine;
using System.Collections;
using System;
using UniRx;
using TMPro;

public class GameUIBeforeStartEffectService
{
    private GameUIController _gameUIController;

    private IDisposable _iDisposable;

    public delegate void GameUIBeforeStarEffectEndDelegate();
    public GameUIBeforeStarEffectEndDelegate GameUIBeforeStarEffectEndEvent = delegate { };

    private TextMeshProUGUI _beforeStartEffectText;

    private string _stageNo;

    public GameUIBeforeStartEffectService(GameUIController gameUIController)
    {
        _gameUIController = gameUIController;
        _beforeStartEffectText = gameUIController.BeforeStartEffectText;
    }

    public void StartAsync(string stageNo)
    {
        _stageNo = stageNo;
        _iDisposable = Observable.FromCoroutine(Coroutine).Subscribe();
    }

    private IEnumerator Coroutine()
    {
        _beforeStartEffectText.text = "STAGE : " + _stageNo;
        _beforeStartEffectText.fontSize = 20;
        yield return new WaitForSeconds(0.5f);

        _beforeStartEffectText.text = "3";
        _beforeStartEffectText.fontSize = 250;
        yield return new WaitForSeconds(0.5f);

        _beforeStartEffectText.text = "2";
        yield return new WaitForSeconds(0.5f);

        _beforeStartEffectText.text = "1";
        yield return new WaitForSeconds(0.5f);

        _beforeStartEffectText.text = "";
        GameUIBeforeStarEffectEndEvent();
        yield return null;
    }

    public void Pause()
    {
        if (_iDisposable != null)
            _iDisposable.Dispose();
    }
}
