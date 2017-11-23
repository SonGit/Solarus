using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AIController))]

public class Plane_AI : Killable {

	public RectTransform _targeting;

	//Caching 
	private Camera _camera;

	private bool _beingLocked;

	private AIController _aiController;

	private bool isAlly;

	public bool _isAlly
	{
		get {
			return isAlly;
		}

		set {
			isAlly = value;

			if (isAlly) {
				transform.tag = "Ally";
				_aiController.TargetTag = new string[]{ "Enemy" };
				_radarVisibility.SetVisible (false);
			
			} else {
				transform.tag = "Enemy";	
				_aiController.TargetTag = new string[]{"Player","Ally"};
				_radarVisibility.SetVisible (true);
			}

		}
	}

	private Bigship _owner;

	private VisibleOnRadar _radarVisibility;

	void Awake () {
		StopLockedOn ();
		_aiController = this.GetComponent<AIController> ();
		_radarVisibility = this.GetComponent<VisibleOnRadar> ();
		//onHitEvent += PlayerController.instance.OnHitEnemy ;
		//onKilledEvent += PlayerController.instance.OnKilledEnemy ;
	}

	public override void OnKilled ()
	{
		ExplosionObject explosion = ObjectPool.instance.GetExplosionObject ();
		explosion.transform.position = transform.position;
		explosion.Live ();

		if (_owner != null) {
			_owner.Spawn ();
		}

		Destroy (gameObject);
	}

	public void Init(BattleCenter bc,bool isAlly,Bigship owner)
	{
		_aiController.CenterOfBattle = bc;
		_isAlly = isAlly;
		_owner = owner;
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
