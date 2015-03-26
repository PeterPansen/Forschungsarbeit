using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FAR{

    public class InteractionMethod : MonoBehaviour {
    	
    	public List<Action> Actions = new List<Action>();
    	public List<GameObject> Listeners = new List<GameObject>();
    	
    	//
    	//public int counter;
    	//
    
    	public ulong CreateTimeStamp()
    	{
    		return UbiMeasurementUtils.getUbitrackTimeStamp();
    	}
    	
    	/// <summary>
    	/// Checks the attached LIST<Gameobject> for new entries and calls "GetAllActionsOnObjs"
    	/// </summary>
    	public void CheckForNewListeners()
    	{
    		if(Listeners.Count > 0)
    		{
    			GetAllActionsOnObjs();
    		}
    	}
        
        virtual public void Start()
        {
            //Actions = new List<Action>();
            GetAllActionsOnObjs();
        }
    
    
    	/// <summary>
    	/// Durchforstet die GameObjects Listeners nach Actions-Skripten und fügt sie den bekannten Aktionen hinzu 
    	/// </summary>
    	public void GetAllActionsOnObjs()
    	{
    		foreach(GameObject listener in Listeners)
    		{
    			//Sucht alle Skripten am angehängten Objekt heraus
    			if(listener != null)
    			{
    				MonoBehaviour[] AllScriptsOnObj = listener.GetComponents<MonoBehaviour>();
    			
    			
	    			foreach(MonoBehaviour temp in AllScriptsOnObj)
	    			{
	    				//Nutzt das Objekt das ACTION-Interface...
	    				if(temp is Action)
	    				{
	    					//...wird es zu unseren Aktionen hinzugefügt und reagiert auf EVENTS
	    					Actions.Add((Action)temp);
	    				}
	    			}
    			}
    
    		}
    		
    		//Listeners am Ende clearen. Neue Objekte werden der leeren Liste hinzugefügt. 
    		//Hierdurch können wir Änderungen an einer NICHT-leeren Liste erkennen.
    		Listeners.Clear();
    
    	}
    	
    	/// <summary>
    	/// Sends your event to all listening scripts that implemented the Action-Interface.
    	/// Nifty!
    	/// </summary>
    	/// <param name="evt">Evt.</param>
    	protected void fireEvent(InteractionEvent evt) {
    		foreach(Action a in Actions){
    			a.doEvent(evt);
    		}
    	}
    }

}
