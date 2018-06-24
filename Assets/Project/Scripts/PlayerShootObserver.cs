using UnityEngine;
using System.Collections;
using UniRx;

public class PlayerShootObserver : MonoBehaviour
{
    private PlayerController _playerController;

    private GameObject _houdai;

    private GameObject _startPoint;

    private GameObject _scopePrefab;

    private GameObject _scope;

    PlayerShootService _playerShootService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="playerController"></param>
    public PlayerShootObserver(PlayerController playerController)
    {
        _playerController = playerController;
        _playerShootService = new PlayerShootService(_playerController);
        _scopePrefab = Resources.Load("Prefabs/Scope2") as GameObject;
        _houdai = _playerController.transform.Find("Houdai").gameObject;
        _startPoint = _houdai.transform.Find("Cylinder/StartPoint").gameObject;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Observe()
    {
        Observable.FromCoroutine(Coroutine).Subscribe();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isDisp"></param>
    private void ScopeDisp(bool isDisp)
    {
        if (_scope == null)
        {
            _scope = GameObject.Instantiate(_scopePrefab);
        }
        _scope.SetActive(isDisp);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isDisp"></param>
    /// <param name="position"></param>
    private void ScopeDisp(bool isDisp,Vector3 position)
    {
        ScopeDisp(isDisp);
        _scope.transform.position = position;
        _scope.transform.LookAt(Camera.main.transform);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator Coroutine()
    {
        while (true)
        {
            Ray ray = new Ray(_startPoint.transform.position, _houdai.transform.forward);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 30.0f, Color.red, 0.0f);

            if (Physics.Raycast(ray, out hit, 30.0f))
            {
                if (hit.collider.tag == TagDefine.TAG_ENEMY) { 
                    ScopeDisp(true, hit.point + new Vector3(0, 0, 0.5f));
                }
                else {
                    ScopeDisp(false);
                }

            }
            else
            {
                ScopeDisp(false);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                _playerShootService.Shoot();
                yield return null;
            }

            yield return null;
        }
    }
}
