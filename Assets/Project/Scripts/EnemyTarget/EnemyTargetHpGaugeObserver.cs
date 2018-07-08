using UnityEngine;
using System.Collections;
using System;
using UniRx;
using UnityEngine.UI;

public class EnemyTargetHpGaugeObserver
{
    private EnemyTargetController _enemyTargetController;

    private GameObject _hpGaugePrefab;

    private Slider _slider;

    private IDisposable _iDisposable;

    public EnemyTargetHpGaugeObserver(EnemyTargetController enemyTargetController)
    {
        _enemyTargetController = enemyTargetController;
        _hpGaugePrefab = NrcResourceManager.GetGameObject(ResourceDefine.PREFAB_ENEMY_TARGET_HP_GAUGE) as GameObject;
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
        slider.value = (float)_enemyTargetController.Hp / (float)_enemyTargetController.MaxHp;

        _enemyTargetController.EnemyTargetDamageService.EnemyTargetHpChangeEvent += (hp) =>
        {
            slider.value = (float)hp / (float)_enemyTargetController.MaxHp;
            if (hp == 0)
            {
                GameObject.Destroy(hpGauge);
                _iDisposable.Dispose();
            }
        };

        while (true)
        {
            if (hpGauge == null) yield break;
            hpGauge.transform.position = _enemyTargetController.transform.position + new Vector3(0, 1, 2);
            hpGauge.transform.LookAt(NrcGameManager.GetActiveCamera().transform);
            yield return null;
        }


    }



    }
