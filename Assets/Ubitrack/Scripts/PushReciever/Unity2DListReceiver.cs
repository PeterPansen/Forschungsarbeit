using UnityEngine;
using System.Collections;
using Ubitrack;
using System;
using System.Collections.Generic;

namespace FAR{

	public class Unity2DListReceiver : SimplePosition2DListReceiver
	{
	    protected Measurement<List<Vector2>> m_data = null;
	    private System.Object thisLock = new System.Object();
	
	    public Measurement<List<Vector2>> getData()
	    {
	        lock (thisLock)
	        {
	            Measurement<List<Vector2>> tmp = m_data;
	            m_data = null;
	            return tmp;
	        }
	
	    }
	
	    public override void receivePosition2DList(SimplePosition2DList position2dList)
	    {
	        lock (thisLock)
	        {
	            m_data = UbiMeasurementUtils.ubitrackToUnity(position2dList);
	        }
	    }
	}

}
