using UnityEngine;
using System.Collections;
using System;
using Ubitrack;

namespace FAR{
	
	public class CameraProjectionMatrixFrom4x4Matrix : UbiTrackComponent {
	public UbitrackEventType ubitrackEvent = UbitrackEventType.Push;    
	
	    public bool UseMainCamera=false;
	
	    public float standardWidth = 800;
	    public float standardHeight = 600;
	    public float nearClipping = 0.01f;
	    public float farClipping = 100.0f;
	
	    protected Unity4x4MatrixReceiver m_matrixReceiver = null;    
	  
	
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
	                    m_matrixReceiver = new Unity4x4MatrixReceiver();
	
	
	                    if (!simpleFacade.setMatrix4x4Callback(patternID, m_matrixReceiver))
	                    {
	                        throw new Exception("UnityMatrixReciever could not be set for matrixID:" + patternID);
	                    }
	                    break;
	                }
	            default:
	                break;
	        }
			
			standardWidth = Screen.width;
			standardHeight = Screen.height;
			//standardWidth =0.38f;
			//standardHeight =0.16f;
	    }
	
	    void Update()
	    {
	        Measurement<Matrix4x4> newData = null;
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
	            //Matrix4x4 proj = CameraUtils.constructProjectionMatrix4x4(newData.data(), standardWidth, standardHeight, nearClipping, farClipping);
				Matrix4x4 proj = newData.data();
	            Debug.Log(proj);
	            
				if (UseMainCamera)
	                Camera.main.projectionMatrix = proj;
	            else
	                GetComponent<Camera>().projectionMatrix = proj;
	                
	        }
	
	    }
	}

}
