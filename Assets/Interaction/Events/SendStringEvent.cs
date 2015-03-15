using UnityEngine;
using System.Collections;

namespace FAR{

    public class SendStringEvent : InteractionEvent {
    
    	/*
    		Sends StringInformation from an InteractionMethod to corresponding Actions/InteractionMethods
    		ExampleUse: A sequence of keys is send as a string from KeyboardStorage to all listening
    		actions
    	*/
    
    	private string m_msg;
    	
    	public string message
    	{
    		get { return m_msg; }
    	}
    	
    	public SendStringEvent ( string message, ulong ts, InteractionMethod sender, InteractionEvent base_evt ) : base (ts,sender,base_evt)
    	{
    		this.m_msg = message;
    	}
    	
    }

}
