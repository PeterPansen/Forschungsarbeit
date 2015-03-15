using UnityEngine;
using System.Collections;

namespace FAR{

    public class HoverEvent : ObjectEvent {
    
        public HoverEvent(GameObject obj , ulong ts , InteractionMethod sender, InteractionEvent base_evt) : base(obj,ts,sender,base_evt)
        {
            //HoverEvent hat noch keine eigenen Variablen die nötig sind
        }
    }

}
