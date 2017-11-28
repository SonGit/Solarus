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
	void Awake () {
		_guns = this.GetComponentsInChildren<Chaingun> ();
		_turretLos = this.GetComponentInChildren<TurretLOS> ();
	}

	// For caching
	Vector3 targetDir;
	float step;
	Vector3 newDir;
	// Update is called once per frame
	void Update () {

		_timeCount += Time.deltaTime;

		if (_timeCount > 1) {
			_target = null;
			_timeCount = 0;
		}


		if (_target == null) {
			//print ("_targetnull");
			_target = _turretLos.GetTarget ();
			return;
		}

		
		targetDir = _target.transform.position - _turretBase.position;
		step = _turretRotSpeed * Time.deltaTime;
		newDir = Vector3.RotateTowards(_turretBase.forward, targetDir, step, 0.0F);

		_turretBase.rotation = Quaternion.LookRotation(newDir);

		if(_target!= null)
		foreach (Chaingun gun in _guns) {
			gun.Fire (gun.transform.position + gun.transform.forward);
		}


	}

	public override void OnKilled ()
	{
		ExplosionObject explosion = ObjectPool.instance.GetExplosionObject ();
		explosion.transform.position = _turretBase.position;
		explosion.Live ();

		gameObject.SetActive (false);
	}

}
