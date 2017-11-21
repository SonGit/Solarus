using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ATTACH TO ObjectPool gameObject

public class ObjectPool : MonoBehaviour {

	public static ObjectPool instance;

	GenericClass<HitSparks> _hitSparks;

	GenericClass<MMTracker> _mmTrackers;

	GenericClass<ExplosionObject> _explosionObjs;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		_hitSparks = new GenericClass<HitSparks>(ObjectFactory.PrefabType.HitSparks,10);
		_mmTrackers = new GenericClass<MMTracker>(ObjectFactory.PrefabType.MM_Tracker,10);
		_explosionObjs = new GenericClass<ExplosionObject>(ObjectFactory.PrefabType.Explosion,10);
	}
		

	public HitSparks GetHitSpark()
	{
		return _hitSparks.GetObj ();
	}

	public MMTracker GetMMTracker()
	{
		return _mmTrackers.GetObj ();
	}

	public ExplosionObject GetExplosionObject()
	{
		return _explosionObjs.GetObj ();
	}
		
}
