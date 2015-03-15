using UnityEngine;
using System.Collections;
using Enums;

namespace FAR{

    public class ObjectEvent : InteractionEvent {
    
    	/*
    		StandardEvent for any ObjectInteraction.
    		That is any interaction that requires selection of an object
    	*/
    
    	private GameObject m_Obj;

    	
    	public GameObject Obj
    	{
    		get { return m_Obj; }
    	}
    

    	public ObjectEvent (GameObject obj , ulong ts , InteractionMethod sender, InteractionEvent base_Evt) : base(ts,sender,base_Evt)
    	{
    		this.m_Obj = obj;

    	}
    }

}
