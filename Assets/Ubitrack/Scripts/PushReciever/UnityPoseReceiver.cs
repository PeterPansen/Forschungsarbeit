using UnityEngine;
using System.Collections;
using Ubitrack;
using System;

namespace FAR{

	public class UnityPoseReceiver : SimplePoseReceiver
	{    
	    protected Measurement<Pose> m_pose = null;
	
	    private System.Object thisLock = new System.Object();  
	
	    public Measurement<Pose> getData()		 
		{
	        lock (thisLock)
	        {
	            Measurement<Pose> tmp = m_pose;
				m_pose = null;
				return tmp;		
			}
			
	    }
	
	    public override void receivePose(SimplePose newPose)
	    {
	        lock (thisLock)
	        {			
	            m_pose = UbiMeasurementUtils.ubitrackToUnity(newPose);            
	        }
	    }
	}

}