using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipAI : MonoBehaviour {

	public enum AIState
	{
		Patrol,
		Pursue,
		Pursued
	}

	public AIState currentState;
	public float actualSensitivity = .35f;
	public float speed = .5f;
	public float targetPlayerAngle = 30;
	public float firingAngle = 10;
	public float bufferDistance = 10;
	public bool isLocked = false;

	public SpaceshipAI currentTarget;

	public virtual void Initialize () 
	{
		// for demo only - i've varied the sensitivity and speed so that they react/behave differently
		// in a game situation, where you want to target the other enemies, you'll have to 
		// set the speed to the same as the player to make it work, unless you provide the player
		// with controls to change speed.
		actualSensitivity = Random.Range(actualSensitivity-.2f, actualSensitivity+.2f);


		speed = Random.Range(speed-5f, speed+5f);
	}

	public void Update () 
	{
		if(currentTarget == null ) return;
		UpdateMovement();	
	}

	void Engage(SpaceshipAI evoker)
	{
		if (currentState == AIState.Pursue || currentState == AIState.Pursued) {

		}
	}

	protected Vector3 relativePos;
	protected Quaternion rotationVector;
	protected void UpdateRotationVector()
	{
		relativePos =  currentTarget.transform.position - transform.position;
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

	protected void UpdateMovement()
	{
		transform.Translate(0,0, (Time.deltaTime * speed));

		UpdateRotationVector();

		UpdateRotation();

	}

}
