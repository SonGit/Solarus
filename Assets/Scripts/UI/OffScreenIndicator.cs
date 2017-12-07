using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OffScreenIndicator : MonoBehaviour {
	
	public Camera _camera;

	public Transform _target;

	public RectTransform CanvasRect;

	Transform t;

	Text _distText;

	void Start () {
		t = transform;
		t.transform.localPosition = new Vector3(9999,9999,9999);
		_distText = this.GetComponentInChildren<Text> ();
	}

	float _dist = 0;
	// Update is called once per frame
	void Update () {
		if (_target != null) {
			GetAngle (_target);
		


		}

	}

	public void Track(Transform target)
	{
		if(_target == null)
		_target = target;
	}

	public void Untrack()
	{
		_target = null;
		t.transform.localPosition = new Vector3(9999,9999,9999);
	}

	void GetAngle(Transform target)
	{
		Vector3 screenPos = _camera.WorldToScreenPoint (target.position);

		//print ("screenPos.x  " + screenPos.x  + " screenPos.y  " +  screenPos.y+ " screenPos.z  " + screenPos.z + " Screen.width " + Screen.width + " Screen.height " + Screen.height);
//		print ("screenPos.x  " + screenPos.x + " screenPos.y  " +  screenPos.y + "  Screen.height " + Screen.height);
		if (screenPos.z>0 && screenPos.x>0 && screenPos.x<Screen.width && screenPos.y>0 && screenPos.y<Screen.height)
		{
			//place gui texture on transform 
			Untrack();
			return;
		} 
		else 
		{
			
			Vector3 screenCenter = new Vector3 (Screen.width,Screen.height,0)/4;
			screenPos -= screenCenter;
//			print (screenCenter);
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

			t.transform.localPosition = screenPos;
			t.transform.localRotation = Quaternion.Euler (0,0,angle*Mathf.Rad2Deg);

		}


			_dist = Vector3.Distance (_camera.transform.position,_target.transform.position);

			if (_dist > 560)
				Untrack ();
			
			_distText.text = (int)_dist + "";
			_distText.transform.localEulerAngles = new Vector3 (0,0,-transform.localEulerAngles.z);
			
	}
}
