using UnityEngine;
using System.Collections;
using FAR;

public class ExampleAction : MonoBehaviour, Action {

	public void doEvent(InteractionEvent evt){
		if(evt.GetType() == typeof(KeyboardEvent)  ) {
			KeyboardEvent kevt = (KeyboardEvent) evt;
				Debug.Log("Keyboard Event:" + kevt.key + " Timestamp: "+ kevt.timestamp);
		
		}
        if(evt.GetType() == typeof(SpawnEvent))
        {
            SpawnEvent sevt = (SpawnEvent) evt;
                Debug.Log("SpawnEvent received "+sevt.timestamp);
        }
        
        Debug.Log("RECEIVED SOMETHING");
	}
}
