using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDie : MonoBehaviour {

	public GameObject panel;

	public Text _secText;

	public GameObject _camera;

	public MouseLook _mouseLook;

	// Use this for initialization
	void Start () {
		panel.SetActive (false);
	}



	public void ShowDie()
	{
		_mouseLook.enabled = false;
		StartCoroutine (Retrying_async());
	}
	
	IEnumerator Retrying_async()
	{
		//Camera works
		_camera.transform.localPosition = new Vector3(0,60,0);
		_camera.transform.localEulerAngles = new Vector3 (90,0,0);

		yield return new WaitForSeconds(3);

		panel.SetActive (true);

		int i = 5;

		while (i > -1) {
			_secText.text = i + "";
			yield return new WaitForSeconds (1);
			i--;
		}

		panel.SetActive (true);
	}
}
