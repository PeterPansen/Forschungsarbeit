using UnityEngine;
using System.Collections;
using Enums;

namespace FAR{

    public class SelectionEvent : ObjectEvent {
    
    	/*
    		This event is sent for every "one-click" selection.
    		It differs from DragEvents that require a held-MouseButton
    	*/
    
    	public SelectionEvent(GameObject obj , ulong ts , InteractionMethod sender, InteractionEvent base_evt) : base(obj,ts,sender,base_evt)
    	{
    		//SelectionEvent hat noch keine eigenen Variablen die nötig sind
    	}
    }

}
