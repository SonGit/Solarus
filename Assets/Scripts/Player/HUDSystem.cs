using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach to Player GameObject

public class HUDSystem : MonoBehaviour {

	public RectTransform _targeting;

	// Use this for initialization
	void Start () {
		
	}
	
	public void Update()
	{

		float thickness = 15f; //<-- Desired thickness here.
		Vector3 origin = transform.position + transform.forward;
		Vector3 direction = transform.TransformDirection(Vector3.forward);
		RaycastHit hitInfo;

		if (Physics.SphereCast(origin, thickness, direction, out hitInfo)) {

			print (hitInfo.transform.name);


			}

	}

}
