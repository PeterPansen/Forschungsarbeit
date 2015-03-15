using UnityEngine;
using System.Collections;
using Ubitrack;

namespace FAR{

	public class Unity3x4MatrixReceiver : SimpleMatrix3x4Receiver
	{
	    protected Measurement<float[]> m_matrix = null;
	    private System.Object thisLock = new System.Object();
	
	    public Measurement<float[]> getData()
	    {
	        lock (thisLock)
	        {
	            Measurement<float[]> tmp = m_matrix;
	            m_matrix = null;
	            return tmp;
	        }
	
	    }
	
	    public override void receiveMatrix3x4(SimpleMatrix3x4 newMatrix)
	    {
	        lock (thisLock)
	        {
	            m_matrix = UbiMeasurementUtils.ubitrackToUnity(newMatrix);
	        }
	    }
	}
}