using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMTracker : Cacheable {

	public Transform t;
	public Transform _tracking;

	void Awake()
	{
		t = transform;
	}

	void FixedUpdate()
	{

	}

	public override void OnDestroy ()
	{
		gameObject.SetActive (false);
	}
	public override void OnLive ()
	{
		gameObject.SetActive (true);
	}
		
}
