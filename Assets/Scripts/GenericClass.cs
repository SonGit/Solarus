﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericClass <T> where T : Cacheable
{
	private List<T> _objs;

	private ObjectFactory.PrefabType _type;

	public GenericClass(ObjectFactory.PrefabType type)
	{
		_objs = new List<T>();
		_type = type;

		for (int i = 0; i < 15; i++) {
			MakeObj ();
		}
	}

	private T MakeObj()
	{
		GameObject go = ObjectFactory.instance.MakeObject(_type);
		T hs = go.GetComponent<T> ();

		if (hs != null) {
			_objs.Add (hs);
			return hs;
		}
		return default(T);
	}

	public T GetObj()
	{
		//Check in cache to see if there is any free spark
		foreach (T t in _objs) 
		{
			if (!t._living)
				return t;
		}

		return MakeObj ();
	}
		
}