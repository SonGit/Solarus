/// <summary>
/// Battle center. Add this script to any object that you wanted using as a Battle center area.
/// </summary> 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleCenter : Cacheable {
	
	// if true will don't let AI flying lower than this battle center position
	public bool FixedFloor = true;

	public List<GameObject> navWaypoints;

	void Start () {
		CreateField ();
	}

	public override void OnDestroy ()
	{
		gameObject.SetActive (false);
	}

	public override void OnLive ()
	{
		gameObject.SetActive (true);
	}

	public void CreateField()
	{
		navWaypoints = new List<GameObject>();

		// ...later, in your createField() method
		GameObject newTarget;
		float fieldWidth = 1;

		for( int i = 0; i < 7; i++ )
		{
			newTarget = new GameObject();

			newTarget.name = "Waypoint_" + i;
			// parent it so that it follows the player
			newTarget.transform.parent = transform;
			newTarget.transform.localPosition = Vector3.zero;
			newTarget.transform.localPosition = Random.insideUnitSphere * fieldWidth;

			// push into our targets array
			navWaypoints.Add(newTarget);

		}

	}
}
