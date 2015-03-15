using UnityEngine;
using System.Collections;
using System;
using Ubitrack;
using FAR;

public class PTAMGUI : MonoBehaviour {

	public ButtonSink PTAMControlButton;
	
	public Vector2 StartPos = new Vector2(0, 0);
	public Vector2 EndPos = new Vector2(640, 480);

	protected SimpleButtonReceiver m_TriggerStateReceiver;
	protected SimpleButtonReceiver m_TriggerFeaturePointsReceiver;
	
	private bool showPTAMGui = false;
	
	void Start () {
        if (PTAMControlButton == null)
            throw new ArgumentException("PTAMControlButton is null");							
	}
	
	void Update(){
		if(Input.GetKeyDown("m"))
			showPTAMGui = !showPTAMGui;
	}
	

	void OnGUI() {
		if (GUI.Button(new Rect(300, 10, 150, 40), "ptam menu"))
			{
	            showPTAMGui = !showPTAMGui;
			}
	
		if(showPTAMGui){
			if (GUI.Button(new Rect(10, 10, 150, 40), "Toggle State"))
			{
	            PTAMControlButton.sendButtonEvent('t');								
			}
	
	        if (GUI.Button(new Rect(160, 10, 150, 40), "Toogle Feature Points"))
	        {
	            PTAMControlButton.sendButtonEvent('f');          
	        }
	
	        if (GUI.Button(new Rect(10, 50, 100, 40), "Save Maps"))
	        {
	            PTAMControlButton.sendButtonEvent('s');
	        }
	
	        if (GUI.Button(new Rect(110, 50, 100, 40), "Load Maps"))
	        {
	            PTAMControlButton.sendButtonEvent('l');
	        }
	
	        if (GUI.Button(new Rect(210, 50, 100, 40), "Toogle Maping"))
	        {
	            PTAMControlButton.sendButtonEvent('m');
	        }
		}
		
		
	}
	
}
