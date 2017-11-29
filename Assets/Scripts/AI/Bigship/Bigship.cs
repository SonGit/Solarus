using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bigship : MonoBehaviour {

	public Faction _faction;

	public Bigship _targetShip;

	public float _minDistanceFromTarget = 900;

	public float _speed;

	public float _currDistanceFromTarget;

	List<Plane_AI> _planeCache;

	public bool stop;

	public Transform _shipBase;

	Transform t;

	BattleCenter _bc;


	// Use this for initialization
	IEnumerator Start () {
		t = transform;
		_bc = this.GetComponentInChildren<BattleCenter> ();

		_planeCache = new List <Plane_AI>();

		for(int i = 0 ; i < 13 ; i++)
		{
			_planeCache.Add( Create() );
			yield return new WaitForSeconds (1);
		}

	}

	// For caching
	Vector3 targetDir;
	float step;
	Vector3 newDir;
	// Update is called once per frame
	void Update () {

		if (stop)
			return;

		_currDistanceFromTarget = Vector3.Distance (t.position, _targetShip.t.position);

		targetDir = _targetShip.t.position - t.position;
		step = 5 * Time.deltaTime;
		newDir = Vector3.RotateTowards(t.forward, targetDir, step, 0.0F);

		t.rotation = Quaternion.LookRotation(newDir);
		if (_currDistanceFromTarget > _minDistanceFromTarget) {
			

			t.position = Vector3.Lerp (t.position, _targetShip.t.position, Time.deltaTime * _speed);

		

			if (_currDistanceFromTarget < _minDistanceFromTarget + 1000 ) {
				_shipBase.localRotation = Quaternion.RotateTowards (_shipBase.localRotation, Quaternion.Euler (new Vector3 (-90, 90, 0)), 2 * Time.deltaTime);
			} else {
				_shipBase.localRotation = Quaternion.RotateTowards(_shipBase.localRotation, Quaternion.Euler(new Vector3(-90,0,0)), 2 * Time.deltaTime);
			}
				

		} else {
			
			//t.rotation = Quaternion.RotateTowards(t.rotation, Quaternion.Euler(new Vector3(0,90,0)), 50 * Time.deltaTime);

			_shipBase.localRotation = Quaternion.RotateTowards(_shipBase.localRotation, Quaternion.Euler(new Vector3(-90,90,0)), 2 * Time.deltaTime);
		}

		UpdateBattleCenterPos ();
	}

	void UpdateBattleCenterPos()
	{
		float x = t.position.x + (_targetShip.t.position.x - t.position.x) /2;
		float y = t.position.y + (_targetShip.t.position.y - t.position.y) /2 + 300;
		float z = t.position.z + (_targetShip.t.position.z - t.position.z) /2;
		_bc.transform.position = new Vector3 (x, y, z);
	}


	public Plane_AI Create()
	{
		GameObject go = ObjectFactory.instance.MakeObject (ObjectFactory.PrefabType.Fighter);
		go.transform.position = new Vector3 (transform.position.x + Random.Range(100,600),transform.position.y + Random.Range(100,400),transform.position.z + Random.Range(100,400));
		Plane_AI AI = go.GetComponent<Plane_AI> ();
		AI.Init (_bc,this);

		return AI;
	}

	public float _penalty = 0;
	public void Spawn(Plane_AI AI)
	{
		StartCoroutine (Spawn_async(AI));
	}

	IEnumerator Spawn_async(Plane_AI AI)
	{
		_penalty += 0.2f;
		yield return new WaitForSeconds (_penalty);
		AI.transform.position = new Vector3 (transform.position.x + Random.Range(100,400),transform.position.y + Random.Range(100,400),transform.position.z + Random.Range(100,400));
		AI.Init (_bc,this);
		AI.gameObject.SetActive (true);
	}

		
}
