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
		MM_Tracker,
		Rocket,
		Explosion,
		Fighter,
		ScoreText,
		BattleCenter,
		OffScreenIndicator,
		AllyFighter
	}


	public Dictionary<PrefabType,string> PrefabPaths = new Dictionary<PrefabType, string> {
		
		{ PrefabType.None, "" },
		{ PrefabType.CGBullet, "Prefabs/Chaingun_bullet" },
		{ PrefabType.HitSparks, "Prefabs/VfxHitSparks" },
		{ PrefabType.MM_Tracker, "Prefabs/MM_Tracker" },
		{ PrefabType.Rocket, "Prefabs/bullet_rocket" },
		{ PrefabType.Explosion, "Prefabs/Explosion" },
		{ PrefabType.Fighter, "Prefabs/Enemy Fighter 1" },
		{ PrefabType.ScoreText, "Prefabs/ScoreText" },
		{ PrefabType.BattleCenter, "Prefabs/BattleCenter" },
		{ PrefabType.OffScreenIndicator, "Prefabs/OffScreenIndicator" },
		{ PrefabType.AllyFighter, "Prefabs/Ally Fighter" },
	};

	public GameObject MakeObject(PrefabType type)
	{
		string path;
		if (PrefabPaths.TryGetValue (type, out path)) {
			return (Instantiate (Resources.Load (path, typeof(GameObject))) as GameObject);
		}
		print ("NULL");
		return null;
	}

}
