using UnityEngine;
using System.Collections;

public class MoverMissile : Cacheable
{
	public float Damping = 3;
	public float Speed = 80;
	public float SpeedMax = 80;
	public float SpeedMult = 1;
	public Vector3 Noise = new Vector3 (20, 20, 20);
	public float TargetLockDirection = 0.5f;
	public int DistanceLock = 70;
	public int DurationLock = 40;
	public bool Seeker;
	public float LifeTime = 5.0f;
	private bool locked;
	private int timetorock;
	private float timeCount = 0;
	GameObject Target;

	public Killable _Owner;

	void Start ()
	{
		timeCount = 0;
		Destroy (gameObject, LifeTime);
	}

	public void SetTarget(GameObject Target)
	{
		this.Target = Target;
	}
	
	private void FixedUpdate ()
	{
		GetComponent<Rigidbody>().velocity = new Vector3 (transform.forward.x * Speed * Time.fixedDeltaTime, transform.forward.y * Speed * Time.fixedDeltaTime, transform.forward.z * Speed * Time.fixedDeltaTime);
		GetComponent<Rigidbody>().velocity += new Vector3 (Random.Range (-Noise.x, Noise.x), Random.Range (-Noise.y, Noise.y), Random.Range (-Noise.z, Noise.z));
		
		if (Speed < SpeedMax) {
			Speed += SpeedMult * Time.fixedDeltaTime;
		}
	}

	private void Update ()
	{
		timeCount += Time.deltaTime;
		
		if (Target) {
			Quaternion rotation = Quaternion.LookRotation (Target.transform.position - transform.transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * Damping);
			Vector3 dir = (Target.transform.position - transform.position).normalized;
			float direction = Vector3.Dot (dir, transform.forward);
			//if (direction > TargetLockDirection) {
				//Target = null;
			//}
		}

	}

	private void OnTriggerEnter(Collider collision)
	{
		if (timeCount < 1f)
			return;
		
		ExplosionObject explosion = ObjectPool.instance.GetExplosionObject ();

		explosion.transform.position = transform.position;
		explosion.Live ();

		Killable killable = Target.GetComponent<Killable> ();

		if (killable != null) {
			//Cause damage
			killable.OnHit (500,_Owner);
		}

		Destroy (gameObject);
	}

	public override void OnDestroy ()
	{
		gameObject.SetActive (false);
	}

	public override void OnLive ()
	{
		gameObject.SetActive (true);
	}

}
