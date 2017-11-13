using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSparks : Cacheable {

	private ParticleSystem _particle;

	private float _timeCount = 0;

	void Awake()
	{
		_particle = this.GetComponentInChildren<ParticleSystem> ();
	}

	void Update()
	{
		if (_living) {

			_timeCount += Time.deltaTime;

			if (_timeCount > _particle.main.duration) {
				Destroy ();
			}

		}
	}

	public override void OnDestroy ()
	{
		gameObject.SetActive (false);
	}

	public override void OnLive ()
	{
		gameObject.SetActive (true);
		_timeCount = 0;
		_particle.gameObject.SetActive (true);
		_particle.Play ();
	}


}
