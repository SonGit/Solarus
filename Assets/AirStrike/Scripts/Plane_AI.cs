using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AIController))]

public class Plane_AI : Killable {
	void Start () {
	}

	public override void OnKilled ()
	{
		print ("KILLED");
	}
}
