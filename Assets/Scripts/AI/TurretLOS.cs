using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLOS : MonoBehaviour {
	
	public string _enemyTag;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	GameObject go;
	private void OnTriggerStay(Collider collision)
	{
		if (collision.tag == _enemyTag) {
			go = collision.gameObject;
		}
	}

	public GameObject GetTarget()
	{
		return go;
	}
}
