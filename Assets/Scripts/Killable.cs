using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Killable: MonoBehaviour {

	//Events
	public delegate void BeingHit ();
	public static event BeingHit onHitEvent;// When got hit
	public delegate void BeingKilled ();
	public static event BeingKilled onKilledEvent;// When got killed

	public float _hitPoints = 100;
	public float _resistance = 0; //resistance value in %

	public void OnHit(float damage, bool isHitByPlayer = false)
	{
		//In case resistance value is invalid
		if (_resistance > 100 || _resistance < 0) {
			print ("Resistance value invalid!");
			_resistance = 0;
		}
		
		_hitPoints -= (damage - (damage * _resistance)/100);

		if (_hitPoints <= 0) {
			OnKilled ();
			RemoveFromMinimap ();

			if(isHitByPlayer)
			{
				PlayerScore.instance.OnKilledEnemy ();
			}
		
			_hitPoints = 100;
		
		} else {
			if(isHitByPlayer)
			{
				PlayerScore.instance.OnHitEnemy ();
			}
		}

	}

	public abstract void OnKilled ();

	public void RemoveFromMinimap()
	{
		VisibleOnRadar visible = this.GetComponent<VisibleOnRadar> ();

		if (visible != null) {
			visible.RemoveTracker ();
		}
	}

}
