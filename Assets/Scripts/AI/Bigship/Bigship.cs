using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bigship : Killable {

	public Bigship _targetShip;

	public float _minDistanceFromTarget = 900;

	public float _speed;

	public float _currDistanceFromTarget;

	protected List<FighterAI> _planeCache;

	public bool stop;

	public Transform _shipBase;

	protected Transform t;

	protected BattleCenter _bc;

	public PlayerWin _playerWin;

	// Use this for initialization
	IEnumerator Start () {
		t = transform;
		_bc = this.GetComponentInChildren<BattleCenter> ();

		_planeCache = new List <FighterAI>();

		for(int i = 0 ; i < 20 ; i++)
		{
			_planeCache.Add( Create() );

		}

		yield return new WaitForSeconds (.1f);
	}


	// Update is called once per frame
	void Update () {

		Loop ();
	}

	// For caching
	Vector3 targetDir;
	float step;
	Vector3 newDir;

	protected void Loop()
	{
		if (stop || _targetShip == null)
			return;

		_currDistanceFromTarget = Vector3.Distance (t.position, _targetShip.t.position);

		targetDir = _targetShip.t.position - t.position;
		step = .25f * Time.deltaTime;
		newDir = Vector3.RotateTowards(t.forward, targetDir, step, 0.0F);

		t.rotation = Quaternion.LookRotation(newDir);
		if (_currDistanceFromTarget > _minDistanceFromTarget) {


			t.position = Vector3.Lerp (t.position, _targetShip.t.position, Time.deltaTime * _speed);



			if (_currDistanceFromTarget < _minDistanceFromTarget + 1000 ) {
				_shipBase.localRotation = Quaternion.RotateTowards (_shipBase.localRotation, Quaternion.Euler (new Vector3 (0, 90, 0)), 2 * Time.deltaTime);
			} else {
				_shipBase.localRotation = Quaternion.RotateTowards(_shipBase.localRotation, Quaternion.Euler(new Vector3(0,0,0)), 2 * Time.deltaTime);
			}


		} else {

			//t.rotation = Quaternion.RotateTowards(t.rotation, Quaternion.Euler(new Vector3(0,90,0)), 50 * Time.deltaTime);

			_shipBase.localRotation = Quaternion.RotateTowards(_shipBase.localRotation, Quaternion.Euler(new Vector3(0,90,0)), 2 * Time.deltaTime);
		}

		UpdateBattleCenterPos ();
	}

	void UpdateBattleCenterPos()
	{
		float x = t.position.x + (_targetShip.t.position.x - t.position.x) /2;
		float y = t.position.y + (_targetShip.t.position.y - t.position.y) /2 + 300;
		float z = t.position.z + (_targetShip.t.position.z - t.position.z) /2;
		_bc.transform.position = _targetShip.transform.position;
	}


	public FighterAI Create()
	{
		GameObject go;
		if (_faction == Faction.ALLY) {
			go = ObjectFactory.instance.MakeObject (ObjectFactory.PrefabType.AllyFighter);
		} else {
			go = ObjectFactory.instance.MakeObject (ObjectFactory.PrefabType.Fighter);
		}

		go.transform.position = new Vector3 (transform.position.x + Random.Range(-900,900),transform.position.y + Random.Range(-900,900),transform.position.z + Random.Range(-900,900));
		FighterAI AI = go.GetComponent<FighterAI> ();
		AI.Initialize (_bc,this);

		return AI;
	}

	public float _penalty = 0;

	public void Spawn(FighterAI AI)
	{
		StartCoroutine (Spawn_async(AI));
	}

	IEnumerator Spawn_async(FighterAI AI)
	{
		_penalty += 0.2f;
		yield return new WaitForSeconds (_penalty);
		AI.transform.position = new Vector3 (transform.position.x + Random.Range(100,400),transform.position.y + Random.Range(100,400),transform.position.z + Random.Range(100,400));
		AI.gameObject.SetActive (true);
		AI.Initialize (_bc,this);

	//	_hitPoints -= 100;

		OnHit (1000);
	}

	public override void OnKilled ()
	{

		if (_faction == Faction.ENEMY) {
			print ("WIN");
			_playerWin.Play ();
		}

		if (_faction == Faction.ALLY) {
			print ("LOSE");
			_playerWin.Play ();
		}
	}

		
}
