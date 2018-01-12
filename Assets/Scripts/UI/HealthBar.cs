using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

	[SerializeField]
	private Transform hpBar;

	public Killable tracked;

	private float _maxScale;

	private float _maxHealth;

	// Use this for initialization
	void Start () {
		_maxScale = hpBar.localScale.x;
		_maxHealth = tracked._hitPoints;
	}

	float remainHPPercent;
	float hpScale;
	// Update is called once per frame
	void FixedUpdate () {

		remainHPPercent = (tracked._hitPoints * 100) / _maxHealth;
		hpScale = (remainHPPercent * _maxScale) / 100;

		hpBar.localScale = new Vector3 (hpScale,hpBar.localScale.y,hpBar.localScale.z);
	}
}
