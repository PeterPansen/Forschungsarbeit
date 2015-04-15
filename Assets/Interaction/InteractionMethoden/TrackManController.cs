using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FAR{

	public class TrackManController : InteractionMethod , Action {
	
		public GameObject SaveLoadMenu;
		s_Settings Einstellungen;
		
	
		// Use this for initialization
		override public void Start () {
			
			base.Start();
			Einstellungen = GameObject.Find ("Settings").GetComponent<s_Settings>();
			SaveLoadMenu.gameObject.SetActive(false);
			
		}
		
		// Update is called once per frame
		void Update () {
		
			CheckForNewListeners();
		
		}
		
		public void doEvent(InteractionEvent evt)
		{		

			
		}
		

		//Called by the GUIButtons of the MainMenu
		public void ToggleSaveLoadMenu()
		{
			if(SaveLoadMenu.gameObject.activeSelf==false)
			{
				SaveLoadMenu.gameObject.SetActive(true);
				Debug.Log ("CALL ME MAYBE");
				SaveLoadMenu.GetComponentInChildren<fileManager>().ResetPaths();
			}
			else
			{
				SaveLoadMenu.gameObject.SetActive(false);
			}
		}
		
		
	}

}