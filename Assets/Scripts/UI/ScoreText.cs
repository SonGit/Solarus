using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

	public enum ScoreType
	{
		None,
		Hit,
		Kill
	}
	
	public float _liveTime = 2;

	public bool _isAvailable;

	public ScoreType _scoreType;

	public float _score = 0;

	public Text _numText;

	public Text _textComponent;

	float _timeCount = 0;

	Color _initColor;

	// Use this for initialization
	void Start () {
		
		_initColor = _textComponent.color;
		_numText.enabled = false;
		_textComponent.enabled = false;

	}

	public void AddScore(int score)
	{
		EnableText ();

		_score += score;
		_numText.text = "+" + _score;

		if (_scoreType == ScoreType.Kill) {
			_textComponent.text = "ENEMY KILLED";
		} else {
			_textComponent.text = "ENEMY HIT";
		}

		StartCoroutine (ScaleUpDown());
	}

	IEnumerator ScaleUpDown()
	{
		iTween.ScaleTo (_numText.gameObject, 
			iTween.Hash 
			("scale", new Vector3(1.5f,1.5f,1.5f),
				"speed", 5f, 
				"easetype", iTween.EaseType.linear));

		yield return new WaitForSeconds (.5f);

		iTween.ScaleTo (_numText.gameObject, 
			iTween.Hash 
			("scale", new Vector3(1f,1f,1f),
				"speed", 5f, 
				"easetype", iTween.EaseType.linear));
	}

	public void TurnOff()
	{
		StartCoroutine (TurnOff_sequence());
	}

	IEnumerator TurnOff_sequence()
	{
		_textComponent.CrossFadeColor (new Color(0,0,0,0),.5f,true,true);
		_numText.CrossFadeColor (new Color(0,0,0,0),.5f,true,true);

		yield return new WaitForSeconds (.5f);

		DisableText ();
		ResetState ();
	}

	void EnableText()
	{
		_numText.enabled = true;
		_textComponent.enabled = true;
	}

	void DisableText()
	{
		_numText.enabled = false;
		_textComponent.enabled = false;
	}

	void ResetState()
	{
		_score = 0;
		_scoreType = ScoreType.None;
		_textComponent.color = _initColor;
		_numText.color = _initColor;
	}


}
