using UnityEngine;
using System.Collections;
using UniRx;
using System;

public class PlayerShootObserver : MonoBehaviour
{
    private PlayerController _playerController;

    private PlayerShootService _playerShootService;

    private GameObject _scope;
    private GameObject Scope
    {
        get
        {
            if (_scope == null)
            {
                _scope = GameObject.Instantiate(NrcResourceManager.GetGameObject(ResourceDefine.PREFAB_SCOPE2) as GameObject);
            }
            return _scope;
        }
    }

    private IDisposable _iDisposable;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="playerController"></param>
    public PlayerShootObserver(PlayerController playerController)
    {
        _playerController = playerController;
        _playerShootService = new PlayerShootService(_playerController);
   }

    /// <summary>
    /// 
    /// </summary>
    public void ObserveAsync()
    {
        _iDisposable =  Observable.FromCoroutine(Coroutine).Subscribe();
    }

    /// <summary>
    /// 
    /// </summary>
    public void Destroy()
    {
        if(_iDisposable != null)
            _iDisposable.Dispose();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isDisp"></param>
    private void ScopeDisp(bool isDisp)
    {
        Scope.SetActive(isDisp);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isDisp"></param>
    /// <param name="position"></param>
    private void ScopeDisp(bool isDisp,Vector3 position)
    {
        ScopeDisp(isDisp);
        Scope.transform.position = position;
        Scope.transform.LookAt(NrcGameManager.GetActiveCamera().transform);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator Coroutine()
    {
        while (true)
        {
            Ray ray = new Ray(_playerController.StartPoint.transform.position, _playerController.Houdai.transform.forward);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 30.0f, Color.red, 0.0f);

            int layerNo = LayerMask.NameToLayer("Bullet");
            int layerMask = ~(1 << layerNo);

            if (Physics.Raycast(ray, out hit, 30.0f, layerMask))
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

            if (Input.GetKey(KeyCode.A) || Input.GetMouseButton(1))
            {
                _playerShootService.Shoot();
                yield return null;
            }

            yield return null;
        }
    }
}
