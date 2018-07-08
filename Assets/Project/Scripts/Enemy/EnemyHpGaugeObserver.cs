﻿using UnityEngine;
using System.Collections;
using UniRx;
using UnityEngine.UI;
using System;

public class EnemyHpGaugeObserver
{
    private EnemyController _enemyController;

    private GameObject _hpGaugePrefab;

    private Slider _slider;

    private IDisposable _iDisposable;

    public EnemyHpGaugeObserver(EnemyController enemyController)
    {
        _enemyController = enemyController;
        _hpGaugePrefab = NrcResourceManager.GetGameObject(ResourceDefine.PREFAB_ENEMY_HP_GAUGE) as GameObject;
    }

    public void DisplayAsync()
    {
        _iDisposable = Observable.FromCoroutine(Coroutine).Subscribe();
    }

    private IEnumerator Coroutine()
    {
        GameObject hpGauge = GameObject.Instantiate(_hpGaugePrefab, 
            NrcGameManager.NrcGameStageService.GetNowStageController().transform);

        Slider slider = hpGauge.transform.Find("HpSlider").GetComponent<Slider>();
        slider.value = (float)_enemyController.Hp / (float)_enemyController.MaxHp;

        _enemyController.EnemyDamageService.EnemyHpChangeEvent += (hp) =>
        {
            slider.value = (float)hp / (float)_enemyController.MaxHp;
            if(hp == 0)
            {
                GameObject.Destroy(hpGauge);
                _iDisposable.Dispose();
            }
        };

        while (true)
        {
            if (hpGauge == null) yield break;
            hpGauge.transform.position = _enemyController.transform.position + new Vector3(0, 1, 2);
            hpGauge.transform.LookAt(NrcGameManager.GetActiveCamera().transform);
            yield return null;
        }

        
    }
}
