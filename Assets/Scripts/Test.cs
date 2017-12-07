using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : Killable {

	public GameObject _go;

	Transform _cameraTransform;

	public Camera _camera;

	public Transform arrow;

	public Canvas canvas;

	public override void OnKilled ()
	{

	}

	void Start()
	{
		_cameraTransform = _camera.transform;
		//_camera = Camera.main;
	}

	void Update()
	{
		GetAngle (_go);
	}

	void GetAngle(GameObject target)
	{
		Vector3 screenPos = _camera.WorldToScreenPoint (target.transform.position);
		Vector3 dir = _cameraTransform.InverseTransformPoint (target.transform.position);
		print("RectTransformUtility  " + RectTransformUtility.PixelAdjustPoint (screenPos,arrow,canvas));
		//print ("screenPos.x  " + screenPos.x  + " screenPos.y  " +  screenPos.y+ " screenPos.z  " + screenPos.z + " Screen.width " + Screen.width + " Screen.height " + Screen.height);
		print (" screenPos.y  " +  screenPos.y + "  Screen.height " + Screen.height);
		if (screenPos.z>0 && screenPos.x>0 && screenPos.x<Screen.width && screenPos.y>0 && screenPos.y<Screen.height)
		{
			//place gui texture on transform 
			arrow.transform.localPosition = new Vector3(999,999,999);


		} else {

			Vector3 screenCenter = new Vector3 (Screen.width,Screen.height,0)/2;
			screenPos -= screenCenter;

			if (screenPos.z < 0) {
				screenPos *= -1;
			}

			float angle = Mathf.Atan2 (screenPos.y,screenPos.x);
			angle -= 90 * Mathf.Deg2Rad;

			float cos = Mathf.Cos (angle);
			float sin = -Mathf.Sin (angle);

			screenPos = screenCenter + new Vector3 (sin*150,cos*150,0);

			float m = cos / sin;

			Vector3 screenBounds = screenCenter * .9f;

			if (cos > 0) {
				screenPos = new Vector3 (screenBounds.y / m, screenBounds.y, 0);
			} else {
				screenPos = new Vector3 (-screenBounds.y / m, -screenBounds.y, 0);
			}

			if(screenPos.x > screenBounds.x)
			{
				screenPos = new Vector3(screenBounds.x, screenBounds.x*m, 0);
			}
			else if(screenPos.x < -screenBounds.x)
			{
				screenPos = new Vector3(-screenBounds.x, -screenBounds.x*m, 0);
			}

			arrow.transform.localPosition = screenPos;
			arrow.transform.localRotation = Quaternion.Euler (0,0,angle*Mathf.Rad2Deg);

		}
		

	}


}
