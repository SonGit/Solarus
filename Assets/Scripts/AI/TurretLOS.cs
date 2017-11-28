using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLOS : MonoBehaviour {
	
	public List<Killable> _enemyInRadius;

	public Collider _ownCollider;

	Turret turret;

	Transform t;
	// Use this for initialization
	void Start () {
		t = transform;
		turret = this.GetComponentInParent<Turret> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	GameObject go;
	private void OnTriggerEnter(Collider collision)
	{
			Killable obj = collision.GetComponent<Killable> ();
			if (obj != null) {
			if(_enemyInRadius.IndexOf (obj) == -1)
				_enemyInRadius.Add (obj);
			}

	}

	private void OnTriggerExit(Collider collision)
	{
			Killable obj = collision.GetComponent<Killable> ();
		if (obj != null) {

			_enemyInRadius.Remove (obj);
		}
	
	}

	public List<Killable> _possibleTargets;
	Killable k;
	public GameObject GetTarget()
	{
		_possibleTargets = new List<Killable> ();

		foreach(Killable enemyObj in _enemyInRadius)
		{
			RaycastHit hit;

			if (Physics.Raycast (t.position , enemyObj.transform.position - t.position, out hit, 10000)) {

				k = hit.transform.GetComponent<Killable> ();

				if (k != null && k == enemyObj) {
					if(FactionRelationshipManager.IsHostile (turret._faction,enemyObj._faction))
						_possibleTargets.Add (enemyObj);
				}
		//		print (hit.transform.name);

			}

		//	Debug.DrawRay (t.position , enemyObj.transform.position - t.position,Color.yellow);
		}

		if (_possibleTargets.Count > 0) {
			return _possibleTargets[Random.Range(0,_possibleTargets.Count)].gameObject;
		}
		//_enemyInRadius = new List<Killable> ();
		return null;
	}
}
