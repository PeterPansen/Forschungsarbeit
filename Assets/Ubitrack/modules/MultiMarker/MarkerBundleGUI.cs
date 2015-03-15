using UnityEngine;
using System.Collections;

namespace FAR{

	public class MarkerBundleGUI : MonoBehaviour {
		public ButtonSink resetButton;
		public ButtonSink addImage;
		
		
		void OnGUI() {
		
			if (GUI.Button(new Rect(10, 10, 150, 40), "resetButton"))
			{
	            resetButton.sendButtonEvent(' ');								
			}
	
	        if (GUI.Button(new Rect(160, 10, 150, 40), "addImage"))
	        {
	            addImage.sendButtonEvent(' ');          
	        }
	
	       
		}
	}

}
