using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Killable: MonoBehaviour {

	public float _hitPoints = 100;
	public float _resistance = 0; //resistance value in %

	public void OnHit(float damage)
	{
		//In case resistance value is invalid
		if (_resistance > 100 || _resistance < 0) {
			print ("Resistance value invalid!");
			_resistance = 0;
		}
		
		_hitPoints -= (damage - (damage * _resistance)/100);

		if (_hitPoints <= 0) {
			OnKilled ();
			_hitPoints = 100;
		}
			
	}

	public abstract void OnKilled ();

	void RemoveFromMinimap()
	{

	}

}
