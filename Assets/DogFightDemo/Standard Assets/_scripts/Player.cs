using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : FighterAI
{
	private static Player instance;
	public static Player Instance
	{
		get
		{
			return instance;
		}
	}
	
	private GameObject dummyTarget;

	public void Awake ()
	{
		instance = this;
	}
	
	public void Start()
	{
		dummyTarget = new GameObject("DummyTarget");
//		EnemyManager.Instance.EnemiesCreatedEvent += HandleEnemyManagerInstanceEnemiesCreatedEvent;
	}

	void HandleEnemyManagerInstanceEnemiesCreatedEvent (List<GameObject> enemies)
	{

	}
	

	
	protected override void GetNextTarget ()
	{
		dummyTarget.transform.position += Random.insideUnitSphere * 200;
		// set new target
	//	currentWaypoint = dummyTarget;
	}
	
	public IEnumerator MonitorFlightControl()
	{
		bool canFly = true;
		while (canFly)
		{
			yield return new WaitForSeconds(Random.Range(.55f, 5f));
			GetNextTarget();
		}
	}
}

