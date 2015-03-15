using UnityEngine;
using System.Collections;
using Ubitrack;
using System;

namespace FAR{

	public class UnityPositionReceiver  : SimplePosition3DReceiver
	{    
	    protected Measurement<Vector3> m_position = null;
	
	    private System.Object thisLock = new System.Object();  
	
	    public Measurement<Vector3> getData()		 
		{
	        lock (thisLock)
	        {
	            Measurement<Vector3> tmp = m_position;
				m_position = null;
				return tmp;		
			}
			
	    }
	
	    public override void receivePosition3D(SimplePosition3D newPosition)
	    {
	        lock (thisLock)
	        {			
	            m_position = UbiMeasurementUtils.ubitrackToUnity(newPosition);            
	        }
	    }
	}

}
