using UnityEngine;
using System.Collections;

public class s_Camera : MonoBehaviour {


    s_Settings Einstellungen;

	// Use this for initialization
	void Start () {

        Einstellungen = GameObject.Find("Settings").GetComponent<s_Settings>();
	
	}
	
	// Update is called once per frame
	void Update () {

    
	
	}


}
