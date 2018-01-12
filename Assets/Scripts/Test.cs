using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {
	
	protected List<Plane_AI> _planeCache;

	protected Transform t;

	public BattleCenter _bc;

	// Use this for initialization
	IEnumerator Start () {
		t = transform;

		_planeCache = new List <Plane_AI>();

		for(int i = 0 ; i < 20 ; i++)
		{
			_planeCache.Add( Create() );
			yield return new WaitForSeconds (.51f);
		}
	}
	public Plane_AI Create()
	{
		GameObject go = ObjectFactory.instance.MakeObject (ObjectFactory.PrefabType.Fighter);
		go.transform.position = new Vector3 (transform.position.x + Random.Range(900,1600),transform.position.y + Random.Range(900,1600),transform.position.z + Random.Range(900,1600));
		Plane_AI AI = go.GetComponent<Plane_AI> ();
		AI.Init (_bc);

		return AI;
	}

}
