/// <summary>
/// Player controller.
/// </summary>
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Collections.Generic;


public class PlayerController : Killable
{
	// Specify the roll rate (multiplier for rolling the ship when steering left/right)	
	public float rollRate = 100.0f;
	// Specify the yaw rate (multiplier for rudder/steering the ship when steering left/right)
	public float yawRate = 30.0f;
	// Specify the pitch rate (multiplier for pitch when steering up/down)
	public float pitchRate = 100.0f;
	// Specify the speed (multiplier for pitch when steering up/down)
	private float speed;

	public float _speed
	{
		get {
			return speed;
		}
		set {
			speed = value;
			OnSpeedChanged ();
		}
	}
	// Private variables
	private Rigidbody _cacheRigidbody;
	// Cache Transform for performance reasons
	private Transform t;
	// Reference to camera shake for cinema
	private CameraShake _cameraShake;
	// Reference to guns
	private Chaingun[] _guns;
	// is player thrusting?
	private bool isThrusting;

	private bool _isThrusting
	{
		get 
		{
			return isThrusting;
		}

		set 
		{
			isThrusting = value;

			if (_cameraShake != null) 
			{
				if (isThrusting) {
					_cameraShake.RumblingHigh ();
				} else {
					_cameraShake.RumblingNormal ();
				}
			}
		}
	}

	private bool _isFiring;
	private bool _isDecelerating;

	public static PlayerController instance;

	public event Action OnHit = delegate {};
	public event Action OnHPChanged = delegate {};
	public event Action OnSpeedChanged = delegate {};

	public BattleCenter _battleCenter;
	public float _distanceFromBattleCenter = 0;

	public OOB _OOBUI;

	[SerializeField]
	private PlayerWin _playerWin;

	Transform mainCamT;

	void Awake()
	{
		instance = this;
	}

	void Start () {		
		// Cache reference to rigidbody to improve performance
		_cacheRigidbody = GetComponent<Rigidbody>();
		if (_cacheRigidbody == null) {
			Debug.LogError("Spaceship has no rigidbody.Add rigidbody component to the spaceship.");
		}
		t = transform;
		_cameraShake = GetComponentInChildren<CameraShake> ();
		_guns = GetComponentsInChildren<Chaingun> ();

		_isThrusting = false;

		mainCamT = Camera.main.transform;

		UnityEngine.XR.InputTracking.Recenter();

		_trackOOB = false;

		CreateField ();
	}
	public bool stop;
	void Update () {

		if (Input.GetKeyDown (KeyCode.S)) {
			_isDecelerating = true;
		}

		if (Input.GetKeyUp (KeyCode.S)) {
			_isDecelerating = false;
		}

		if (Input.GetKeyDown (KeyCode.W)) {
			_isThrusting = true;
		}

		if (Input.GetKeyUp (KeyCode.W)) {
			_isThrusting = false;
		}

		if (_isThrusting) {
			_speed += 1;
		}

		if (_isDecelerating) {
			_speed -= 1;
		}

		if ( Input.GetKeyDown(KeyCode.R)) {
			Fire ();
		}

		if ( Input.GetKeyUp(KeyCode.R)) {
			_isFiring = false;
		}

		if (OVRInput.Get (OVRInput.Axis1D.SecondaryIndexTrigger) > 0 || Input.GetMouseButtonDown(0)) {
			_isFiring = true;
		}

		if (OVRInput.Get (OVRInput.Axis1D.SecondaryIndexTrigger) == 0|| Input.GetMouseButtonUp(0)) {
			_isFiring = false;
		}

		if (_isFiring)
			Fire ();

		if (_speed < 50)
			_speed = 50;

		if (_speed > 200)
			_speed = 200;

		if(!stop)
		t.position += t.forward * Time.deltaTime * _speed ;
		
		if(_trackOOB)
		TrackOOB ();
	}

	public float OOBTime = 10;
	float currentOOBTime = 0;
	public bool _trackOOB;

	void TrackOOB()
	{
		_distanceFromBattleCenter = Vector3.Distance (t.position,_battleCenter.transform.position);

		if (_distanceFromBattleCenter > 6500) {

			currentOOBTime -= Time.deltaTime;

			_OOBUI.ShowTime ((int)currentOOBTime);

			if (currentOOBTime <= 0) {

				currentOOBTime = OOBTime;
				_OOBUI.StopShowTime ();
				t.position = _battleCenter.transform.position;

			}

		} else {
			currentOOBTime = OOBTime;
			_OOBUI.StopShowTime ();
		}
	}


	void FixedUpdate () {
		Quaternion AddRot = Quaternion.identity;
		float roll = 0;
		float pitch = 0;
		float yaw = 0;

		Vector2 rightVal = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

		if(OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger) > 0)
			roll = -OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger) * (Time.deltaTime * rollRate);
		if(OVRInput.Get (OVRInput.Axis1D.SecondaryHandTrigger) > 0)
			roll = OVRInput.Get (OVRInput.Axis1D.SecondaryHandTrigger) * (Time.deltaTime * rollRate);

//		print (OVRInput.Get (OVRInput.Axis1D.PrimaryIndexTrigger));
//		print (OVRInput.Get (OVRInput.Axis1D.SecondaryIndexTrigger));
		pitch = rightVal.y * (Time.deltaTime * pitchRate);
		yaw =  rightVal.x * (Time.deltaTime * yawRate);

		Vector2 leftVal = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
		_speed += leftVal.y * .5f;

		Quaternion targetRotation = transform.rotation * Quaternion.Euler(new Vector3(-pitch, yaw, -roll));
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 100 * Time.deltaTime);

	}

	void Fire()
	{
		for (int i = 0; i < _guns.Length; i++) {
			_guns [i].Fire (t.position + t.forward * 1000);
		}
	}

	public void Accelerate()
	{
		_isThrusting = true;
	}

	public void EndAccelerate()
	{
		_isThrusting = false;
	}

	public void Decelerate()
	{
		_isDecelerating = true;
	}

	public void EndDecelerate()
	{
		_isDecelerating = false;
	}
	public void Firing()
	{
		_isFiring = true;
	}

	public void EndFiring()
	{
		_isFiring = false;
	}

	public override void OnHitAdditional()
	{
		_cameraShake.ShakeHit ();

		OnHPChanged ();
	}

	private void OnTriggerEnter(Collider collision)
	{

	}

	public override void OnKilled ()
	{
		stop = true;

		ExplosionObject explosion = ObjectPool.instance.GetExplosionObject ();

		explosion.transform.position = transform.position;
		explosion.transform.localScale = new Vector3 (10,10,10);
		explosion.Live ();

		_playerWin.Play ();
	}

	public void Win()
	{
		_playerWin.Play ();
	}
		
}
