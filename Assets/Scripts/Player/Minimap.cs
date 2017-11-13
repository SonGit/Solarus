using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

	public static Minimap instance;

	// Prefab to create on Minimap
	public GameObject prefab;
	// center of the minimap
	public Transform _center;
	// Tag of obj to track
	public float _scale;
	// Tag of obj to track
	public float _radius;
	// Cached Transform
	Transform t;

	// Use this for initialization
	void Awake()
	{
		instance = this;
	}

	void Start () {
		t = transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Scan (_radius);
	}
		
	// Caching purposes
	Collider[] hitColliders;

	void Scan(float radius)
	{
		
		hitColliders = Physics.OverlapSphere(_center.position, radius);

		int i = 0;

		while (i < hitColliders.Length)
		{
			VisibleOnRadar visible = hitColliders [i].GetComponent<VisibleOnRadar> ();

			if (visible != null) {
				
//				print (Vector3.Distance(visible.transform.position,_center.position));

				MMTracker tracker;

				//Check if VisibleOnRadar is paired with a tracker... 
				if (visible.isPaired()) {

					// if VisibleOnRadar has any other conditions to be visible...
					if (!visible.isVisible ())
						break;
					
					// .. if not, create a new tracker and pair it with VisibleOnRadar
					tracker = ObjectPool.instance.GetMMTracker();
					tracker.t.parent = transform;
					tracker.Live ();
					visible.Pair (tracker);
					tracker.t.eulerAngles = Vector3.zero;
					tracker.t.localScale = Vector3.one;

				} else {
					// .. if yes, get the paired tracker
					tracker = visible.GetTracker();
					if(!tracker._living)
						tracker.Live ();
				}

				//Set tracker position on minimap
				if (tracker != null) {
					
					tracker.t.position = SetTrackerPos(visible.transform.position);
					tracker.t.localPosition = new Vector3 (tracker.t.localPosition.x , 0 , tracker.t.localPosition.z);
					tracker.t.localEulerAngles = Vector3.zero;
				}

			}

			i++;

		}

	}

	Vector3 SetTrackerPos(Vector3 targetPos)
	{
		Vector3 dir = targetPos - _center.position;

		return (_center.position + (dir * _scale));
	}

	// Check if object is outside radius
	public bool IsInRadius(Vector3 pos)
	{
		return Vector3.Distance(pos,_center.position) < _radius;
	}
		
		
}
