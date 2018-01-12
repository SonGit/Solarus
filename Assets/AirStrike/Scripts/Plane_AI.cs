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

	private Bigship _owner;

	public bool _living;

	Transform _playerTransform;

	void Awake () {
		StopLockedOn ();
		_aiController = this.GetComponent<AIController> ();
	}

	void Start()
	{
		_playerTransform = PlayerController.instance.transform;
	}

	void Update()
	{
		//If enemy, show on/off screen indicators based on distance
		if (_faction == Faction.ENEMY) {
			float distanceToPlayer = Vector3.Distance (transform.position,_playerTransform.position);

			if (distanceToPlayer < 1000) {
				
			} else {

			}
		}
	}


	public override void OnKilled ()
	{
		ExplosionObject explosion = ObjectPool.instance.GetExplosionObject ();
		explosion.transform.position = transform.position;
		explosion.Live ();

		//Destroy (gameObject);
		transform.position = new Vector3(9999,9999,9999);
		_living = false;
		gameObject.SetActive(false);

		if (_owner != null) {
		//	_owner.Spawn (this);
		}
	}

	public void Init(BattleCenter bc,Bigship owner = null)
	{
		_aiController.CenterOfBattle = bc;
	
		_living = true;

		if (owner != null) {
			_owner = owner;
			_faction = owner._faction;
		} else {
			_faction = Faction.ALLY;
		}
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
