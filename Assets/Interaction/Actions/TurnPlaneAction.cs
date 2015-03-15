using UnityEngine;
using System.Collections;

namespace FAR{

    public class TurnPlaneAction : MonoBehaviour , Action {
    
    	public void doEvent(InteractionEvent evt)
    	{
    		if(evt.GetType() == typeof(KeyboardEvent))
    		{
    			KeyboardEvent kevt = (KeyboardEvent) evt;
    			
    			Vector3 currScale = transform.localScale;
    			
    			if(kevt.key == 'w')
    			{
    				this.transform.localScale = new Vector3(currScale.x+0.5f,currScale.y+0.5f,currScale.z+0.5f);
    			}
    			
    			if(kevt.key == 's')
    			{
    				this.transform.localScale = new Vector3(currScale.x-0.5f,currScale.y-0.5f,currScale.z-0.5f);
    			}
    			
    			if(kevt.key == 'a')
    			{
    				Quaternion r = this.transform.localRotation;
    				this.transform.localRotation = new Quaternion(r.x,r.y+0.2f,r.z,r.w);
    			}
    			
    			if(kevt.key == 'd')
    			{
    				Quaternion r = this.transform.localRotation;
    				this.transform.localRotation = new Quaternion(r.x,r.y-0.2f,r.z,r.w);
    			}
    		}
    	}
    	
    	// Use this for initialization
    	void Start () {
    	
    	}
    	
    	// Update is called once per frame
    	void Update () {
    	
    	}
    }

}
