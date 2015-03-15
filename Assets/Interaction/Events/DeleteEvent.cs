using UnityEngine;
using System.Collections;
using Enums;

namespace FAR{
    
    public class DeleteEvent : InteractionEvent {
        
        /// <summary>
        /// Describes whether we IMPORT or EXPORT a DFG
        /// </summary>
        private GameObject m_DeletedObj;
        /// <summary>
        /// The type of the deleted Object (NODE or EDGE)
        /// </summary>
        private GraphElementType m_Type;
        
        /// <summary>
        /// The pattern this Obj belonged to
        /// </summary>
        private string m_ParentPattern;
        
        public string ParentPattern
        {
            get { return m_ParentPattern; }
        }

        
        public GameObject DeletedObj
        {
            get { return m_DeletedObj; }
        }
        
        public GraphElementType Type
        {
            get { return m_Type; }
        }

        public DeleteEvent (GameObject DelObj, GraphElementType type,string ParentPattern, ulong ts , InteractionMethod sender, InteractionEvent base_Evt) : base(ts,sender,base_Evt)
        {
            this.m_DeletedObj = DelObj;
            this.m_Type = type;
            this.m_ParentPattern = ParentPattern;
        }
    }
}
