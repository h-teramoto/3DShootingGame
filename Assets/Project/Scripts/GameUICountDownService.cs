using UnityEngine;
using System.Collections;
using System;
using TMPro;
using UniRx;

public class GameUICountDownService
{
    private GameUIController _gameUIController;

    private IDisposable _iDisposable;

    private TextMeshProUGUI _countDownText;

    public delegate void GameUICountDownEndDelegate();
    public GameUICountDownEndDelegate GameUICountDownEndEvent = delegate { };

    private int _countNo;

    public GameUICountDownService(GameUIController gameUIController)
    {
        _gameUIController = gameUIController;
        _countDownText = gameUIController.CountDownText;
    }

    public void StartAsync(int countNo)
    {
        _countNo = countNo;
        _iDisposable = Observable.FromCoroutine(Coroutine).Subscribe();
    }

    private IEnumerator Coroutine()
    {
        for (int i = _countNo; i >= 0; i--)
        {
            _countDownText.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }

        GameUICountDownEndEvent();
    }
}
