using UnityEngine;
using System.Collections;
using Enums;

namespace FAR{

    public class LayoutEvent : InteractionEvent {
    
        /*
            This Event changes the current Camera-LayoutMode
        */
    
    
        private LayoutMode m_Lay;
        
        
        public LayoutMode LayMode
        {
            get { return m_Lay; }
        }
        
        
        public LayoutEvent (LayoutMode Lay, ulong ts , InteractionMethod sender, InteractionEvent base_Evt) : base(ts,sender,base_Evt)
        {
            this.m_Lay = Lay;
            
        }
    }
}
