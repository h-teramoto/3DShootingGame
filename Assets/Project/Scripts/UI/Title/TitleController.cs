using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour {

    [SerializeField]
    private Button _startButton;
    public Button StartButton { get { return _startButton; } }

	// Use this for initialization
	void Start () {

        TitleStartButtonObserver _titleStartButtonObserver = new TitleStartButtonObserver(this);
        _titleStartButtonObserver.Observe();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
