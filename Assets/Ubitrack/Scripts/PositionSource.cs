using UnityEngine;
using System;
using System.Collections;
using Ubitrack;

namespace FAR{

	//Vorher PositionSink jetzt auf UnityPerspektive geändert
	public class PositionSource : UbiTrackComponent {   
		public UbitrackEventType ubitrackEvent = UbitrackEventType.Push;
		public UbitrackRelativeToUnity relative = UbitrackRelativeToUnity.World;
	
	    protected SimpleApplicationPullSinkPosition3D m_positionPull = null;
	    protected SimplePosition3D m_simplePosition = null;	
	
		protected UnityPositionReceiver m_positionReceiver = null;	
		protected Measurement<Vector3> m_position;		
		
		// Use this for initialization    
	    public override void utInit(Ubitrack.SimpleFacade simpleFacade)
	    {
	        base.utInit(simpleFacade);
	        
	        		
			switch(ubitrackEvent)
			{
			case UbitrackEventType.Pull:{
	            m_positionPull = simpleFacade.getPullSinkPosition3D(patternID);
	            m_simplePosition = new SimplePosition3D();
				 	if (m_positionPull == null)
				    {
	                    throw new Exception("SimpleApplicationPullSinkPosition3D not found for poseID:" + patternID);
				    }
					break;
				}
			case UbitrackEventType.Push:{
	            m_positionReceiver = new UnityPositionReceiver();
	
	            if (!simpleFacade.set3DPositionCallback(patternID, m_positionReceiver))
					{
	                    throw new Exception("UnityPositionReceiver could not be set for poseID:" + patternID);
					}
	              
					break;
				}
			default:
			break;
			}    		
		}
		
	    void FixedUpdate()
	    {
	        m_position = null;
		
			switch(ubitrackEvent)
			{
			case UbitrackEventType.Pull:{				
					ulong lastTimestamp =  UbiMeasurementUtils.getUbitrackTimeStamp();
					if(m_positionPull.getPosition3D(m_simplePosition, lastTimestamp))
					{					
	                    m_position = UbiMeasurementUtils.ubitrackToUnity(m_simplePosition);    
					}	
					break;
				}
			case UbitrackEventType.Push:{
	            m_position = m_positionReceiver.getData();
					break;
				}
			default:
			break;
			}
	
	        if (m_position != null)
	        {
	            UbiUnityUtils.setGameObjectPosition(relative, gameObject, m_position.data());
	        }
	        	
	    }
	    
	}

}