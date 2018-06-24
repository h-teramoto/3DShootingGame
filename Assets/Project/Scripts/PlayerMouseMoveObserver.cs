using UnityEngine;
using UnityEditor;
using UniRx;
using System.Collections;
using System;

/// <summary>
/// 
/// </summary>
public class PlayerMouseMoveObserver
{
    private PlayerController _playerController;
    
    // 回転速度
    public Vector2 _rotationSpeed;
    
    // マウス移動方向とカメラ回転方向を反転する判定フラグ
    public bool _reverse;
    
    // マウス座標
    private Vector2 _lastMousePosition;

    // 角度（初期値に0,0を代入）
    private Vector2 _newAngle = new Vector2(0, 0);

    private GameObject _houdai;

    private GameObject _startPoint;

    public PlayerMouseMoveObserver(PlayerController playerController)
    {
        _playerController = playerController;
        _rotationSpeed = new Vector2(0.4f,0.4f);
        _reverse = false;
        _houdai = _playerController.transform.Find("Houdai").gameObject;
        _startPoint = _houdai.transform.Find("Cylinder/StartPoint").gameObject;

    }

    public void Observe()
    {
        Observable.FromCoroutine(Coroutine).Subscribe();
    }

    private IEnumerator Coroutine()
    {
        while (true)
        {

            // 左クリックした時
            if (Input.GetMouseButtonDown(0))
            {
                _newAngle = _houdai.transform.localEulerAngles;
                _lastMousePosition = Input.mousePosition;
            }
            // 左ドラッグしている間
            else if (Input.GetMouseButton(0))
            {
                if (!_reverse)
                {
                    _newAngle.y -= (_lastMousePosition.x - Input.mousePosition.x) * _rotationSpeed.y;
                    _newAngle.x -= (Input.mousePosition.y - _lastMousePosition.y) * _rotationSpeed.x;
                    _houdai.transform.localEulerAngles = _newAngle;
                    _lastMousePosition = Input.mousePosition;
                }
                else if (_reverse)
                {
                    _newAngle.y -= (Input.mousePosition.x - _lastMousePosition.x) * _rotationSpeed.y;
                    _newAngle.x -= (_lastMousePosition.y - Input.mousePosition.y) * _rotationSpeed.x;
                    _houdai.transform.localEulerAngles = _newAngle;
                    _lastMousePosition = Input.mousePosition;
                }
            }

            yield return null;
        }
    }

}