using UnityEngine;
using System.Collections;

public class NrcGameCameraService
{
    public enum CAMERA_MODE
    {
        CAMERA_MODE_MAIN,
        CAMERA_MODE_FIRST_PERSON
    }

    private PlayerController _playerController;
    private NrcSceneLoader _nrcSceneLoader;
    private CAMERA_MODE _mode;

    public NrcGameCameraService(NrcSceneLoader nrcSceneLoader)
    {
        _playerController = nrcSceneLoader.PlayerController;
        _nrcSceneLoader = nrcSceneLoader;
        //_mode = CAMERA_MODE.CAMERA_MODE_MAIN;
        //Change(_mode);
    }

    public void Change(CAMERA_MODE changeMode)
    {
        if (changeMode == CAMERA_MODE.CAMERA_MODE_MAIN)
        {
            _nrcSceneLoader.MainCamera.gameObject.SetActive(true);
            _nrcSceneLoader.FirstPersonCamera.gameObject.SetActive(false);
            _playerController.Laser.SetActive(true);
        }
        else if (changeMode == CAMERA_MODE.CAMERA_MODE_FIRST_PERSON)
        {
            _nrcSceneLoader.FirstPersonCamera.gameObject.SetActive(true);
            _nrcSceneLoader.MainCamera.gameObject.SetActive(false);
            _playerController.Laser.SetActive(false);
        }

    }

    public CAMERA_MODE Change()
    {
        if (_mode == CAMERA_MODE.CAMERA_MODE_FIRST_PERSON)
        {
            _mode = CAMERA_MODE.CAMERA_MODE_MAIN;
            Change(_mode);
        }
        else if(_mode == CAMERA_MODE.CAMERA_MODE_MAIN)
        {
            _mode = CAMERA_MODE.CAMERA_MODE_FIRST_PERSON;
            Change(_mode);
        }

        return _mode;
    }

    public Camera GetActiveCamera()
    {
        if (_mode == CAMERA_MODE.CAMERA_MODE_FIRST_PERSON)
        {
            return _nrcSceneLoader.FirstPersonCamera;
        }
        else if (_mode == CAMERA_MODE.CAMERA_MODE_MAIN)
        {
            return _nrcSceneLoader.MainCamera;
        }
        return null;
    }


}
