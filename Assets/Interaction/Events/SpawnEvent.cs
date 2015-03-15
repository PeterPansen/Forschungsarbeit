using UnityEngine;
using System.Collections;
using Enums;

namespace FAR{

    public class SpawnEvent : InteractionEvent {
    
    	
        private GraphElementType m_Type;
        
        private string m_OutputInput;
        
        private s_InstantPatternData m_Pattern;
        
        
        public GraphElementType Type
        {
            get { return m_Type; }
        }
        
        public string OutputInput
        {
            get { return m_OutputInput; }
        }
        
        public s_InstantPatternData Pattern
        {
            get { return m_Pattern; }
        }

        public SpawnEvent (ulong ts,InteractionEvent base_Evt,InteractionMethod sender,GraphElementType type,string OutIn,s_InstantPatternData Pat) : base(ts,sender,base_Evt)
        {
            this.m_Type = type;
            this.m_OutputInput = OutIn;
            this.m_Pattern = Pat;
        }
    }

}
