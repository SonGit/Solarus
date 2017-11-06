using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ATTACH TO ObjectPool gameObject

public class ObjectPool : MonoBehaviour {

	public static ObjectPool instance;

	private List<HitSparks> _hitSparkParticles;

	private List<MMTracker> _mmTrackers;

	void Awake()
	{
		instance = this;
		_hitSparkParticles = new List<HitSparks> ();
		_mmTrackers = new List<MMTracker> ();
	}

	// Use this for initialization
	void Start () {
		FillSparksParticles ();
		FillMMTrackers ();
	}

	private void FillSparksParticles()
	{
		for (int i = 0; i < 25; i++) {
			MakeHitSpark ();
		}
	}

	private HitSparks MakeHitSpark()
	{
		GameObject go = ObjectFactory.instance.MakeObject(ObjectFactory.PrefabType.HitSparks);
		HitSparks hs = go.GetComponent<HitSparks> ();

		if (hs != null) {
			_hitSparkParticles.Add (hs);
			hs.Destroy ();// default state is destroyed
			return hs;
		}
		return null;
	}

	public HitSparks GetHitSpark()
	{
		//Check in cache to see if there is any free spark
		foreach (HitSparks spark in _hitSparkParticles) 
		{
			if (!spark._living)
				return spark;
		}

		return MakeHitSpark ();
	}

	private void FillMMTrackers()
	{
		for (int i = 0; i < 5; i++) {
			MakeMMTrackers ();
		}
	}

	private MMTracker MakeMMTrackers()
	{
		GameObject go = ObjectFactory.instance.MakeObject(ObjectFactory.PrefabType.MM_Tracker);
		MMTracker hs = go.GetComponent<MMTracker> ();

		if (hs != null) {
			_mmTrackers.Add (hs);
			hs.Destroy ();// default state is destroyed
			return hs;
		}
		return null;
	}

	public MMTracker GetTracker()
	{
		//Check in cache to see if there is any free spark
		foreach (MMTracker tracker in _mmTrackers) 
		{
			if (!tracker._living)
				return tracker;
		}

		return MakeMMTrackers ();
	}

}
