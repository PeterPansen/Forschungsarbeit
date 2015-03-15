using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FAR{

    public class KeyboardStorage : InteractionMethod, Action {
    	//ComplexActionInteractionMethod
    	
    	private string zeichenfolge = "";
        
        
        override public void Start()
        {
            base.Start();
        }
    	
    	public void doEvent(InteractionEvent evt) {
    		if(evt.GetType() == typeof(KeyboardEvent))
    		{
    			KeyboardEvent k_evt = (KeyboardEvent) evt;
    			if(k_evt.key=='#')
    			{
    				Debug.Log ("You entered : "+zeichenfolge+" TS: "+k_evt.timestamp);
    				
    				SendStringEvent ssevt = new SendStringEvent(zeichenfolge,CreateTimeStamp(),this.gameObject.GetComponent<InteractionMethod>(),k_evt);
    				fireEvent(ssevt);
    				
    				zeichenfolge = "";
    			}
    			
    			else
    			{
    				zeichenfolge += ((KeyboardEvent) evt).key;
    			}
    
    
    		}
    			
    	}
    	
    	
    	void Update()
    	{
    		CheckForNewListeners();
    	}
    }

}

