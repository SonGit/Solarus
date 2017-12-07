using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDebug : MonoBehaviour {
	public PlayerController _player;
	public Text _debug;

	public Text _allyShipHPText;
	public Text _enemyShipHPText;

	public BigshipHealth _allyShipHP;
	public BigshipHealth _enemyShipHP;

	Vector3 _initPos;
	// Use this for initialization
	void Start () {
		_initPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		_debug.text = "Speed:  "+_player._speed;

		_allyShipHPText.text = _allyShipHP._hitPoints + "";
		_enemyShipHPText.text = _enemyShipHP._hitPoints+ "";

	}

	public void ReturnToInit()
	{
		Application.LoadLevel ("Test2Scene");
	}
}
