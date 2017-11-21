using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreList : MonoBehaviour {

	ScoreText[] _scTexts;

	void Start()
	{
		_scTexts = GetComponentsInChildren<ScoreText> ();
	}

	public void ShowScore(ScoreText.ScoreType type,int score)
	{
		foreach (ScoreText scText in _scTexts) {
			if (scText._scoreType == type) {
				scText.AddScore (score);
				return;
			}
		}

		foreach (ScoreText scText in _scTexts) {
			if (scText._scoreType == ScoreText.ScoreType.None) {
				scText._scoreType = type;
				scText.AddScore (score);
				return;
			}
		}
	}

	public void StopShowScore(ScoreText.ScoreType type)
	{
		foreach (ScoreText scText in _scTexts) {
			if (scText._scoreType == type ) {
				scText.TurnOff ();
				return;
			}
		}
	}
}
