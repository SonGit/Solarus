using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ATTACH TO ObjectFactory gameObject
public class ObjectFactory: MonoBehaviour {

	public static ObjectFactory instance;

	void Awake()
	{
		instance = this;
	}

	public enum PrefabType
	{
		None,
		CGBullet,
		HitSparks,
		MM_Tracker 
	}


	public Dictionary<PrefabType,string> PrefabPaths = new Dictionary<PrefabType, string> {
		
		{ PrefabType.None, "" },
		{ PrefabType.CGBullet, "Prefabs/Chaingun_bullet" },
		{ PrefabType.HitSparks, "Prefabs/VfxHitSparks" },
		{ PrefabType.MM_Tracker, "Prefabs/MM_Tracker" }
	};

	public GameObject MakeObject(PrefabType type)
	{
		string path;
		if (PrefabPaths.TryGetValue (type, out path)) {
			return (Instantiate (Resources.Load (path, typeof(GameObject))) as GameObject);
		}
		return null;
	}

}
