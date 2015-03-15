using UnityEngine;
using System.Collections;
using Enums;

namespace FAR{

    public class SaveLoadEvent : InteractionEvent {
    
    
        private SL_State m_SaveLoad;
        
        public SL_State SaveLoad
        {
            get { return m_SaveLoad; }
        }
        
        
        public SaveLoadEvent (SL_State SL, ulong ts , InteractionMethod sender, InteractionEvent base_Evt) : base(ts,sender,base_Evt)
        {
            this.m_SaveLoad = SL;
            
        }
    }
}
