using UnityEngine;
using System.Collections;
using UniRx;
using UnityEngine.UI;

public class EnemyHpGaugeObserver
{
    private EnemyController _enemyController;

    private GameObject _hpGaugePrefab;

    private EnemyDamageService _enemyDamageService;

    private Slider _slider;

    public EnemyHpGaugeObserver(EnemyController enemyController, EnemyDamageService enemyDamageService)
    {
        _enemyController = enemyController;
        _enemyDamageService = enemyDamageService;
        _hpGaugePrefab = NrcResourceManager.GetGameObject(ResourceDefine.PREFAB_ENEMY_HP_GAUGE) as GameObject;
    }

    public void Display()
    {
        Observable.FromCoroutine(Coroutine).Subscribe();
    }

    private IEnumerator Coroutine()
    {
        GameObject hpGauge = GameObject.Instantiate(_hpGaugePrefab);
        hpGauge.transform.position = _enemyController.transform.position + new Vector3(0, 1, 2);
        hpGauge.transform.LookAt(NrcGameManager.GetActiveCamera().transform);

        Slider slider = hpGauge.transform.Find("HpSlider").GetComponent<Slider>();
        slider.value = (float)_enemyController.Hp / (float)_enemyController.MaxHp;

        _enemyDamageService.EnemyHpChangeEvent += (hp) =>
        {
            slider.value = (float)hp / (float)_enemyController.MaxHp;
            if(hp == 0)
            {
                GameObject.Destroy(hpGauge);
            }
        };

        yield return null;
    }
}
