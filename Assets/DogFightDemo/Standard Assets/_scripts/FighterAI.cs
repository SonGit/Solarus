using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FighterAI : Killable 
{
	public float actualSensitivity = .35f;
	public float speed = .5f;
	public float targetPlayerAngle = 30;
	public float firingAngle = 10;
	public float bufferDistance = 10;
	public bool isLocked = false;
	
	public Chaingun leftGun;
	public Chaingun rightGun;
	public Light targetLight;

	public List<GameObject> targetNavWaypoint;

	public GameObject currentWaypoint;
	public float distanceToTarget;
	
	public Killable target;
	public Transform targetTransform;
	public List<Killable> possibleTargets;

	protected float originalSensitivity;

	public BattleCenter battleCenter;

	private Bigship _owner;

	void Start()
	{

	}
	
	public virtual void Initialize (BattleCenter battleCenter,Bigship _owner) 
	{
		targetNavWaypoint = new List<GameObject> ();
		possibleTargets= new List<Killable> ();
		target = null;
		targetTransform = null;
		// for demo only - i've varied the sensitivity and speed so that they react/behave differently
		// in a game situation, where you want to target the other enemies, you'll have to 
		// set the speed to the same as the player to make it work, unless you provide the player
		// with controls to change speed.
		actualSensitivity = Random.Range(actualSensitivity-.2f, actualSensitivity+.2f);
		originalSensitivity = actualSensitivity;
		
		speed = Random.Range(speed-5f, speed+5f);
		
		targetLight.enabled = false;

		this.battleCenter = battleCenter;

		this._owner = _owner;

		CreateField ();
	}

	float timeCount = 0;
	public float timeToResetTarget = 10;
	public void Update () 
	{
		if (target == null) {
			GetNearbyTarget ();
		}
		else {
			timeCount += Time.deltaTime;
			if (timeCount > timeToResetTarget) {
				timeCount = 0;
				target = null;
				GetNearbyTarget ();
				print ("TARGET RESETTED");
			}
		}
		
		//if( currentTarget == null ) return;
		UpdateMovement();	
	}
	
	protected Vector3 relativePos;
	protected Quaternion rotationVector;
	protected void UpdateRotationVector()
	{
	    relativePos =  currentWaypoint.transform.position - transform.position;
	    rotationVector = Quaternion.LookRotation(relativePos);
	}


	protected Quaternion rotQ;
	protected float yRot = 0;
	protected float lastYRot;
	protected void UpdateRotation()
	{
	    rotQ = Quaternion.Lerp(transform.rotation, rotationVector, Time.deltaTime*actualSensitivity);
	    transform.rotation = rotQ;
		
		// this next bit is to get them to bank into their turns
		yRot = lastYRot - transform.localEulerAngles.y;
		yRot = Mathf.Clamp(yRot, -3, 3);
		transform.Rotate(0,0, yRot, Space.Self);
		
		lastYRot = transform.localEulerAngles.y;
	}
	
	private Vector3 targetRotation;
	public float enemyAngle;
	private void UpdateTargeting()
	{
		if( !target ) return;
		
		targetRotation = targetTransform.position - transform.position;
	    enemyAngle = Vector3.Angle (transform.forward, targetRotation);
	
		if (enemyAngle <= targetPlayerAngle) {
			if (currentWaypoint != target.gameObject) {
				currentWaypoint = target.gameObject;
				isLocked = true;				
				actualSensitivity = actualSensitivity + .5f; // increase to stay with target
				
				// for the demo only
//				CameraFollow.Instance.SetTarget (this);
				targetLight.enabled = true;

//				print ("1st");
			}
	
			if (enemyAngle <= firingAngle && !canFireCannons && distanceToTarget < 500) {
				StartCoroutine (FireCannons ());
				
				// for demo only
				targetLight.color = Color.green;

		//		print ("2st");
			} else {
				canFireCannons = false;
				
				// for demo only
				targetLight.color = Color.red;
			//	print ("3st");
			}
		} else if (currentWaypoint == target.gameObject) {
			// if player is NOT in the targeting cone
			actualSensitivity = originalSensitivity; // put back to original
			isLocked = false;
			GetNextTarget ();
			
			// for demo only
			targetLight.enabled = false;

		//	print ("4st");
		} else {
			canFireCannons = false;
		//	print ("5st");
		}

	}
	
	protected bool canFireCannons = false;
	protected IEnumerator FireCannons()
	{
		canFireCannons = true;

		leftGun.Fire (leftGun.transform.position + leftGun.transform.forward);
		rightGun.Fire (rightGun.transform.position + rightGun.transform.forward);
		
		while( canFireCannons )
		{
			yield return new WaitForSeconds(.25f);
		}

	}
	
	protected void UpdateMovement()
	{
		transform.Translate(0,0, (Time.deltaTime * speed));
		
		UpdateRotationVector();
		
		UpdateRotation();
		
		UpdateTargeting();

		distanceToTarget = Vector3.Distance( transform.position, currentWaypoint.transform.position );
		// if player isn't targeted, then check distance
		if( target && currentWaypoint != target.gameObject )
		{
	    	if( distanceToTarget < bufferDistance ) GetNextTarget();
		}
	}

	protected virtual void GetNextTarget()
	{

		// random, unique
		GameObject newTarget = targetNavWaypoint[Random.Range(0, targetNavWaypoint.Count)];
		
		while ( newTarget == currentWaypoint ) newTarget = targetNavWaypoint[Random.Range(0, targetNavWaypoint.Count)];

		// set new target
		currentWaypoint = newTarget;
	}

	public override void OnKilled ()
	{
		ExplosionObject explosion = ObjectPool.instance.GetExplosionObject ();
		explosion.transform.position = transform.position;
		explosion.Live ();

		//Destroy (gameObject);
		transform.position = new Vector3(9999,9999,9999);
		gameObject.SetActive(false);
		if (_owner != null) {
			_owner.Spawn (this);
		}
	}

	private void GetNearbyTarget()
	{
		// init target list
		possibleTargets = new List<Killable>();

		GameObject[] gos = GameObject.FindGameObjectsWithTag ("Fighter");

		foreach(GameObject go in gos)
		{
			Killable killable = go.GetComponent<Killable> ();
			float distance = Vector3.Distance (killable.transform.position,transform.position);
			if (killable != null && distance < 1000) {
				if (FactionRelationshipManager.IsHostile (killable._faction, _faction)) {
					possibleTargets.Add (killable);
				}
			}
		}

		//If no possible targets found, go to battle center
		if (possibleTargets.Count == 0) {

			if (currentWaypoint == null) {
				//targetTransform = battleCenter.transform;
				targetNavWaypoint = battleCenter.navWaypoints;
				GetNextTarget();
			}
			return;
		}
		
		// Randomize the targets
		target = possibleTargets[Random.Range(0,possibleTargets.Count-1)];

		if (target != null) {
			
			targetTransform = target.transform;
			targetNavWaypoint = target.navWaypoints;

			// set first target
			GetNextTarget();

		}
	}


}
