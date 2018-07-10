using UnityEngine;
using System.Collections;
using System;
using UniRx;
using UnityEngine.UI;

public class EnemyTargetHpGaugeObserver : INrcObserver
{
    private EnemyTargetController _enemyTargetController;

    private GameObject _hpGaugePrefab;

    private GameObject _hpGauge;

    private Slider _slider;

    private IDisposable _iDisposable;

    public EnemyTargetHpGaugeObserver(EnemyTargetController enemyTargetController)
    {
        _enemyTargetController = enemyTargetController;
        _hpGaugePrefab = NrcResourceManager.GetGameObject(ResourceDefine.PREFAB_ENEMY_TARGET_HP_GAUGE) as GameObject;

        _hpGauge = GameObject.Instantiate(_hpGaugePrefab,
        NrcGameManager.NrcGameStageService.GetNowStageController().transform);

        Slider slider = _hpGauge.transform.Find("HpSlider").GetComponent<Slider>();
        slider.value = (float)_enemyTargetController.Hp / (float)_enemyTargetController.MaxHp;

        _enemyTargetController.EnemyTargetDamageService.EnemyTargetHpChangeEvent += (hp) =>
        {
            slider.value = (float)hp / (float)_enemyTargetController.MaxHp;
            if (hp == 0)
            {
                GameObject.Destroy(_hpGauge);
                _iDisposable.Dispose();
            }
        };

    }

    public void BeginningAsync()
    {
        _iDisposable = Observable.FromCoroutine(Coroutine).Subscribe();
    }

    public void Pause()
    {
        if (_iDisposable != null)
            _iDisposable.Dispose();
    }

    private IEnumerator Coroutine()
    {
        while (true)
        {
            if (_hpGauge == null) yield break;
            _hpGauge.transform.position = _enemyTargetController.transform.position + new Vector3(0, 1, 2);
            _hpGauge.transform.LookAt(NrcGameManager.GetActiveCamera().transform);
            yield return null;
        }
    }
}
