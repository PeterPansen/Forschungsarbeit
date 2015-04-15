using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;

namespace FAR{

	public class DFGParserAction : MonoBehaviour, Action {
	
		/*
			TODO:
			Diese Aktion soll SÄMTLICHE DFG Exports und Imports übernehmen.
			Ausgelöst wird sie durch den TrackManController der direkt DFGEvents mit Anweisungen schickt.
		*/
	

		
		public void doEvent(InteractionEvent evt)
		{
			//Checks what kind of DFGEvent we received
			if(evt.GetType() == typeof(DFGEvent))
			{
				DFGEvent devt = (DFGEvent) evt;
				if(devt.DFGProc == Enums.DFGProcess.Export)
				{
					Debug.Log ("EXPORT COMMAND RECEIVED");
					ExportDFG(devt.Path);
				}
				if(devt.DFGProc == Enums.DFGProcess.Import)
				{
					Debug.Log ("IMPORT COMMAND RECEIVED");
					ImportDFG(devt.Path);
				}
			}
		}
		
		/// <summary>
		/// Imports the DFG specified in an event. Gets called by the TrackManController
		/// </summary>
		/// <param name="path">Path.</param>
		public void ImportDFG(string path){}
		
		
		
		/// <summary>
		/// Exports the DFG specified in an DFGEvent. Gets called by the TrackManController
		/// </summary>
		/// <param name="path">Path.</param>
		public void ExportDFG(string path){}
		
		
	}
}
