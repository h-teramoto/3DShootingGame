using UnityEngine;
using System.Collections;
using UniRx;
using UnityEngine.UI;
using System;

public class EnemyHpGaugeObserver : INrcObserver
{
    private EnemyController _enemyController;

    private GameObject _hpGaugePrefab;

    private GameObject _hpGauge;

    private Slider _slider;

    private IDisposable _iDisposable;

    public EnemyHpGaugeObserver(EnemyController enemyController)
    {
        _enemyController = enemyController;
        _hpGaugePrefab = NrcResourceManager.GetGameObject(ResourceDefine.PREFAB_ENEMY_HP_GAUGE) as GameObject;

        _hpGauge = GameObject.Instantiate(_hpGaugePrefab,
        NrcGameManager.NrcGameStageObserver.GetNowStageController().transform);

        Slider slider = _hpGauge.transform.Find("HpSlider").GetComponent<Slider>();
        slider.value = (float)_enemyController.Hp / (float)_enemyController.MaxHp;

        _enemyController.EnemyDamageService.EnemyHpChangeEvent += (hp) =>
        {
            slider.value = (float)hp / (float)_enemyController.MaxHp;
            if (hp == 0)
            {
                GameObject.Destroy(_hpGauge);
                _iDisposable.Dispose();
            }
        };
    }

    public void Pause()
    {
        if (_iDisposable != null)
            _iDisposable.Dispose();
    }

    public void BeginningAsync()
    {
        _iDisposable = Observable.FromCoroutine(Coroutine).Subscribe();
    }

    private IEnumerator Coroutine()
    {
        while (true)
        {
            if (_hpGauge == null) yield break;
            _hpGauge.transform.position = _enemyController.transform.position + new Vector3(0, 1, 2);
            _hpGauge.transform.LookAt(NrcGameManager.GetActiveCamera().transform);
            yield return null;
        }        
    }

}
