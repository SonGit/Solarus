using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour {

	public static AISpawner instance;

	void Awake()
	{
		instance = this;
	}

	public void Spawn()
	{
		GameObject ai = ObjectFactory.instance.MakeObject (ObjectFactory.PrefabType.Fighter);
		ai.transform.position = transform.position;
	}
}
