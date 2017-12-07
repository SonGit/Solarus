using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenIndicatorManager : MonoBehaviour {

	OffScreenIndicator[] _indicators;

	public OffScreenTracking[] _enemyInRadius;

	// Use this for initialization
	void Start () {
		_indicators = this.GetComponentsInChildren<OffScreenIndicator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		_enemyInRadius = GameObject.FindObjectsOfType<OffScreenTracking> ();

		for (int i = 0; i < _enemyInRadius.Length; i++) {

			// If no more indicator to accomodate
			if (i > _indicators.Length - 1) {
				return;
			}

			if(FactionRelationshipManager.IsHostile(Faction.PLAYER,_enemyInRadius[i]._faction))
			_indicators [i].Track (_enemyInRadius [i].transform);


		}
			

	}

}
