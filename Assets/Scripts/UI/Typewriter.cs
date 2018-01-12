using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Typewriter : MonoBehaviour {

	Text txt;
	public string story;
	public RectTransform blip;
	public float speed;
	public float offset;

	public AudioSource clickSound;

	void Awake () 
	{
		txt = GetComponent<Text> ();
		txt.text = "";

		// TODO: add optional delay when to start
	}

	public void Play()
	{
		StartCoroutine (Play_async());
	}

	public void Fade()
	{
		StopAllCoroutines ();
		StartCoroutine (Fade_async());
	}

	IEnumerator Fade_async()
	{
		txt.CrossFadeColor (new Color(0,0,0,0), .5f, false,true);
		blip.GetComponent<Image> ().CrossFadeColor (new Color(0,0,0,0), .5f, false,true);
		yield return new WaitForSeconds (.5f);
	}
		
	IEnumerator Play_async()
	{
		yield return StartCoroutine (PlayText());
		yield return StartCoroutine (FlashBlip());
	}

	IEnumerator PlayText()
	{
		foreach (char c in story) 
		{
			if (c.Equals (' ')) {
				txt.text += "   ";
			} else {
				txt.text += c;

				float x = blip.anchoredPosition.x + offset;
				blip.anchoredPosition = new Vector2 (x,blip.anchoredPosition.y);
			}

			clickSound.Play ();
			
			yield return new WaitForSeconds (speed);
		}
	}

	IEnumerator FlashBlip()
	{
		Image img = blip.GetComponent<Image> ();
		Color prevColor = img.color;
		Color transColor = new Color(0,0,0,0);

		float rate = .2f;

		while (true) {
			img.CrossFadeColor(transColor, rate, false,true);
			yield return new WaitForSeconds(rate);
			img.CrossFadeColor(prevColor, rate, false,true);
			yield return new WaitForSeconds(rate);
		}
			
	}

}
