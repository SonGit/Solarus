using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWin : MonoBehaviour {

	public Text _missionWinText;


	// Use this for initialization
	void Start () {
		_missionWinText.enabled = false;
	}

	public void ShowWin()
	{
		_missionWinText.enabled = true;
		StartCoroutine (Flashing());
	}
	
	IEnumerator Flashing()
	{
		Color _prevColor = _missionWinText.color;
		float time = .5f;
		while(true)
		{
			_missionWinText.CrossFadeColor (new Color(0,0,0,0),time,false,true);
			yield return new WaitForSeconds (time);
			_missionWinText.CrossFadeColor (_prevColor,time,false,true);
			yield return new WaitForSeconds (time);
		}
	}
}
