using UnityEngine;
using System.Collections;
using Ubitrack;

namespace FAR{

	public class Unity4x4MatrixReceiver : SimpleMatrix4x4Receiver {
	
		protected Measurement<Matrix4x4> m_matrix = null;
	    private System.Object thisLock = new System.Object();
	
	    public Measurement<Matrix4x4> getData()
	    {
	        lock (thisLock)
	        {
	            Measurement<Matrix4x4> tmp = m_matrix;
	            m_matrix = null;
	            return tmp;
	        }
	
	    }
	
	    public override void receiveMatrix4x4(SimpleMatrix4x4 newMatrix)
	    {
	        lock (thisLock)
	        {
	            m_matrix = UbiMeasurementUtils.ubitrackToUnity(newMatrix);
	        }
	    }
	}

}
