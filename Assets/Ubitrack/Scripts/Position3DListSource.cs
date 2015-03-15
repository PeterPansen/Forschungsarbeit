using UnityEngine;
using System.Collections.Generic;
using Ubitrack;
using System;

namespace FAR{

	//Vorher Position3DListSink jetzt auf UnityPerspektive geändert
	public class Position3DListSource : UbiTrackComponent
	{
	    public UbitrackEventType ubitrackEvent = UbitrackEventType.Push;
	
	    protected SimpleApplicationPullSinkPositionList3D m_position3DListPull = null;
	    protected SimplePositionList3D m_simplePosition3DList = null;	
	
	    protected Unity3DListReceiver m_listReceiver = null;
	    protected Measurement<List<Vector3>> m_data;
	
	    // Use this for initialization    
	    public override void utInit(Ubitrack.SimpleFacade simpleFacade)
	    {
	        base.utInit(simpleFacade);
	
	        switch (ubitrackEvent)
	        {
	            case UbitrackEventType.Pull:
	                {
	                    m_position3DListPull = simpleFacade.getPullSinkPosition3DList(patternID);
	                    m_simplePosition3DList = new SimplePositionList3D();
	                    if (m_position3DListPull == null)
	                    {
	                        throw new Exception("SimpleApplicationPositionList3DReceiver not found for patternID:" + patternID);
	                    }
	                    break;
	                }
	            case UbitrackEventType.Push:
	                {
	                    m_listReceiver = new Unity3DListReceiver();
	
	                    if (!simpleFacade.set3DPositionListCallback(patternID, m_listReceiver))
	                    {
	                        throw new Exception("Simple3DListReceiver could not be set for patternID:" + patternID);
	                    }
	
	                    break;
	                }
	            default:
	                break;
	        }
	    }
	
	    public Measurement<List<Vector3>> getList()
	    {
	        return m_data;
	    }
	
	    void FixedUpdate()
	    {
	        switch (ubitrackEvent)
	        {
	            case UbitrackEventType.Pull:
	                {
	                    ulong lastTimestamp = UbiMeasurementUtils.getUbitrackTimeStamp();
	                    if (m_position3DListPull.getPositionList3D(m_simplePosition3DList, lastTimestamp))
	                    {
	                        m_data = UbiMeasurementUtils.ubitrackToUnity(m_simplePosition3DList);
	                    }
	                    break;
	                }
	            case UbitrackEventType.Push:
	                {
	                    Measurement<List<Vector3>> tmp = m_listReceiver.getData();
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
