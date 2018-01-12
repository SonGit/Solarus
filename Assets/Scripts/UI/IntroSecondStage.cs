using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSecondStage : MonoBehaviour {
	public Accelerometer _acc;

	public Typewriter[] texts1;
	public Typewriter killtext;
	public ParticleSystem particle;
	public ParticleSystem[]  fogParticles;
	public GameObject _arrow;
	Transform _pc;

	public AudioSource warpSound;

	// Use this for initialization
	IEnumerator Start () {
		_acc.enabled = false;

		_pc = PlayerController.instance.transform;

		//PlayerController.instance.enabled = false;
		_speed = 50;
		_pc.position = new Vector3 (3000,3000,3000);

		yield return FirstStage();

		foreach (Typewriter text in texts1)
		{
			text.Play ();
			yield return new WaitForSeconds (4f);
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
		yield return new WaitForSeconds (1);
		warpSound.Play ();
		yield return new WaitForSeconds (4);
		killtext.Fade ();
	
		var emission = particle.emission;
		emission.rateOverTime = 9000;
		//yield return new WaitForSeconds (2);

		yield return new WaitForSeconds (2);
		_speed = 0;
		isThrusting = false;
		_pc.position = new Vector3 (321,412,-12342);
		_pc.eulerAngles = new Vector3 (0,42,0);
		PlayerController.instance.enabled = true;

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
				ps.transform.localPosition = new Vector3 (-9099, -9099, -9990);
			else
				ps.transform.localPosition = Vector3.zero;
		}
	}

	IEnumerator FirstStage()
	{
		yield return null;
	}
}
