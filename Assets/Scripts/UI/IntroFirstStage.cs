using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroFirstStage : MonoBehaviour {

	[SerializeField]
	private SpriteRenderer blackBG;
	[SerializeField]
	private SpriteRenderer text;
	[SerializeField]
	private float minimum = 0.0f;
	[SerializeField]
	private float maximum = 1f;
	[SerializeField]
	public float speed = 5.0f;
	[SerializeField]
	public float threshold = float.Epsilon;

	public IntroSecondStage secondStage;

	public AudioSource flashSound;

	// Use this for initialization
	void Start () {
		UnityEngine.XR.InputTracking.Recenter();
		Play ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void Play()
	{
		StartCoroutine (FirstStage());
	}

	IEnumerator FirstStage()
	{
		blackBG.color = new Color(0f, 0f, 0f, 1f);
		yield return FadeIn (text);
		yield return new WaitForSeconds(6);
		yield return FadeOut (text);
		flashSound.Play ();
		yield return new WaitForSeconds(.5f);
		yield return FadeOut (blackBG);
		secondStage.enabled = true;
		yield return null;
	}

	IEnumerator FadeIn(SpriteRenderer sprite)
	{
		while (true) {
			float step = speed * Time.deltaTime;
			sprite.color = new Color(1f, 1f, 1f, Mathf.Lerp(sprite.color.a, maximum, step));

			if (sprite.color.a > 0.99f) {
				yield break;
			}
	
			yield return new WaitForEndOfFrame ();
		}
	}

	IEnumerator FadeOut(SpriteRenderer sprite)
	{
		while (true) {
			float step = speed * Time.deltaTime;
			sprite.color = new Color(1f, 1f, 1f, Mathf.Lerp(sprite.color.a, minimum, step));

			if (sprite.color.a <= 0.01) {
				yield break;
			}

			yield return new WaitForEndOfFrame ();
		}
	}
}
