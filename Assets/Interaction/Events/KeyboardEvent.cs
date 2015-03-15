using UnityEngine;
using System.Collections;

namespace FAR{

    public class KeyboardEvent : InteractionEvent {
    
    	/*
    		Passes information about a KEY-BUTTON that was pressed on the keyboard
    	*/
    
    	private char m_key;
    	
    	public char key
    	{
    		get { return m_key; }
    	}
    	
    	public KeyboardEvent ( char key, ulong ts, InteractionMethod sender, InteractionEvent base_event ) : base(ts,sender,base_event)
    	{
    		this.m_key = key;
    	}
    
    
    }

}
