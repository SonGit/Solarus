using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OOB : MonoBehaviour {

	public Text text1;
	public Text text2;

	// Use this for initialization
	void Start () {
		StopShowTime ();
	}

	public void ShowTime(int remainingTime)
	{
		if (!text1.enabled)
			text1.enabled = true;
		if (!text2.enabled)
			text2.enabled = true;
		
		text2.text = "RETURNING TO ORIGINAL POSITION IN ... " + remainingTime;
	}

	public void StopShowTime()
	{
		text1.enabled = false;
		text2.enabled = false;
	}
}
