using UnityEngine;
using System.Collections;
using System;
using UniRx;
using TMPro;

public class GameUIBeforeStarEffectService
{
    private GameUIController _gameUIController;

    private IDisposable _iDisposable;

    public delegate void GameUIBeforeStarEffectEndDelegate();
    public GameUIBeforeStarEffectEndDelegate GameUIBeforeStarEffectEndEvent = delegate { };

    private TextMeshProUGUI _beforeStartEffectText;

    private string _stageNo;

    public GameUIBeforeStarEffectService(GameUIController gameUIController)
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
        _beforeStartEffectText.fontSize = 50;
        yield return new WaitForSeconds(0.5f);

        _beforeStartEffectText.text = "3";
        _beforeStartEffectText.fontSize = 250;
        yield return new WaitForSeconds(1);

        _beforeStartEffectText.text = "2";
        yield return new WaitForSeconds(1);

        _beforeStartEffectText.text = "1";
        yield return new WaitForSeconds(1);

        _beforeStartEffectText.text = "";
        GameUIBeforeStarEffectEndEvent();
        yield return null;
    }
}
