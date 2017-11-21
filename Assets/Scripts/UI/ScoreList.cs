using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreList : MonoBehaviour {

	public GridLayoutGroup _grid;

	public float _liveTime = 1;
	float _timeCount = 0;


	float _cellHeight;

	List<string> _scTexts = new List<string>();

	// Use this for initialization
	void Start () {
		StartCoroutine (Push());
	}
	
	// Update is called once per frame
	void Update () {


		//string text = _scTexts[0];
		//_scTexts.RemoveAt (0);
	}

	IEnumerator Push()
	{
		while (true) {

			if (_scTexts.Count != 0 && _scTexts.Count % 2 == 0) {
				print ("(_scTexts.Count" + _scTexts.Count);
				for (int i = 0; i < 2; i++) {
					string text = _scTexts [i];
				
					ScoreText scText = MakeText ();
					scText.SetText (text);
					//yield return new WaitForSeconds (.25f);
				}

				_scTexts.RemoveRange (0,2);
				yield return new WaitForSeconds (2.5f);
			} 

			if (_scTexts.Count == 1 ) {
				string text = _scTexts [0];

				ScoreText scText = MakeText ();
				scText.SetText (text);
				_scTexts.RemoveAt (0);
				yield return new WaitForSeconds (2.5f);
			}




			yield return new WaitForEndOfFrame ();
		}
	}

	public void ShowKillScore()
	{
		_scTexts.Add ("ENEMY KILLED +100");
		//ScoreText scText = MakeText ();
		//scText.SetText ("Enemy Killed +100");
	}

	public void ShowHitScore()
	{
		//_scTexts.Add ("ENEMY HIT +100");
		//ScoreText scText = MakeText ();
		//scText.SetText ("Enemy Hit +100");
	}
	ScoreText MakeText()
	{
		ScoreText text = ObjectPool.instance.GetScoreText ();
		text.transform.parent = _grid.transform ;
		text.transform.localPosition = Vector3.zero;
		text.transform.localEulerAngles = Vector3.zero;
		text.transform.localScale = Vector3.one;
		text.Live ();
		return text;
	}
		
}
