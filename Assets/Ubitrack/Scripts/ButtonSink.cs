using UnityEngine;
using System.Collections;
using Ubitrack;
using System;

namespace FAR{
	
	//Vorher ButtonSource jetzt auf UnityPerspektive geändert
	public class ButtonSink : UbiTrackComponent
	{    
	    public string ButtonKey = "";
	    public int EventID = 0;
	
	    private SimpleButtonReceiver m_button;
	    public override void utInit(Ubitrack.SimpleFacade simpleFacade)
	    {
	        base.utInit(simpleFacade);
	        m_button = simpleFacade.getPushSourceButton(patternID);
	        if (m_button == null)
	        {
	            throw new Exception("SimpleApplicationPushSourceButton not found for ID:" + patternID);
	        }
	    }
	
	    public void sendButtonEvent(int eventID)
	    {
	        SimpleButton b = new SimpleButton();
	        b.timestamp = UbiMeasurementUtils.getUbitrackTimeStamp();
	        b._event = eventID;
	        m_button.receiveButton(b);
	    }
		
		// Update is called once per frame
		void Update () {
	        if (ButtonKey.Length == 1 && Input.GetKeyDown(ButtonKey))
	        {
	            sendButtonEvent(EventID);
	        }
		}
	}

}
