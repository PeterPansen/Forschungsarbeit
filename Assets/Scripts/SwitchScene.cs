using UnityEngine;
using System.Collections;

public class SwitchScene : MonoBehaviour {
	public int level;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("l")){
			Application.LoadLevel(level);			
		}
	}
	
	void OnGUI(){
		if(GUI.Button(new Rect(0,0,50,50), "..")){
			Application.LoadLevel(level);		
		}
		if(GUI.Button(new Rect(150,0,50,50), "q")){
			Application.Quit();
		}
	}
}
