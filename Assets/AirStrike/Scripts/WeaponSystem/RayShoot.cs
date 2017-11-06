using UnityEngine;
using System.Collections;

public class RayShoot : DamageBase
{

	public int Range = 10000;
	public Vector3 AimPoint;
	public GameObject Explosion;
	public float LifeTime = 1;
	public LineRenderer Trail;
	private Vector3 startPosition;
	
	void Start ()
	{
		if (GetComponent<Collider>()) {
			Physics.IgnoreCollision (GetComponent<Collider>(), Owner.GetComponent<Collider>());
			if (Owner.transform.root) {
				foreach (Collider col in Owner.transform.root.GetComponentsInChildren<Collider>()) {
					Physics.IgnoreCollision (GetComponent<Collider>(), col);
				}
			}
		}
		DamagePackage dm = new DamagePackage();
		dm.Damage = Damage;
		dm.Owner = Owner;
		RaycastHit hit;
		GameObject explosion = null;
		startPosition = this.transform.position;
		
		if (Physics.Raycast (this.transform.position, this.transform.forward, out hit, Range)) {
			AimPoint = hit.point;
			if (Explosion != null) {
				explosion = (GameObject)GameObject.Instantiate (Explosion, AimPoint, Explosion.transform.rotation);
			}
			hit.collider.gameObject.SendMessage ("ApplyDamage", dm, SendMessageOptions.DontRequireReceiver);
		} else {
			AimPoint = this.transform.forward * Range;
			if (Explosion != null) 
				explosion = (GameObject)GameObject.Instantiate (Explosion, AimPoint, Explosion.transform.rotation);
			
		}
		if (explosion) {
			DamageBase dmg = explosion.GetComponent<DamageBase> ();
			if (dmg) {
				dmg.TargetTag = TargetTag;	
			}
		}
		
		
		if (Trail) {
			Trail.SetPosition (0, startPosition);
			Trail.SetPosition (1, this.transform.position + this.transform.forward * 10000);
		}
		Destroy (this.gameObject, LifeTime);
	}
	
	void Update ()
	{
		if(Trail){
			startPosition = Vector3.Lerp(startPosition,this.transform.position + this.transform.forward * 10000,0.5f);
			Trail.SetPosition(0,startPosition);	
		}
	}
}
