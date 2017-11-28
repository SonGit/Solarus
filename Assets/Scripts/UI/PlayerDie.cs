using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDie : MonoBehaviour {

	public GameObject panel;

	public Text _secText;

	// Use this for initialization
	void Start () {
		panel.SetActive (false);
	}

	public void ShowDie()
	{
		StartCoroutine (Retrying_async());
	}
	
	IEnumerator Retrying_async()
	{
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
