using UnityEngine;
using System.Collections;
using Ubitrack;

namespace FAR{

	public class UnityErrorPoseReceiver : SimpleErrorPoseReceiver
	{
	    protected Measurement<ErrorPose> m_pose = null;
	
	    private System.Object thisLock = new System.Object();
	
	    public Measurement<ErrorPose> getData()
	    {
	        lock (thisLock)
	        {
	            Measurement<ErrorPose> tmp = m_pose;
	            m_pose = null;
	            return tmp;
	        }
	
	    }
	
	    public override void receiveErrorPose(SimpleErrorPose newPose)
	    {
	        lock (thisLock)
	        {
	            m_pose = UbiMeasurementUtils.ubitrackToUnity(newPose);
	        }
	    }
	}

}
