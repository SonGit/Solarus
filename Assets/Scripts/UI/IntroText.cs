using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroText : MonoBehaviour {
	public Accelerometer _acc;
	public RectTransform _debugMenu;
	public Typewriter[] texts1;
	public Typewriter killtext;
	public ParticleSystem particle;
	public ParticleSystem[]  fogParticles;
	Transform _pc;

	// Use this for initialization
	IEnumerator Start () {
		_acc.enabled = false;
		_debugMenu.gameObject.SetActive (false);
		_pc = PlayerController.instance.transform;
		PlayerController.instance.enabled = false;
		_speed = 50;
		_pc.position = new Vector3 (3000,3000,3000);
		foreach (Typewriter text in texts1)
		{
			text.Play ();
			yield return new WaitForSeconds (2f);
		}

		foreach (Typewriter text in texts1)
		{
			text.Fade ();
			yield return new WaitForSeconds (1);
		}

		TurnFogParticle (false);

		particle.Play ();
		killtext.gameObject.SetActive (true);
		killtext.Play ();
		isThrusting = true;
		yield return new WaitForSeconds (5);
		killtext.Fade ();
		var emission = particle.emission;
		emission.rateOverTime = 9000;
		//yield return new WaitForSeconds (2);

		yield return new WaitForSeconds (2);
		_speed = 0;
		isThrusting = false;
		_pc.position = new Vector3 (-1547,292,-1287);
		_pc.eulerAngles = Vector3.zero;
		PlayerController.instance.enabled = true;
		_debugMenu.gameObject.SetActive (true);
		_acc.enabled = true;
		TurnFogParticle(true);
	}

	public float _speed = 0;
	bool isThrusting;
	// Update is called once per frame
	void Update () {
		_pc.position += _pc.forward * Time.deltaTime * _speed ;

		if (isThrusting)
			_speed += 55;
	}

	void TurnFogParticle(bool val)
	{
		foreach (ParticleSystem ps in fogParticles)
		{
		//	ps.gameObject.SetActive (val);
			if (!val)
				ps.transform.localPosition = new Vector3 (9099, 9099, 9990);
			else
				ps.transform.localPosition = Vector3.zero;
		}
	}
}
