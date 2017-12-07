using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
	
	//Specify desired raycast thickness.
	private float _thickness = 20f;
	//Specify max distance for lockon.
	private float _lockDistance = 250f;

	public GameObject Target;

	private Chaingun[] _guns;

	float _timeCount = 0;

	// Use this for initialization
	void Start () {
//		flightControl = this.GetComponent<AeroplaneAiControl> ();
//		flightControl.SetTarget (Target.transform);

		_guns = this.GetComponentsInChildren<Chaingun> ();
	}
	
	void FixedUpdate ()
	{
		Vector3 origin = transform.position;
		Vector3 direction = transform.TransformDirection(Vector3.forward *2);
		RaycastHit hitInfo;

		if (Physics.SphereCast (origin, _thickness, direction, out hitInfo, _lockDistance)) {
			
			GameObject go = hitInfo.transform.gameObject;

			// if object in sight is the same as target, fire..
			if (Target == go) {
				Fire ();
			} 
		
		}

		if (Target != null) {

		}
	}

	void Update ()
	{

		transform.rotation = Quaternion.Slerp( transform.rotation, Quaternion.LookRotation( Target.transform.position - transform.position ), Time.deltaTime );
		transform.position += transform.forward * Time.deltaTime * 10 ;
	}

	private void Fire()
	{
		//foreach (Chaingun gun in _guns)
		//	gun.Fire ();
	}
}
