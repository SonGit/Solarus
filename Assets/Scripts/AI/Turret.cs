using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Killable {

	public GameObject _target;

	public Transform _turretBase;

	public float _turretRotSpeed;

	private Chaingun[] _guns;

	public bool _seeTarget;

	private TurretLOS _turretLos;

	private float _timeOut = .5f;

	private float _timeCount = 0;

	// Use this for initialization
	void Start () {
		_guns = this.GetComponentsInChildren<Chaingun> ();
		_turretLos = this.GetComponentInChildren<TurretLOS> ();
	}

	// For caching
	Vector3 targetDir;
	float step;
	Vector3 newDir;
	// Update is called once per frame
	void Update () {



		if (_target == null) {
			_target = _turretLos.GetTarget ();
			return;
		}
			
		
		targetDir = _target.transform.position - _turretBase.position;
		step = _turretRotSpeed * Time.deltaTime;
		newDir = Vector3.RotateTowards(_turretBase.forward, targetDir, step, 0.0F);
		Debug.DrawRay(_turretBase.position, newDir, Color.red);
		_turretBase.rotation = Quaternion.LookRotation(newDir);

		RaycastHit hit;
	
		if (Physics.Raycast (_turretBase.position +_turretBase.forward * 5, _target.transform.position - _turretBase.position, out hit, 10000)) {

			Debug.DrawRay (_turretBase.position +_turretBase.forward * 5 , _target.transform.position - _turretBase.position, Color.yellow);
//			print (hit.transform.gameObject.name);
			if (hit.transform.gameObject == _target) {
				_seeTarget = true;
			} else {
				_seeTarget = false;
			}

		}

		if (!_seeTarget) {
			_timeCount += Time.deltaTime;

			if (_timeCount >= _timeOut) {
				_timeCount = 0;
				_target = _turretLos.GetTarget ();
			}
		}

		if(_seeTarget)
		foreach (Chaingun gun in _guns) {
			gun.Fire (gun.transform.position + gun.transform.forward);
		}
	}

	public override void OnKilled ()
	{
		ExplosionObject explosion = ObjectPool.instance.GetExplosionObject ();
		explosion.transform.position = _turretBase.position;
		explosion.Live ();

		Destroy (gameObject);
	}

}
