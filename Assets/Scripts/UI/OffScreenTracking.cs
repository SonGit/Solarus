using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenTracking : MonoBehaviour {

	public Faction _faction;
	public bool _permanent;
	// Use this for initialization
	void Start () {
		
		_faction = Faction.NEUTRAL;

		Killable chars = GetComponent<Killable>();
		if (chars != null)
			_faction = chars._faction;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
