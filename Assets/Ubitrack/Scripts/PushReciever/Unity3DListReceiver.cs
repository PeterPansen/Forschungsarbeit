using UnityEngine;
using System.Collections;
using Ubitrack;
using System;
using System.Collections.Generic;

namespace FAR{

	public class Unity3DListReceiver : SimplePositionList3DReceiver
	{
	    protected Measurement<List<Vector3>> m_data = null;
	    private System.Object thisLock = new System.Object();
	
	    public Measurement<List<Vector3>> getData()
	    {
	        lock (thisLock)
	        {
	            Measurement<List<Vector3>> tmp = m_data;
	            m_data = null;
	            return tmp;
	        }
	
	    }
	
	    public override void receivePositionList3D(SimplePositionList3D position3dList)
	    {
	        lock (thisLock)
	        {
	            m_data = UbiMeasurementUtils.ubitrackToUnity(position3dList);
	        }
	    }
	}

}