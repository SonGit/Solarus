using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleOnRadar : MonoBehaviour {

	private MMTracker _tracker;
	private Minimap _minimap;
	private Transform t;

	// Use this for initialization
	void Start () {
		_minimap = Minimap.instance;
		t = transform;
	}

	void FixedUpdate()
	{
		if (!_minimap.IsInRadius (t.position)) {
			if(_tracker != null)
			_tracker.Destroy ();
		}
	}

	public bool isVisible()
	{
		return true;
	}

	public bool isPaired()
	{
		return _tracker == null;
	}

	public void Pair(MMTracker tracker)
	{
		_tracker = tracker;
	}

	public MMTracker GetTracker()
	{
		return _tracker;
	}

}
