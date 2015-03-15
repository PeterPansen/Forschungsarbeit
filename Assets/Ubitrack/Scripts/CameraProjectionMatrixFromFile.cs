using UnityEngine;
using System.Collections;

namespace FAR{

	public class CameraProjectionMatrixFromFile : MonoBehaviour {
	
	    public TextAsset intrinsicsFile = null;
	    public int standardWidth = 640;
	    public int standardHeight = 480;
	    public float nearClipping = 0.01f;
	    public float farClipping = 100.0f;
	    public float[] intrinsics = new float[9];
	    public TextAsset projectionFile = null;
	
	    // Use this for initialization
	    void Start()
	    {
	        // Set projection matrix         
	        Matrix4x4 mProj;
	        if (intrinsicsFile != null)
	        {
	            Debug.Log("UbiCam Resolution: " + standardWidth + "x" + standardHeight + " from 3x3 file:" + intrinsicsFile.name);
	            float[] d3x3 = UbiFileUtils.read3x3MatrixFromFile(intrinsicsFile.text);
	            mProj = CameraUtils.constructProjectionMatrix3x3(d3x3, standardWidth, standardHeight, nearClipping, farClipping);
	        } else if(projectionFile != null)
	        {
	            Debug.Log("UbiCam Resolution: " + standardWidth + "x" + standardHeight + " from 3x4 file:" + projectionFile.text);
	            float[] projection = UbiFileUtils.read3x4MatrixFromFile(projectionFile.text);
	            mProj = CameraUtils.constructProjectionMatrix3x4(projection, standardWidth, standardHeight, nearClipping, farClipping);
	        } else
	        {
	            Debug.Log("UbiCam Resolution: " + standardWidth + "x" + standardHeight + " from static array:" + intrinsics);            
	            mProj = CameraUtils.constructProjectionMatrix3x3(intrinsics, standardWidth, standardHeight, nearClipping, farClipping);
	        }
	
	        
	        
	
	
	
	        Debug.Log(mProj);
	
	        GetComponent<Camera>().projectionMatrix = mProj;
	    }
	
	   
	
	   
	}

}
