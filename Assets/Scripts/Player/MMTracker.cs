using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMTracker : Cacheable {

	public Transform t;

	void Awake()
	{
		t = transform;
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
