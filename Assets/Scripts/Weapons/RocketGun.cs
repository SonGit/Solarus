using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketGun : MonoBehaviour {
	//Specify desired raycast thickness.
	public float _thickness = 15f;
	//Specify time for lockon.
	public float _lockOnTime;
	//Specify max distance for lockon.
	public float _lockDistance = 25f;
	//Specify rocket spawnpoint
	public Transform _spawnPoint;

	private float _timeCount = 0;
	private float _timeOut = 0;

	private bool _allowFire;

	private GameObject[] _enemies;

	public Killable _owner;

	// Use this for initialization
	void Start () {
		_enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		_owner = this.GetComponentInParent<Killable> ();
	}
	// For caching purposes
	private Plane_AI enemyAI;
	private Plane_AI prevenemyAI;
	private bool _isLostTrack;
	// Update is called once per frame
	void FixedUpdate () {

		RaycastHit hit;

		if (Physics.Raycast (Camera.main.transform.position, Camera.main.transform.forward, out hit, 5000)) {

			enemyAI = hit.transform.GetComponent<Plane_AI> ();

			//if enemy is locked-on cone of sight...
			if (enemyAI != null && prevenemyAI == null) {

				// ..count time
				_timeCount += Time.deltaTime;
				//.. check if time is over locked on time
				if (_timeCount >= _lockOnTime) {

					enemyAI.LockedOn ();
					//..Allow fire missile
					_allowFire = true;

					prevenemyAI = enemyAI;
				} 


			} 

		} else {


			if (prevenemyAI != null) {
				_timeOut += Time.deltaTime;

				if (_timeOut > 1) {
					StopLockedOn ();
				
					_timeOut = 0;
				}
			}

		}
		
		//Debug.DrawRay (Camera.main.transform.position, Camera.main.transform.forward * 5000 ,Color.red);

		if (_allowFire && enemyAI != null) {

			if (Input.GetKeyDown (KeyCode.F)) {
				Fire ();
			}

		}



	}
		
	private MoverMissile MakeRocket()
	{
		GameObject go = ObjectFactory.instance.MakeObject(ObjectFactory.PrefabType.Rocket);
		MoverMissile rocket = go.GetComponent<MoverMissile> ();

		if (rocket != null) {
			rocket.transform.position = _spawnPoint.position;
			rocket.transform.LookAt (enemyAI.transform.position);
			rocket.SetTarget (enemyAI.gameObject);
			rocket._Owner = _owner;
			return rocket;
		} else {
			return null;
		}
	}

	void StopLockedOn()
	{
		if(enemyAI != null)
			enemyAI.StopLockedOn ();
		if(prevenemyAI != null)
			prevenemyAI.StopLockedOn ();
		_timeCount = 0;
		_timeOut = 0;
		_allowFire = false;
		prevenemyAI = null;
		enemyAI = null;
	}

	public void Fire()
	{
		if (_allowFire && enemyAI != null) {
			MakeRocket ();
			StopLockedOn ();
		}

	}

}
