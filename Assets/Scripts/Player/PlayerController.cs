/// <summary>
/// Player controller.
/// </summary>
using UnityEngine;
using System.Collections;
using UnityEngine.Events;


public class PlayerController : Killable
{
	// Specify the roll rate (multiplier for rolling the ship when steering left/right)	
	public float rollRate = 100.0f;
	// Specify the yaw rate (multiplier for rudder/steering the ship when steering left/right)
	public float yawRate = 30.0f;
	// Specify the pitch rate (multiplier for pitch when steering up/down)
	public float pitchRate = 100.0f;
	// Specify the speed (multiplier for pitch when steering up/down)
	public float _speed;
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

	public static PlayerController instance;

	public UnityEvent OnLockingEvent = new UnityEvent();

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
	}

	void Update () {

		if (Input.GetKey (KeyCode.S)) {
			_speed -= 5;

			if (_speed < 50)
				_speed = 50;
		}

		if (Input.GetKeyDown (KeyCode.W)) {
			_isThrusting = true;
		}

		if (Input.GetKeyUp (KeyCode.W)) {
			_isThrusting = false;
		}

		if (Input.GetKey (KeyCode.W)) {
			_speed += 5;

			if (_speed > 250)
				_speed = 250;
		}
			

		if (Input.GetKey (KeyCode.R)) {
			Fire ();
		}

		t.position += t.forward * Time.deltaTime * _speed ;
	}
		
	void FixedUpdate () {
		Quaternion AddRot = Quaternion.identity;
		float roll = 0;
		float pitch = 0;
		float yaw = 0;

		roll = Input.GetAxis("Roll") * (Time.deltaTime * rollRate);
		pitch = Input.GetAxis("Pitch") * (Time.deltaTime * pitchRate);
		yaw = Input.GetAxis("Yaw") * (Time.deltaTime * yawRate);

		Quaternion targetRotation = transform.rotation * Quaternion.Euler(new Vector3(-pitch, yaw, -roll));
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 100 * Time.deltaTime);

	}

	void Fire()
	{
		for (int i = 0; i < _guns.Length; i++) {
			_guns [i].Fire (Camera.main.transform.position + Camera.main.transform.forward * 1000);
		}
	}

	public override void OnKilled ()
	{
		print ("KILLED");
	}


}
