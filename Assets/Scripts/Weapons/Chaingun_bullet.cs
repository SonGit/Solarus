using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaingun_bullet : Cacheable {

	Transform t;

	public float _damage;

	public float _timeToLive = 1.5f; //maximum time before object disappear

	public float _timeCount = 0;

	private Vector3 _initPos;

	// Use this for initialization
	void Awake () {
		t = transform;
	}
	
	// Update is called once per frame
	void Update () {

		if (_living) {
			
			t.position += t.forward * Time.deltaTime * 400;

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
			print ("SS");
			//Create a hit spark particle and activate it
			HitSparks hs = ObjectPool.instance.GetHitSpark ();
			hs.transform.position = t.position;
			hs.Live ();
			//Rotate spark
			hs.transform.LookAt(_initPos);
				
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
	}
}
