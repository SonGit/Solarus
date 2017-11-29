using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction
{
	NONE,
	NEUTRAL,
	ALLY,
	ENEMY,
	PLAYER,
}

public abstract class Killable: MonoBehaviour {

	public Faction _faction;

	public float _hitPoints = 100;
	public float _resistance = 0; //resistance value in %

	public void OnHit(float damage, Killable attacker)
	{
		// If friendly fire, ignore
		if (!FactionRelationshipManager.IsHostile (_faction,attacker._faction)) {
			return;
		}

		//In case resistance value is invalid
		if (_resistance > 100 || _resistance < 0) {
			print ("Resistance value invalid!");
			_resistance = 0;
		}

		// Decrease health
		_hitPoints -= (damage - (damage * _resistance)/100);

		if (_hitPoints <= 0) {
			
			Killed ();

			if (attacker._faction == Faction.PLAYER) {
				PlayerScore.instance.OnKilledEnemy ();
			}

			_hitPoints = 100;
		
		} else {
			if (attacker._faction == Faction.PLAYER) {
				PlayerScore.instance.OnHitEnemy ();
			}
		}

		OnHitAdditional ();
	}

	public abstract void OnKilled ();

	public void RemoveFromMinimap()
	{
		VisibleOnRadar visible = this.GetComponent<VisibleOnRadar> ();

		if (visible != null) {
			visible.RemoveTracker ();
		}
	}

	public virtual void OnHitAdditional()
	{

	}

	public void Killed()
	{
		OnKilled ();
		RemoveFromMinimap ();
	}

}
