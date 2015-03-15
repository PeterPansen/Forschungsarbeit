using UnityEngine;
using System;
using System.Collections;
using Ubitrack;

namespace FAR{

	//Vorher ErrorPoseSink jetzt auf UnityPerspektive geändert
	public class ErrorPoseSource : UbiTrackComponent {	
		public UbitrackEventType ubitrackEvent = UbitrackEventType.Push;
		public UbitrackRelativeToUnity relative = UbitrackRelativeToUnity.World;
			
		protected SimpleApplicationPullSinkErrorPose m_posePull = null;
	    protected SimpleErrorPose m_simplePose = null;
	
		protected UnityErrorPoseReceiver m_poseReceiver = null;	
	    protected Measurement<ErrorPose> m_pose;	
		
		// Use this for initialization
	    public override void utInit(Ubitrack.SimpleFacade simpleFacade)
	    {
	        base.utInit(simpleFacade);   
	        
			
			
			switch(ubitrackEvent)
			{
			case UbitrackEventType.Pull:{
					m_posePull = simpleFacade.getSimplePullSinkErrorPose(patternID);
	                m_simplePose = new SimpleErrorPose();				
				 	if (m_posePull == null)
				    {
	                    throw new Exception("SimpleApplicationPullSinkErrorPose not found for poseID:" + patternID);
				    }
					break;
				}
			case UbitrackEventType.Push:{
	            m_poseReceiver = new UnityErrorPoseReceiver();
	
	
	            if (!simpleFacade.setErrorPoseCallback(patternID, m_poseReceiver))
					{
	                    throw new Exception("SimpleErrorPoseReceiver could not be set for poseID:" + patternID);
					}
					break;
				}
			default:
			break;
			}    		
		}
		
	    void FixedUpdate()
	    { 
			m_pose = null;
		
			switch(ubitrackEvent)
			{
			case UbitrackEventType.Pull:{				
					ulong lastTimestamp =  UbiMeasurementUtils.getUbitrackTimeStamp();
					if(m_posePull.getPose(m_simplePose, lastTimestamp))
					{					
	                    m_pose = UbiMeasurementUtils.ubitrackToUnity(m_simplePose);    
					}	
					break;
				}
			case UbitrackEventType.Push:{
	            m_pose = m_poseReceiver.getData();
					break;
				}
			default:
			break;
			}
	
	        if (m_pose != null)
	        {
	            UbiUnityUtils.setGameObjectPose(relative, gameObject, m_pose.data());
	        }
	        	
	    }
	
	}

}
