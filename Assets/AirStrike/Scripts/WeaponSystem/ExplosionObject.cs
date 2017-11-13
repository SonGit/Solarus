using UnityEngine;
using System.Collections;

public class ExplosionObject : Cacheable
{
	ParticleSystem[] particles;

    void Start()
    {
		particles = this.GetComponentsInChildren<ParticleSystem> ();
		Destroy ();
    }

	IEnumerator Play()
	{
		foreach (ParticleSystem particle in particles) {
			particle.Play ();
		}
		yield return new WaitForSeconds (3);
		Destroy ();
	}

	public override void OnDestroy ()
	{
		gameObject.SetActive (false);
	}

	public override void OnLive ()
	{
		particles = this.GetComponentsInChildren<ParticleSystem> ();
		gameObject.SetActive (true);
		StartCoroutine (Play());
	}

}
