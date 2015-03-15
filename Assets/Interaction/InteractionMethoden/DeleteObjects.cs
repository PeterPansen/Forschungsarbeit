using UnityEngine;
using System.Collections;

namespace FAR{

    public class DeleteObjects : InteractionMethod,Action {
    
        GameObject CurrentSelection;
        
        s_Settings Einstellungen;
    
    	// Use this for initialization
    	void Start () {
    	
            base.Start();
            Einstellungen = GameObject.Find("Settings").GetComponent<s_Settings>();
    	}
        
        // Update is called once per frame
        void Update () {
            
            CheckForNewListeners();
            
        }
        
        public void doEvent(InteractionEvent evt)
        {
            if(evt.GetType() == typeof(SelectionEvent))
            {
                SelectionEvent sevt = (SelectionEvent) evt;
                CurrentSelection = sevt.Obj;
            }
            if(evt.GetType() == typeof(SingleKeyboardEvent) && CurrentSelection != null)
            {
                SingleKeyboardEvent skevt = (SingleKeyboardEvent) evt;
                if(skevt.key.ToString() == Einstellungen.DeleteKey)
                {
                    //Check if its a Node
                    if(CurrentSelection.GetComponent<NodeAction>() != null)
                    {
                        DeleteEvent devt = new DeleteEvent(CurrentSelection,Enums.GraphElementType.Node,
                        CurrentSelection.GetComponent<s_VisualNode>().InstantNodeData.PatternName,CreateTimeStamp(),
                        this.GetComponent<InteractionMethod>(),skevt);
                        fireEvent(devt);
                    }
                    //Check if its an Edge
                    else if(CurrentSelection.GetComponent<EdgeAction>() != null)
                    {
                        DeleteEvent devt = new DeleteEvent(CurrentSelection,Enums.GraphElementType.Edge,
                        CurrentSelection.GetComponent<s_VisualEdge>().InstantEdgeData.PatternName,CreateTimeStamp(),
                        this.GetComponent<InteractionMethod>(),skevt);
                        fireEvent(devt);
                    }
                }
            }
        }
    	
    }
}