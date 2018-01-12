using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorvetteBattleship : Bigship {

	private BigshipHealth _health;

	// Use this for initialization
	IEnumerator Start () {
		_health = this.GetComponentInChildren<BigshipHealth> ();
		//_health.OnHitEvent += Blood;

		t = transform;
		_bc = this.GetComponentInChildren<BattleCenter> ();

		_planeCache = new List <FighterAI>();

		for(int i = 0 ; i < 20 ; i++)
		{
			_planeCache.Add( Create() );
			yield return new WaitForSeconds (.1f);
		}

	}
	
	// Update is called once per frame
	void Update () {
		Loop ();
	}



}
