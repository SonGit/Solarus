using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AIController))]

public class Plane_AI : Killable {

	public RectTransform _targeting;

	//Caching 
	private Camera _camera;

	private bool _beingLocked;

	void Start () {
		StopLockedOn ();

		//onHitEvent += PlayerController.instance.OnHitEnemy ;
		//onKilledEvent += PlayerController.instance.OnKilledEnemy ;
	}

	public override void OnKilled ()
	{
		ExplosionObject explosion = ObjectPool.instance.GetExplosionObject ();
		explosion.transform.position = transform.position;
		explosion.Live ();

		AISpawner.instance.Spawn ();
		Destroy (gameObject);
	}

	void Update()
	{
		
	}

	//UI Stuffs

	public void LockedOn()
	{
		SetTargetingColor (Color.red);
		_beingLocked = true;
	}

	public bool IsLockedOn()
	{
		return _beingLocked;
	}

	public void StopLockedOn()
	{
		SetTargetingColor (Color.green);
		_beingLocked = false;
	}

	void SetTargetingColor(Color color)
	{
		if(!_targeting.gameObject.activeInHierarchy)
			_targeting.gameObject.SetActive (true);
		
		Image[] imgs = _targeting.GetComponentsInChildren<Image>();

		foreach (Image img in imgs) {
			img.color = color;
		}
	}


}
