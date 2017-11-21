using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : Cacheable {

	Text _textComponent;

	public float _liveTime = 2;

	// Use this for initialization
	void Start () {
		Init ();
	}

	public void Init()
	{
		_textComponent = this.GetComponent<Text> ();
	}

	float _timeCount = 0;

	// Update is called once per frame
	void Update () {

		if (_living) {

			_timeCount += Time.deltaTime;

			if (_timeCount >= _liveTime) {
			//	Destroy ();
				_textComponent.CrossFadeColor (new Color(0,0,0,0),.3f,true,true);
				_timeCount = 0;
				Invoke ("Destroy",.3f);
			}
				
		}
		
	}

	public void SetText(string text)
	{
		if (_textComponent != null)
			_textComponent.text = text;
		else {
			print ("_textComponent null");
		}
			
	}

	public override void OnDestroy ()
	{
		transform.parent = null;
		gameObject.SetActive (false);
	}

	public override void OnLive ()
	{
		Init ();
		gameObject.SetActive (true);
		_textComponent.color = new Color (255,255,255,255);
	}

}
