/// <summary>
/// Player controller.
/// </summary>
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;


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
				if(_introing)
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
	public bool _introing;

	public static PlayerController instance;

	public Text _HPText;
	public Text _hitText;

	private PlayerDie _playerDie;

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
		_playerDie = GetComponentInChildren<PlayerDie> ();

		_isThrusting = false;
	
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

		if (Input.GetKeyDown (KeyCode.R) || Input.GetMouseButtonDown(0)) {
			_isFiring = true;
		}

		if (Input.GetKeyUp (KeyCode.R)|| Input.GetMouseButtonUp(0)) {
			_isFiring = false;
		}

		if (_isFiring)
			Fire ();

		if (_speed < 25)
			_speed = 25;

		if (_speed > 200)
			_speed = 200;

		if(!stop)
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
		_HPText.text = _hitPoints + "";
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
		_playerDie.ShowDie ();
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
		print ("I'M HIT");
		_cameraShake.ShakeHit ();
		StartCoroutine (ShowHit());
	}

	IEnumerator ShowHit()
	{
		_hitText.enabled = true;
		yield return new WaitForSeconds (1);
		_hitText.enabled = false;
	}

	private void OnTriggerEnter(Collider collision)
	{
		BigshipHealth bsh = collision.GetComponent<BigshipHealth> ();

		if (bsh != null)
			_hitPoints = -1;
	}
		
}
