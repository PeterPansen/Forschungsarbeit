using UnityEngine;
using System.Collections;

namespace FAR{

    public class SingleKeyboardEvent : InteractionEvent {
        
        /*
            Passes information about a KEY-BUTTON that was pressed on the keyboard
        */
        
        private char m_key;
        
        public char key
        {
            get { return m_key; }
        }
        
        public SingleKeyboardEvent ( char key, ulong ts, InteractionMethod sender, InteractionEvent base_evt ) : base(ts,sender,base_evt)
        {
            this.m_key = key;
        }
        
        
    }

}
