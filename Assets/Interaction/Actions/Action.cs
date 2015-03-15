using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FAR{

    public interface Action  {
    
    	
    	void doEvent(InteractionEvent evt);
    	
    
    	//public void undoEvent(InteractionEvent evt);
    }
    
    public interface UndoableAction : Action  {
    	
    	void undoEvent(InteractionEvent evt);
    }

}
