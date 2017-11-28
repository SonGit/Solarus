using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipAI : MonoBehaviour {
	
	AIFlightSystem _flightSystem;

	void Start()
	{
		_flightSystem = this.GetComponent<AIFlightSystem> ();
	
	}

	void Update()
	{
		Arc ();
	}

	void TurnBack()
	{
		_flightSystem.PositionTarget = -transform.forward * 1000;
	}

	void Arc()
	{
		_flightSystem.PositionTarget = transform.forward + new Vector3(0,10,0) * 1000;
	}

}
