using UnityEngine;
using System.Collections;

namespace FAR{
	
	
	public class Measurement<T>  {
		private ulong m_timestamp;
		private T m_data;
		
		public Measurement(T data, ulong timestamp){
			m_data = data;
			m_timestamp = timestamp;	
		}
		
		public Measurement(T data){
			m_data = data;
			m_timestamp = FAR.UbiMeasurementUtils.getUbitrackTimeStamp();	
		}
		
		public ulong time(){
			return m_timestamp;
		}
		
		public T data(){
			return m_data;
		}
		
	}
	
	public class Pose {
	    public Pose() { 
	        pos = new Vector3();
		    rot = new Quaternion();
	    }
	    public Pose(Transform trans){		
			if(trans == null)
				return;
	        pos = trans.position;
	        rot = trans.rotation;
	    }
	    public Pose(Vector3 pos, Quaternion rot)
	    {
	        this.pos = pos;
	        this.rot = rot;
	    }
	    public void setTranformValues(Transform trans)
	    {
	        trans.position = pos;
	        trans.rotation = rot;
	    }
		public Vector3 pos;
		public Quaternion rot;
	}
	
	public class ErrorPose : Pose
	{
	    public double[,] covariance = new double[6, 6];
	}

}
