using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionRelationshipManager : MonoBehaviour {

	private static FactionRelationshipManager instance;

	private FactionRelationshipManager() {}

	public static FactionRelationshipManager Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new FactionRelationshipManager();
			}
			return instance;
		}
	}

	public static bool IsHostile(Faction faction1, Faction faction2)
	{
		// PLAYER and ALLY faction are allied
		if (faction1 == Faction.PLAYER && faction2 == Faction.ALLY) {
			return false;
		}
		if (faction1 == Faction.ALLY && faction2 == Faction.PLAYER) {
			return false;
		}

		if (faction1 != faction2)
			return true;
		else
			return false;
	}
}
