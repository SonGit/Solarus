using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLOS : MonoBehaviour {
	
	public string _enemyTag;

	public List<GameObject> _enemyInRadius;

	Transform t;
	// Use this for initialization
	void Start () {
		t = transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	GameObject go;
	private void OnTriggerEnter(Collider collision)
	{
		if (collision.tag == _enemyTag) {
			_enemyInRadius.Add (collision.gameObject);
		}
	}

	private void OnTriggerExit(Collider collision)
	{
		if (collision.tag == _enemyTag) {
			_enemyInRadius.Remove (collision.gameObject);
		}
	}

	List<GameObject> _possibleTargets = new List<GameObject>();
	public GameObject GetTarget()
	{
		_possibleTargets = new List<GameObject> ();

		foreach(GameObject enemyObj in _enemyInRadius)
		{
			RaycastHit hit;

			if (Physics.Raycast (t.position + t.forward * 5, enemyObj.transform.position - t.position, out hit, 10000)) {
				_possibleTargets.Add (enemyObj);
			}
		}

		if (_possibleTargets.Count > 0) {
			return _possibleTargets[Random.Range(0,_possibleTargets.Count)];
		}
//		print ("NULL?");
		return null;
	}
}
