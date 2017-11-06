using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility {

	private static Utility Instance;

	private Utility() {}

	public static Utility instance
	{
		get 
		{
			if (instance == null)
			{
				Instance = new Utility();
			}
			return Instance;
		}
	}

	public const float RUMBLING_NORMAL_AMOUNT = .1f;
	public const float RUMBLING_HIGH_AMOUNT = .25f;
	public const float SHAKE_HIT_AMOUNT = 2f;
	public const float SHAKE_HIT_DURATION = 2f;
}
