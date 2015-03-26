using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FAR{

	public class TrackManController : InteractionMethod , Action {
	
	
		s_Settings Einstellungen;
	
		// Use this for initialization
		override public void Start () {
			
			base.Start();
			Einstellungen = GameObject.Find ("Settings").GetComponent<s_Settings>();
			
		}
		
		// Update is called once per frame
		void Update () {
		
			CheckForNewListeners();
		
		}
		
		public void doEvent(InteractionEvent evt)
		{		
			//Reagiert auf KeyboardEvents
			if(evt.GetType() == typeof(SingleKeyboardEvent))
			{
				SingleKeyboardEvent sevt = (SingleKeyboardEvent) evt;
				
				if(sevt.key.ToString() == Einstellungen.SaveKey)
				{
					SaveDFG(sevt);
				}
				if(sevt.key.ToString() == Einstellungen.LoadKey)
				{
					LoadDFG(sevt);
				}
			}
			
		}
		
		/// <summary>
		/// Called from our SAVE-Button on the MainMenu
		/// </summary>
		public void SaveDFG(InteractionEvent evt)
		{
			Debug.Log ("Saving DFG");
			/*
				TODO:
				Implement an export function able to save the current DFG and all its components
			*/
			
			DFGEvent devt = new DFGEvent(Enums.DFGProcess.Export,"NOT READY YET",CreateTimeStamp(),this.GetComponent<InteractionMethod>(),evt);
			fireEvent(devt);
		}
		//And another for calling by the GUI-Buttons who are not capable of passing InteractionEvents
		public void SaveDFG()
		{
			Debug.Log ("Saving DFG");
			/*
				TODO:
				Implement an export function able to save the current DFG and all its components
			*/
			
			DFGEvent devt = new DFGEvent(Enums.DFGProcess.Export,"NOT READY YET",CreateTimeStamp(),this.GetComponent<InteractionMethod>(),null);
			fireEvent(devt);
		}
		
		
		/// <summary>
		/// Called from our LOAD-Button on the MainMenu
		/// </summary>
		public void LoadDFG(InteractionEvent evt)
		{
			Debug.Log ("Loading DFG");
			/*
				TODO:
				Implement a load-function able to load a presaved DFG and all of its components
			*/
			DFGEvent devt = new DFGEvent(Enums.DFGProcess.Import,"NOT READY YET",CreateTimeStamp(),this.GetComponent<InteractionMethod>(),evt);
			fireEvent(devt);
		}
		//And another for calling by the GUI-Buttons who are not capable of passing InteractionEvents
		public void LoadDFG()
		{
			Debug.Log ("Loading DFG");
			/*
				TODO:
				Implement a load-function able to load a presaved DFG and all of its components
			*/
			DFGEvent devt = new DFGEvent(Enums.DFGProcess.Import,"NOT READY YET",CreateTimeStamp(),this.GetComponent<InteractionMethod>(),null);
			fireEvent(devt);
		}
	}

}