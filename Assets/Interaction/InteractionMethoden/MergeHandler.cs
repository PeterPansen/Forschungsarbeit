using UnityEngine;
using System.Collections;

namespace FAR{

    public class MergeHandler : InteractionMethod , Action {
    
        public void doEvent(InteractionEvent evt)
        {
            
            if(evt.GetType() == typeof(MergeEvent))
            {
                MergeEvent mevt = (MergeEvent) evt;
                
                if(mevt.Dragged == null || mevt.Onto == null)
                {
                    return;
                }
                
                Debug.Log("Now combining " + mevt.Dragged.name + " and "+mevt.Onto.name);
                GameObject[] Nodes = {mevt.Dragged,mevt.Onto};
                if(CheckNodeCombination(Nodes))
                {
                    CombineTwoNodes(mevt);
                }
            }
        }
        
        
        public void CombineTwoNodes(MergeEvent mevt)
        {
            GameObject Drag = mevt.Dragged;
            GameObject Onto = mevt.Onto;
            
            if(Drag.GetComponent<Renderer>().enabled == true && Onto.GetComponent<Renderer>().enabled == true)
            {
                Onto.GetComponent<Renderer>().enabled = false;
                Onto.GetComponent<Collider>().enabled = false;
                
    
                //Second ist ONTO
                s_InstantNodeData Drag_IND = Drag.GetComponent<s_VisualNode>().InstantNodeData;
                s_InstantNodeData Onto_IND = Onto.GetComponent<s_VisualNode>().InstantNodeData;
                
                if(Drag_IND.InputOutput == "Output")
                { 
                    Onto.GetComponent<s_VisualNode>().ShowDesc(false);
                    Onto.GetComponent<s_VisualNode>().SetGuiText("");
                }
                else
                {
                    Drag.GetComponent<s_VisualNode>().ShowDesc(false);
                    Drag.GetComponent<s_VisualNode>().SetGuiText("");
                }
                
                
                if( Drag_IND.InputOutput == "Output" || Onto_IND.InputOutput == "Output" )
                {
                    Drag.GetComponent<s_VisualNode>().SetColor("Blue");
                }
                
                Drag.transform.position = Onto.transform.position;
    
                
                
            }
            
            else
            {
                return;
            }
            
            
        }
        
        public bool CheckNodeCombination(GameObject[] Nodes)
        {
            if(Nodes[0].name == "NewInventory" || Nodes[1].name == "NewInventory")
            {
                Debug.Log("Inventory Selected");
                return false;
            }
            //DFGParser = GameObject.Find ("DFGParser").GetComponent<s_MyDFGParser>();
            GameObject First = Nodes[0];
            GameObject Second = Nodes[1];
            
            s_InstantNodeData First_IND = First.GetComponent<s_VisualNode>().InstantNodeData;
            s_InstantNodeData Second_IND = Second.GetComponent<s_VisualNode>().InstantNodeData;
            
            if(First_IND.PatternName == Second_IND.PatternName)
            {
                Debug.Log ("///Node1 "+First_IND.NodeName+" and Node2 "+Second_IND.PatternName
                           +"have the Patterns "+First_IND.PatternName+" and "+Second_IND.PatternName);
                Debug.Log ("Two Nodes of the same Pattern CANNOT be combined");
                return false;
            }
            else
            {       
                //Wir ergaenzen beide Patterns mit der Information mit wem sie kombiniert wurden
                s_VisualPatternData FirstPattern = First.transform.parent.GetComponent<s_VisualPatternData>();
                s_VisualPatternData SecondPattern = Second.transform.parent.GetComponent<s_VisualPatternData>();
                if(!FirstPattern.RelatedPatterns.Contains(Second.transform.parent.gameObject))
                {
                    Debug.Log ("Adding "+Second.transform.parent.name+" to "+First.transform.parent.name+"'s Relations"); 
                    FirstPattern.RelatedPatterns.Add (Second.transform.parent.gameObject);
                }
                if(!SecondPattern.RelatedPatterns.Contains(First.transform.parent.gameObject))
                {
                    Debug.Log ("Adding "+First.transform.parent.name+" to "+Second.transform.parent.name+"'s Relations"); 
                    SecondPattern.RelatedPatterns.Add (First.transform.parent.gameObject);
                }
                return true;
            }
        }
        
        
    
            
        
        
    }

}
