using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour {
	
	[SerializeField]
	private TextMesh hpText;
	[SerializeField]
	private TextMesh speedText;
	[SerializeField]
	private Transform arrow;
	[SerializeField]
	private Transform target;

	PlayerController pc;

	public static PlayerUI instance;

	public Killable _trackedChar;

	void Awake()
	{
		instance = this;
	}
	// Use this for initialization

	void Start () {
		pc = GetComponent<PlayerController> ();
		pc.OnHPChanged += OnHPChanged;
		pc.OnSpeedChanged += OnSpeedChanged;

		OnHPChanged ();
		OnSpeedChanged ();
	}

	void Update()
	{
		if (arrow != null && target != null) {
			arrow.LookAt (target);
		}
	}

	void OnHPChanged()
	{
		hpText.text = (int)(pc._hitPoints/10) + "%";
	}

	void OnSpeedChanged()
	{
		speedText.text = "Thrust: "+(int)pc._speed;
	}
		
}
