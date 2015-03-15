using UnityEngine;
using System.Collections;
using System;
using Ubitrack;

namespace FAR{

	public class CameraProjectionMatrixFrom3x4Matrix : UbiTrackComponent{
	    public UbitrackEventType ubitrackEvent = UbitrackEventType.Push;    
	
	    public bool UseMainCamera=false;
	
	    public int standardWidth = 800;
	    public int standardHeight = 600;
	    public float nearClipping = 0.01f;
	    public float farClipping = 100.0f;
	
	    protected Unity3x4MatrixReceiver m_matrixReceiver = null;    
	  
	
	    //private ulong lastTimestamp = 0;
	    // Use this for initialization
	    public override void utInit(Ubitrack.SimpleFacade simpleFacade)
	    {
	        base.utInit(simpleFacade);
	
	        switch (ubitrackEvent)
	        {
	            case UbitrackEventType.Pull:
	                {
	                    throw new Exception("Pull not supported yet");                    
	                }
	            case UbitrackEventType.Push:
	                {
	                    m_matrixReceiver = new Unity3x4MatrixReceiver();
	
	
	                    if (!simpleFacade.setMatrix3x4Callback(patternID, m_matrixReceiver))
	                    {
	                        throw new Exception("UnityMatrixReciever could not be set for matrixID:" + patternID);
	                    }
	                    break;
	                }
	            default:
	                break;
	        }
	    }
	
	    void Update()
	    {
	        Measurement<float[]> newData = null;
	        switch (ubitrackEvent)
	        {
	            case UbitrackEventType.Pull:
	                {
	                    
	                    break;
	                }
	            case UbitrackEventType.Push:
	                {
	                    newData = m_matrixReceiver.getData();                                       
	                    break;
	                }
	            default:
	                break;
	        }
	
	        if (newData != null)
	        {
	            Matrix4x4 proj = CameraUtils.constructProjectionMatrix3x4(newData.data(), standardWidth, standardHeight, nearClipping, farClipping);
	            Debug.Log(proj);
	            if (UseMainCamera)
	                Camera.main.projectionMatrix = proj;
	            else
	                GetComponent<Camera>().projectionMatrix = proj;
	        }
	
	    }
	
	
	}

}
