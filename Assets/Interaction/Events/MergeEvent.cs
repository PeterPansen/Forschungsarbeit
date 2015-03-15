using UnityEngine;
using System.Collections;

namespace FAR{

    public class MergeEvent : InteractionEvent {
    
        /*
            Wird für jede MergeOperation von zwei GraphenElementen genutzt
        */
    
    	private GameObject m_Dragged;
        private GameObject m_Onto;
        
        //NODE oder EDGE
        private string m_ElementType;
        
    
        public GameObject Dragged
        {
            get {   return m_Dragged;   }
        }
        
        public GameObject Onto
        {
            get {   return m_Onto;  }
        }
        
        public string ElementType
        {
            get {   return m_ElementType;   }
        }
        
        
        
        public MergeEvent (GameObject Dragged, GameObject Onto, string TypeOfElement ,ulong ts , InteractionMethod sender, InteractionEvent base_evt) : base(ts,sender,base_evt)
        {
            this.m_Dragged = Dragged;
            this.m_Onto = Onto;
            this.m_ElementType = TypeOfElement;
        }
    }

}
