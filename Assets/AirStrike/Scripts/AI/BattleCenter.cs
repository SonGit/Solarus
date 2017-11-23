/// <summary>
/// Battle center. Add this script to any object that you wanted using as a Battle center area.
/// </summary> 

using UnityEngine;
using System.Collections;

public class BattleCenter : Cacheable {
	
	// if true will don't let AI flying lower than this battle center position
	public bool FixedFloor = true;
	
	void Start () {
	
	}

	public override void OnDestroy ()
	{
		gameObject.SetActive (false);
	}

	public override void OnLive ()
	{
		gameObject.SetActive (true);
	}
}
