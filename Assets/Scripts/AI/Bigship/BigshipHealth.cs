using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigshipHealth : Killable {

	Bigship _bs;

	// Use this for initialization
	void Start () {
		_bs = this.GetComponentInParent<Bigship> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public override void OnKilled ()
	{

	}

}
