using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace FAR{

    public class DefaultAction : MonoBehaviour , Action {
    
        //Items important for the custom editor 
        //START
        public GameObject Placeholder;
        public List<int> SelectedScriptNumber = new List<int>();
        public List<int> SelectedMethodNumber = new List<int>();
        //END
        
        //Enthält alle Methoden die vom User als Callable deklariert wurden
        public List<string> MethodsToCall = new List<string>();
        //Enthält die zugehörigen Objekte die diese Methoden beinhalten
        public List<GameObject> Callers = new List<GameObject>();
    
        
    	public void doEvent(InteractionEvent evt)
        {
            //Im Falle eines eingehenden Events werden alle Methoden aufgerufen
            for ( int i = 0; i < Callers.Count; i++ )
            {
                Callers[i].BroadcastMessage(MethodsToCall[i]);
            }

        }

    }

}
