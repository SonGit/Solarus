using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWin : MonoBehaviour {

	public AudioSource _deathCry;
	public AudioSource _flash;
	public AudioSource _remember;

	[SerializeField]
	private float speed = 5.0f;
	[SerializeField]
	private float minimum = 0.0f;
	[SerializeField]
	private float maximum = 1f;
	[SerializeField]
	private SpriteRenderer blackBG;

	void Start()
	{
	//	Play ();
	}

	public void Play()
	{
		StartCoroutine (Play_async());
	}

	IEnumerator Play_async()
	{
		_deathCry.Play ();
		yield return new WaitForSeconds (2);
		_remember.Play ();
		yield return new WaitForSeconds (1);

		for (int i = 0; i < 4; i++) {
			_flash.Stop ();
			_flash.Play ();
			yield return FadeIn (blackBG);
			yield return FadeOut (blackBG);
		}
		yield return FadeOut (blackBG);
		yield return FadeIn (blackBG);
		Application.LoadLevel ("Test2Scene");
		//intro.Play ();
	}
		

	IEnumerator FadeIn(SpriteRenderer sprite)
	{
		sprite.color = new Color(1f, 1f, 1f, 0);
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
		sprite.color = new Color(1f, 1f, 1f, 1);
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
