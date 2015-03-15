using UnityEngine;
using System.Collections;
using System;

namespace FAR{

	public class UbiTrackComponent : MonoBehaviour
	{
	    public string patternID;
	    public SimpleFacade facade = null;
		
		
		protected Ubitrack.SimpleFacade ubiFacade = null;
	
	    public void Awake(){
	        if (facade == null)
	        {
				facade = (SimpleFacade) GameObject.FindObjectOfType(typeof(SimpleFacade));			
	        }
			
			bool wasEnabled = this.enabled;
			this.enabled = false;
			
			if (facade == null)
				throw new Exception("SimpleFacade must not be null");
			
	        if(wasEnabled)
	            facade.registerUbitrackComponent(this);
	        
	    }
	
	    public virtual void utInit(Ubitrack.SimpleFacade simpleFacade)
	    {         
	        if (patternID == null || patternID.Length == 0)
	        {
	            throw new MissingMemberException("patternID is not set in gameobject: "+gameObject.name);
	        }
			ubiFacade = simpleFacade;
	    }
	    
	    public virtual void utDestroy()
	    {
	    }
	
	    public virtual void utStart()
	    {
	        this.enabled = true;
	    }
	
	    public virtual void utStop()
	    {
	        this.enabled = false;
	    }
				
	}

}
