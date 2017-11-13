using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaingun_bullet : Cacheable {

	Transform t;

	public float _damage;

	public float _timeToLive = 1f; //maximum time before object disappear

	public float _timeCount = 0;

	private Vector3 _initPos;

	private float _speed = 400;

	// Use this for initialization
	void Awake () {
		t = transform;
	}
	
	// Update is called once per frame
	void Update () {

		if (_living) {
			
			t.position += t.forward * Time.deltaTime * _speed;

			if (_timeCount > .1f)
				_speed = 2000;

			_timeCount += Time.deltaTime;
			
			if (_timeCount >= _timeToLive)
				Destroy ();

		}

	}

	void OnTriggerEnter(Collider collider)
	{
//		print (_timeCount);
		if (_timeCount < .04f)
			return;
		
		Killable killable = collider.GetComponent<Killable> ();

		if (killable != null) {
	
			//Create a hit spark particle and activate it
			HitSparks hs = ObjectPool.instance.GetHitSpark ();
			hs.transform.position = t.position;
			hs.Live ();
			//Rotate spark
			hs.transform.LookAt(_initPos);
			print ("CAUSED");
			//Cause damage
			killable.OnHit (50);

			Destroy ();
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
		_initPos = t.position;
		_speed = 400;
	}
}
