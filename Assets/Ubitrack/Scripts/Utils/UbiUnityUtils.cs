using UnityEngine;
using System.Collections;
using System;

namespace FAR{
	
	public class UbiUnityUtils  {
		
		public static void setGameObjectPose (UbitrackRelativeToUnity relative, GameObject go, Pose pose)
		{
			if (go.GetComponent<Rigidbody>() != null) {
				switch (relative) {
				case UbitrackRelativeToUnity.Local:
					{
	
						throw new NotSupportedException ();
					}
				case UbitrackRelativeToUnity.World:
					{
	
						go.GetComponent<Rigidbody>().MovePosition (pose.pos);
						go.GetComponent<Rigidbody>().MoveRotation (pose.rot);
						break;
					}
				default:
					break;
				}
			} else {
				
				switch (relative) {
				case UbitrackRelativeToUnity.Local:
					{
						go.transform.localPosition = pose.pos;
						go.transform.localRotation = pose.rot;
						break;
					}
				case UbitrackRelativeToUnity.World:
					{
	
						go.transform.position = pose.pos;
						go.transform.rotation = pose.rot;
						break;
					}
				default:
					break;
				}
			}
			 
		}
		
		public static void setGameObjectPosition (UbitrackRelativeToUnity relative, GameObject go, Vector3 pos)
		{
			if (go.GetComponent<Rigidbody>() != null) {
				switch (relative) {
				case UbitrackRelativeToUnity.Local:
					{
	
						throw new NotSupportedException ();
					}
				case UbitrackRelativeToUnity.World:
					{
	
						go.GetComponent<Rigidbody>().MovePosition (pos);					
						break;
					}
				default:
					break;
				}
			} else {
				
				switch (relative) {
				case UbitrackRelativeToUnity.Local:
					{
						go.transform.localPosition = pos;					
						break;
					}
				case UbitrackRelativeToUnity.World:
					{
	
						go.transform.position = pos;					
						break;
					}
				default:
					break;
				}
			}
			 
		}
	
	
	    internal static Ubitrack.SimplePose getGameObjectPose(UbitrackRelativeToUnity relative, GameObject go)
	    {
	        Ubitrack.SimplePose pose = new Ubitrack.SimplePose();
	        Vector3 pos = new Vector3();
	        Quaternion rot = new Quaternion();
	        switch (relative)
	        {
	            case UbitrackRelativeToUnity.Local:
	                {
	                    UbiMeasurementUtils.coordsysemChange(go.transform.localPosition ,ref pos);
	                    UbiMeasurementUtils.coordsysemChange(go.transform.localRotation, ref rot);                    
	                    break;
	                }
	            case UbitrackRelativeToUnity.World:
	                {
	                   UbiMeasurementUtils.coordsysemChange(go.transform.position, ref pos);
	                   UbiMeasurementUtils.coordsysemChange(go.transform.rotation, ref rot);                    
	                    break;
	                }
	            default:
	                break;
	        }
	        pose.tx = pos.x;
	        pose.ty = pos.y;
	        pose.tz = pos.z;
	
	        pose.rx = rot.x;
	        pose.ry = rot.y;
	        pose.rz = rot.z;
	        pose.rw = rot.w;
	        return pose;
	    }
	}

}
