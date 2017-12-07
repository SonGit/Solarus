using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleOnRadar : MonoBehaviour {

	private MMTracker _tracker;
	private Minimap _minimap;
	private Transform t;
	public bool _isVisible;

	// Use this for initialization
	void Start () {
		_minimap = Minimap.instance;
		t = transform;
	}

	void FixedUpdate()
	{
		if(_minimap != null)
		if (!_minimap.IsInRadius (t.position)) {
			if(_tracker != null)
			_tracker.Destroy ();
		}
	}

	public bool isVisible()
	{
		return _isVisible;
	}

	public bool isPaired()
	{
		return _tracker == null;
	}

	public void Pair(MMTracker tracker)
	{
		_tracker = tracker;
		_tracker._tracking = transform;
	}

	public MMTracker GetTracker()
	{
		return _tracker;
	}

	public void RemoveTracker()
	{
		if(_tracker != null)
			_tracker.Destroy ();
	}

	public void SetVisible(bool val)
	{
		_isVisible = val;

		if (!_isVisible)
			RemoveTracker ();
	}


}
