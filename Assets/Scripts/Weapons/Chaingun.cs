using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaingun : MonoBehaviour {

	public Transform target;

	public float _bulletScale = 1;

	public float _fireRate;

	private bool _firing;

	public bool _playerOwned;

	//Cache the bullets
	private List<Chaingun_bullet> _cgBullets;

	// Use this for initialization
	void Start () {
		FillCGunBullets ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Fire(Vector3 pos)
	{
		StartCoroutine (Fire_async(pos));
	}

	IEnumerator Fire_async(Vector3 target)
	{
		if (target == null) {
			print ("Null Target!");
		}

		if (!_firing) {
			
			_firing = true;

			Chaingun_bullet bullet = TakeCGunBullet ();

			bullet.transform.position = transform.position;
			bullet.transform.LookAt (target);
			bullet.transform.localScale = new Vector3 (_bulletScale, _bulletScale, _bulletScale);
			bullet._playerOwned = _playerOwned;
			bullet.Live ();

			yield return new WaitForSeconds (_fireRate);

			_firing = false;

		}

	}

	IEnumerator Delay()
	{
		yield return new WaitForSeconds (_fireRate);
	}

	//Initialize bullets list for later use
	void FillCGunBullets()
	{
		_cgBullets = new List<Chaingun_bullet> ();

		for(int i = 0 ; i < 10 ; i++)
		{
			CreateCGBullet ();
		}
	}

	//Take bullet from cache
	public Chaingun_bullet TakeCGunBullet()
	{
		//Check in cache to see if there is any free bullet
		foreach (Chaingun_bullet bullet in _cgBullets) 
		{
			if (!bullet._living)
				return bullet;
		}

		return CreateCGBullet ();
	}

	//Create bullet
	Chaingun_bullet CreateCGBullet()
	{
		GameObject go;
		Chaingun_bullet cg;
		//Create GameObject
		go = ObjectFactory.instance.MakeObject(ObjectFactory.PrefabType.CGBullet);

		if (go == null) {
			print ("NULL OBJECT");
			return null;
		}
			
		cg = go.GetComponent<Chaingun_bullet> ();
		//Default state is destroyed
		cg.Destroy ();
		//Add to list
		_cgBullets.Add (cg);

		return cg;
	}
}
