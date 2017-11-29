using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigshipHealth : Killable {

	Bigship _bs;

	public GameObject _explode;

	Turret[] _turrets;

	// Use this for initialization
	void Start () {
		_bs = this.GetComponentInParent<Bigship> ();

		_turrets = this.GetComponentsInChildren<Turret> ();

		_faction = _bs._faction;

		foreach(Turret turret in _turrets)
		{
			turret._faction = _bs._faction;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public override void OnKilled ()
	{
		if(_explode != null)
		_explode.SetActive (true);

		_bs.stop = true;

		if (_faction == Faction.ENEMY) {
			print ("WIN");
			PlayerController.instance.Win ();
		}

		if (_faction == Faction.ALLY) {
			print ("LOSE");
		}
	}

}
