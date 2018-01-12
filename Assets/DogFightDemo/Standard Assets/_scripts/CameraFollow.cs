using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	private static CameraFollow instance;
	public static CameraFollow Instance
	{
		get
		{
			return instance;
		}
	}
	
	public GameObject target;
	public float sensitivity = .6f;
	private FighterAI currentEnemy;
	
	public void Awake()
	{
		instance = this;
	}
	
	public void SetTarget(FighterAI value)
	{
		if( !currentEnemy || !currentEnemy.isLocked )
		{
			currentEnemy = value;
			target = currentEnemy.gameObject;
		}
	}
	
	public void Update ()
	{
		if( target )
		{
			transform.position = Vector3.Slerp(transform.position, target.transform.position, Time.deltaTime * sensitivity);
			transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, Time.deltaTime * sensitivity);
		}
	}
}

