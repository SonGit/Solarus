using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clamp : MonoBehaviour {
	Transform t;
	Transform cameraT;
	bool _isAlly;
	Plane_AI ai;

	// Use this for initialization
	void Start () {
		t = transform;
		cameraT = Camera.main.transform;

		ai = this.GetComponentInParent<Plane_AI> ();
	}

	float _distance = 0;
	float _scale = 1;
	public float testNum = 0;
	public bool special;
	// Update is called once per frame
	void FixedUpdate () {
		// If enemy, show targeting
		if(ai != null)
		if (!FactionRelationshipManager.IsHostile (ai._faction,Faction.PLAYER)) {
			t.localScale = Vector3.zero;
			return;
		}
		
		t.LookAt(transform.position +cameraT.rotation * Vector3.forward,
			cameraT.rotation * Vector3.up);
//		print (Vector3.Distance(t.position,cameraT.position));

		_distance = Vector3.Distance(t.position,cameraT.position);

		if (special)
		if (_distance < 3000f) {
			t.localScale = Vector3.zero;
		} else {
			t.localScale = Vector3.one;
		}

		//if (_distance > 110f) {

		//	_scale = _distance / testNum;
			//t.localScale = new Vector3 (_scale, _scale, _scale);

		//} //else {
			//_scale = 1;
			//t.localScale = new Vector3 (_scale, _scale, _scale);
		//}
	}
}
