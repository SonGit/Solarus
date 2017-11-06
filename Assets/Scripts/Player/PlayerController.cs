/// <summary>
/// Player controller.
/// </summary>
using UnityEngine;
using System.Collections;


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
	// Specify the crosshair 
	public Transform _crosshair;
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

		if (Input.GetKeyDown (KeyCode.Space)) {
			_isThrusting = true;
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			_isThrusting = false;
		}

		if (Input.GetKey (KeyCode.Space)) {
			t.position += t.forward * Time.deltaTime * _speed * 2.5f;
		}
		else
			t.position += t.forward * Time.deltaTime * _speed ;


		if (Input.GetKey (KeyCode.R)) {
			Fire ();
		}
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
			_guns [i].Fire (_guns [i].transform.position + t.forward);
		}
	}

	public override void OnKilled ()
	{
		print ("KILLED");
	}
}
