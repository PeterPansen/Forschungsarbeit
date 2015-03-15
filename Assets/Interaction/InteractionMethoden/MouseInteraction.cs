using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

namespace FAR{

    public class MouseInteraction : InteractionMethod {
    
    
    	// Use this for initialization
    	override public void Start () {
    		
            base.Start();
    	}
    	
    	// Update is called once per frame
    	void Update () {
    
    		CheckForNewListeners();

    		if(Input.anyKey)
    		{
        
    			if(Input.GetMouseButton(0))
    			{
    				MouseEvent evt = new MouseEvent(Input.mousePosition.x,Input.mousePosition.y,MouseButton.Left,MouseState.Down,CreateTimeStamp(),this.gameObject.GetComponent<MouseInteraction>(),null);
    				fireEvent(evt);
    			}
    			
    			if(Input.GetMouseButton(1))
    			{
    				MouseEvent evt = new MouseEvent(Input.mousePosition.x,Input.mousePosition.y,MouseButton.Right,MouseState.Down,CreateTimeStamp(),this.gameObject.GetComponent<MouseInteraction>(),null);
    				fireEvent(evt);
    			}
    			
    			if(Input.GetMouseButton(2))
    			{
    				MouseEvent evt = new MouseEvent(Input.mousePosition.x,Input.mousePosition.y,MouseButton.Wheel,MouseState.Down,CreateTimeStamp(),this.gameObject.GetComponent<MouseInteraction>(),null);
    				fireEvent(evt);
    			}	
    			
    
    		}
    		
    		else if(Input.GetMouseButtonUp(0))
    		{
    			MouseEvent evt = new MouseEvent(Input.mousePosition.x,Input.mousePosition.y,MouseButton.Left,MouseState.Up,CreateTimeStamp(),this.gameObject.GetComponent<MouseInteraction>(),null);
    			fireEvent(evt);
    		}
    		
    		else if(Input.GetMouseButtonUp(1))
    		{
    			MouseEvent evt = new MouseEvent(Input.mousePosition.x,Input.mousePosition.y,MouseButton.Right,MouseState.Up,CreateTimeStamp(),this.gameObject.GetComponent<MouseInteraction>(),null);
    			fireEvent(evt);
    		}
    		
    		else if(Input.GetMouseButtonUp(2))
    		{
    			MouseEvent evt = new MouseEvent(Input.mousePosition.x,Input.mousePosition.y,MouseButton.Wheel,MouseState.Up,CreateTimeStamp(),this.gameObject.GetComponent<MouseInteraction>(),null);
    			fireEvent(evt);
    		}
            
            else
            {
                MouseEvent evt = new MouseEvent(Input.mousePosition.x,Input.mousePosition.y,MouseButton.NONE,MouseState.NONE,CreateTimeStamp(),this.gameObject.GetComponent<MouseInteraction>(),null);
                fireEvent(evt);           
            }
    		
    		
    	}
    	
    	
    	
    }

}
