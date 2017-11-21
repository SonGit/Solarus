using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour {

	private Quaternion localRotation; // 
	public float speed = 1.0f; // ajustable speed from Inspector in Unity editor

	// Use this for initialization
	void Start () 
	{
		Application.targetFrameRate = 60;
		Input.gyro.enabled = true;
	}


	void Update() // runs 60 fps or so
	{
		transform.Rotate (-Input.gyro.rotationRateUnbiased.x * 1.35f, -Input.gyro.rotationRateUnbiased.y* 1.35f, 0);
	}
}
