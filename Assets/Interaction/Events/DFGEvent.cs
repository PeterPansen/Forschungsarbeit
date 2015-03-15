using UnityEngine;
using System.Collections;
using Enums;

namespace FAR{

    public class DFGEvent : InteractionEvent {
    
        /// <summary>
        /// Describes whether we IMPORT or EXPORT a DFG
        /// </summary>
        private DFGProcess m_DFGProcess;
        
        /// <summary>
        /// The path of the File in the System
        /// </summary>
        private string m_Path;
        
        public DFGProcess DFGProc
        {
            get { return m_DFGProcess; }
        }
        
        public string Path
        {
            get { return m_Path; }
        }
        
        

        
        public DFGEvent (DFGProcess D,string Path, ulong ts , InteractionMethod sender, InteractionEvent base_Evt) : base(ts,sender,base_Evt)
        {
            this.m_DFGProcess = D;            
            this.m_Path = Path;
        }
}
}