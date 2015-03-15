using UnityEngine;
using System.Collections;

namespace FAR{

    public class InteractionEvent {
    
    	/*
    		The basic InteractionEvent-Class. Contains a TIMESTAMP in Unix-ULong-Format (since 1.1.1970) 
    		and a reference to the SENDING InteractionMethod
    	*/
    
    	private ulong m_timestamp;
    	
    	private InteractionMethod m_sender;
    	
        /*
            BaseEvent is a newly added Property.
            It shows which InteractionEvent lead to this one
            Normally null,Only contains something if triggered by
            a compley InteractionMethod.
        */
        private InteractionEvent m_BaseEvent;
    	
    	public ulong timestamp
    	{
    		get { return m_timestamp; }
    	}
    	
    	public InteractionMethod sender
    	{
    		get { return m_sender; }
    	}
        
        public InteractionEvent BaseEvent
        {
            get { return m_BaseEvent; }
        }
        
    	public InteractionEvent(ulong ts,InteractionMethod sender, InteractionEvent base_event)
    	{
    		m_timestamp = ts;
    		m_sender = sender;
            m_BaseEvent = base_event;
    	}

        

    	
    	
    
    }


}