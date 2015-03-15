using UnityEngine;
using System.Collections;
using System;
using Ubitrack;

namespace FAR{

	public class CameraProjectionMatrixFrom3x3Matrix : UbiTrackComponent {    
	    
	    public int standardWidth = 640;
	    public int standardHeight = 480;
	    public float nearClipping = 0.01f;
	    public float farClipping = 100.0f;
	
	    private SimpleApplicationPullSinkMatrix3x3 m_matrixPull = null;
	
		// Use this for initialization
	    public override void utInit(Ubitrack.SimpleFacade simpleFacade)
	    {
	        base.utInit(simpleFacade);
	
	        m_matrixPull = simpleFacade.getPullSinkMatrix3x3(patternID);
	        if (m_matrixPull == null)
	        {
	            throw new Exception("SimpleApplicationPushSourceButton not found for ID:" + patternID);
	        }
	        
		}
	
	    public override void utStart()
	    {
	        base.utStart();
	        SimpleMatrix3x3 matrix = new SimpleMatrix3x3();
	        if (m_matrixPull.getMatrix3x3(matrix, UbiMeasurementUtils.getUbitrackTimeStamp()))
	        {
	            Measurement<float[]> intrinsics = UbiMeasurementUtils.ubitrackToUnity(matrix);
	            
	            Matrix4x4 mProj;
	            mProj = CameraUtils.constructProjectionMatrix3x3(intrinsics.data(), standardWidth, standardHeight, nearClipping, farClipping);
	            Debug.Log(mProj);
	            GetComponent<Camera>().projectionMatrix = mProj;
	        }
	        else
	        {
	            throw new Exception("unable to get 3x3 matrix");
	        }
	    }
	}

}
