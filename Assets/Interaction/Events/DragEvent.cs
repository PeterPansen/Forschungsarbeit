using UnityEngine;
using System.Collections;
using Ubitrack;
using Enums;

namespace FAR{

    public class DragEvent : ObjectEvent {
    
    	/*
    		Extending ObjectEvent this Event is sent when moving an object on the screen.
    		It contains information about TIMESTAMP,SENDER-INTERACTIONMETHOD,MOVEDOBJECT,TRIGGERBUTTON (The button that triggered the movement),
    		and the NEWPOS as an UBITRACK-POSE
    	*/
        

    
    	private Pose m_newPos;
    	
    	public Pose newPos
    	{
    		get { return m_newPos; }
    	}
        

    
    	public DragEvent (Pose nPos, GameObject obj , ulong ts , InteractionMethod sender, InteractionEvent base_evt) : base(obj,ts,sender,base_evt)
    	{
    		this.m_newPos = nPos;
    	}
    }

}