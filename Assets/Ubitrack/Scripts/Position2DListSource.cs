using UnityEngine;
using System.Collections.Generic;
using Ubitrack;
using System;

namespace FAR{

	//Vorher Position2DListSink jetzt auf UnityPerspektive geändert
	public class Position2DListSource : UbiTrackComponent
	{
	    public UbitrackEventType ubitrackEvent = UbitrackEventType.Push;
	        
	    protected Unity2DListReceiver m_listReceiver = null;
	    protected Measurement<List<Vector2>> m_data;
	
	    // Use this for initialization    
	    public override void utInit(Ubitrack.SimpleFacade simpleFacade)
	    {
	        base.utInit(simpleFacade);
	
	        switch (ubitrackEvent)
	        {
	            case UbitrackEventType.Pull:
	                {
	                    throw new Exception("Pull event not yet supported");
	                }
	            case UbitrackEventType.Push:
	                {
	                    m_listReceiver = new Unity2DListReceiver();
	
	                    if (!simpleFacade.setPosition2DListCallback(patternID, m_listReceiver))
	                    {
	                        throw new Exception("Simple2DListReceiver could not be set for patternID:" + patternID);
	                    }
	
	                    break;
	                }
	            default:
	                break;
	        }
	    }
	
	    public Measurement<List<Vector2>> getList()
	    {
	        return m_data;
	    }
	
	    void FixedUpdate()
	    {        
	        switch (ubitrackEvent)
	        {
	            case UbitrackEventType.Pull:
	                {                    
	                    break;
	                }
	            case UbitrackEventType.Push:
	                {
	                    Measurement<List<Vector2>> tmp = m_listReceiver.getData();
	                    if (tmp != null)
	                        m_data = tmp;                    
	                    break;
	                }
	            default:
	                break;
	        }        
	    }
	}

}
