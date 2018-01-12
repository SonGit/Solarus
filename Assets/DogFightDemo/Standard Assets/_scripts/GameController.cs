using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{	
	private static GameController instance;
	public static GameController Instance
	{
		get	
		{
			return instance;
		}
	}
	
	public void Awake()
	{
		instance = this;
	}
	
	public bool showFullScreenButton = false;
	public GUIStyle fullScreenStyle;
	
	public Resolution[] resolutions;
	
	public void Start () 
	{		
		resolutions = Screen.resolutions;
		
		// create way points
//		EnemyManager.Instance.InitializeAndCreateFirstWave();
	}
	
	public void OnGUI()
	{
		if( !showFullScreenButton ) return;
		if( GUI.Button(new Rect(Screen.width-128, Screen.height-128, 128, 128), "", fullScreenStyle) )
		{
			if( !Screen.fullScreen ) 
				Screen.SetResolution(resolutions[resolutions.Length-1].width, resolutions[resolutions.Length-1].height, true);
			else
				Screen.fullScreen = false;
		}
	}
	
	public void Update()
	{
		transform.position = Player.Instance.transform.position;
	}
}
