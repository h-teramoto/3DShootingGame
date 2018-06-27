using UnityEngine;
using System.Collections;

public class StageController : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPoint;

    private PlayerController _playerController;

    public delegate void StageClearDelegate(StageController stageController);
    public StageClearDelegate stageClearEvent = delegate { };

    // Use this for initialization
    void Start()
    {
        _playerController = NrcGameManager.GetPlayerController();
        _playerController.transform.position = _playerPoint.transform.position;
   }

    // Update is called once per frame
    void Update()
    {

    }
}
