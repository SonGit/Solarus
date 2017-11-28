using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigshipHealth : Killable {

	Bigship _bs;

	public GameObject _explode;

	// Use this for initialization
	void Start () {
		_bs = this.GetComponentInParent<Bigship> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public override void OnKilled ()
	{
		if(_explode != null)
		_explode.SetActive (true);

		if (_faction == Faction.ENEMY) {
			print ("WIN");
		}

		if (_faction == Faction.ALLY) {
			print ("LOSE");
		}
	}

}
