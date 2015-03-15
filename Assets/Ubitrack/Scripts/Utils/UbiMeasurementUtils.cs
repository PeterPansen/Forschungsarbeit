using UnityEngine;
using System.Collections.Generic;
using System;
using Ubitrack;

namespace FAR{

	public enum UbitrackRelativeToUnity
	{
	    Local,
	    World
	};
	
	public enum UbitrackEventType
	{
	    Push,
	    Pull
	};
	
	public class UbiMeasurementUtils  {
		
	
		
	    static readonly DateTime StartOfEpoch = new DateTime (1970, 1, 1);
	
		public static ulong getUbitrackTimeStamp ()
		{        
			ulong millis = (ulong)(DateTime.UtcNow - StartOfEpoch).Ticks / TimeSpan.TicksPerMillisecond;
			return millis * 1000000;
		}
	
	    public static void ubitrackCovarianceToFloatArray(SimpleErrorPose ubitrackPose, ref double[,] covariance)
	    {
	        // todo needs rhs lhs transfrom
	        covariance[0, 0] = (float)ubitrackPose.co11;
	        covariance[0, 1] = (float)ubitrackPose.co12;
	        covariance[0, 2] = (float)ubitrackPose.co13;
	        covariance[0, 3] = (float)ubitrackPose.co14;
	        covariance[0, 4] = (float)ubitrackPose.co15;
	        covariance[0, 5] = (float)ubitrackPose.co16;
	
	        covariance[1, 0] = (float)ubitrackPose.co21;
	        covariance[1, 1] = (float)ubitrackPose.co22;
	        covariance[1, 2] = (float)ubitrackPose.co23;
	        covariance[1, 3] = (float)ubitrackPose.co24;
	        covariance[1, 4] = (float)ubitrackPose.co25;
	        covariance[1, 5] = (float)ubitrackPose.co26;
	
	        covariance[2, 0] = (float)ubitrackPose.co31;
	        covariance[2, 1] = (float)ubitrackPose.co32;
	        covariance[2, 2] = (float)ubitrackPose.co33;
	        covariance[2, 3] = (float)ubitrackPose.co34;
	        covariance[2, 4] = (float)ubitrackPose.co35;
	        covariance[2, 5] = (float)ubitrackPose.co36;
	
	        covariance[3, 0] = (float)ubitrackPose.co41;
	        covariance[3, 1] = (float)ubitrackPose.co42;
	        covariance[3, 2] = (float)ubitrackPose.co43;
	        covariance[3, 3] = (float)ubitrackPose.co44;
	        covariance[3, 4] = (float)ubitrackPose.co45;
	        covariance[3, 5] = (float)ubitrackPose.co46;
	
	        covariance[4, 0] = (float)ubitrackPose.co51;
	        covariance[4, 1] = (float)ubitrackPose.co52;
	        covariance[4, 2] = (float)ubitrackPose.co53;
	        covariance[4, 3] = (float)ubitrackPose.co54;
	        covariance[4, 4] = (float)ubitrackPose.co55;
	        covariance[4, 5] = (float)ubitrackPose.co56;
	
	        covariance[5, 0] = (float)ubitrackPose.co61;
	        covariance[5, 1] = (float)ubitrackPose.co62;
	        covariance[5, 2] = (float)ubitrackPose.co63;
	        covariance[5, 3] = (float)ubitrackPose.co64;
	        covariance[5, 4] = (float)ubitrackPose.co65;
	        covariance[5, 5] = (float)ubitrackPose.co66;
	    }
	
	    public static void ubitrackCovarianceToFloatArray(SimpleErrorPose ubitrackPose, ref float[,] covariance)
	    {        
	        // todo needs rhs lhs transfrom
	        covariance[0, 0] = (float)ubitrackPose.co11;
	        covariance[0, 1] = (float)ubitrackPose.co12;
	        covariance[0, 2] = (float)ubitrackPose.co13;
	        covariance[0, 3] = (float)ubitrackPose.co14;
	        covariance[0, 4] = (float)ubitrackPose.co15;
	        covariance[0, 5] = (float)ubitrackPose.co16;
	
	        covariance[1, 0] = (float)ubitrackPose.co21;
	        covariance[1, 1] = (float)ubitrackPose.co22;
	        covariance[1, 2] = (float)ubitrackPose.co23;
	        covariance[1, 3] = (float)ubitrackPose.co24;
	        covariance[1, 4] = (float)ubitrackPose.co25;
	        covariance[1, 5] = (float)ubitrackPose.co26;
	
	        covariance[2, 0] = (float)ubitrackPose.co31;
	        covariance[2, 1] = (float)ubitrackPose.co32;
	        covariance[2, 2] = (float)ubitrackPose.co33;
	        covariance[2, 3] = (float)ubitrackPose.co34;
	        covariance[2, 4] = (float)ubitrackPose.co35;
	        covariance[2, 5] = (float)ubitrackPose.co36;
	
	        covariance[3, 0] = (float)ubitrackPose.co41;
	        covariance[3, 1] = (float)ubitrackPose.co42;
	        covariance[3, 2] = (float)ubitrackPose.co43;
	        covariance[3, 3] = (float)ubitrackPose.co44;
	        covariance[3, 4] = (float)ubitrackPose.co45;
	        covariance[3, 5] = (float)ubitrackPose.co46;
	
	        covariance[4, 0] = (float)ubitrackPose.co51;
	        covariance[4, 1] = (float)ubitrackPose.co52;
	        covariance[4, 2] = (float)ubitrackPose.co53;
	        covariance[4, 3] = (float)ubitrackPose.co54;
	        covariance[4, 4] = (float)ubitrackPose.co55;
	        covariance[4, 5] = (float)ubitrackPose.co56;
	
	        covariance[5, 0] = (float)ubitrackPose.co61;
	        covariance[5, 1] = (float)ubitrackPose.co62;
	        covariance[5, 2] = (float)ubitrackPose.co63;
	        covariance[5, 3] = (float)ubitrackPose.co64;
	        covariance[5, 4] = (float)ubitrackPose.co65;
	        covariance[5, 5] = (float)ubitrackPose.co66;
	    }
	
	    public static void ubitrack3x4MatrixToFloatArray(SimpleMatrix3x4 ubiMatrix, ref float[] matrix)
	    {        
	        matrix[0] = (float)ubiMatrix.e11;
	        matrix[1] = (float)ubiMatrix.e12;
	        matrix[2] = (float)ubiMatrix.e13;
	        matrix[3] = (float)ubiMatrix.e14;
	
	        matrix[4] = (float)ubiMatrix.e21;
	        matrix[5] = (float)ubiMatrix.e22;
	        matrix[6] = (float)ubiMatrix.e23;
	        matrix[7] = (float)ubiMatrix.e24;
	
	        matrix[8] = (float)ubiMatrix.e31;
	        matrix[9] = (float)ubiMatrix.e32;
	        matrix[10] = (float)ubiMatrix.e33;
	        matrix[11] = (float)ubiMatrix.e34;
	    }
		
		public static void ubitrack4x4MatrixToMatrix(SimpleMatrix4x4 ubiMatrix, ref Matrix4x4 matrix)
	    {        
			doubleArrayClass da = doubleArrayClass.frompointer(ubiMatrix.values);
			//for(int i=0;i<16;i++){
			for(int row=0;row<4;row++)
				for(int col=0;col<4;col++){
					matrix[row,col] = (float)da.getitem(row*4+col);
				}
	    }
	
	    public static void ubitrack3x4MatrixToFloatArray(SimpleMatrix3x3 ubiMatrix, ref float[] matrix)
	    {
	        doubleArrayClass dac = doubleArrayClass.frompointer(ubiMatrix.values);        
	        for (int i = 0; i < 9; i++)
	        {
	            matrix[i] = (float)dac.getitem(i);
	        }       
	    }
	
	    public static void coordsysemChange(Vector3 input, ref Vector3 output)
	    {
	        output.x = input.x;
	        output.y = input.y;
	        output.z = -input.z;
	    }
	
	    public static void coordsysemChange(Quaternion input, ref Quaternion output)
	    {
	        output.x = -input.x;
	        output.y = -input.y;
	        output.z = input.z;
	        output.w = input.w;
	
	    }
	
		public static Quaternion QuaternionFromMatrix (Matrix4x4 m)
		{
			// Adapted from: http://www.euclideanspace.com/maths/geometry/rotations/conversions/matrixToQuaternion/index.htm
			Quaternion q = new Quaternion ();
			q.w = Mathf.Sqrt (Mathf.Max (0, 1 + m [0, 0] + m [1, 1] + m [2, 2])) / 2;
			q.x = Mathf.Sqrt (Mathf.Max (0, 1 + m [0, 0] - m [1, 1] - m [2, 2])) / 2;
			q.y = Mathf.Sqrt (Mathf.Max (0, 1 - m [0, 0] + m [1, 1] - m [2, 2])) / 2;
			q.z = Mathf.Sqrt (Mathf.Max (0, 1 - m [0, 0] - m [1, 1] + m [2, 2])) / 2;
			q.x *= Mathf.Sign (q.x * (m [2, 1] - m [1, 2]));
			q.y *= Mathf.Sign (q.y * (m [0, 2] - m [2, 0]));
			q.z *= Mathf.Sign (q.z * (m [1, 0] - m [0, 1]));
			return q;
		}
	
	    internal static Measurement<Pose> ubitrackToUnity(Ubitrack.SimplePose ubitrackPose)
	    {
	        // mirrored at z-Axis, rotation inverted
	
	        Vector3 trans = new Vector3((float)ubitrackPose.tx, (float)ubitrackPose.ty, (float)ubitrackPose.tz);
	        Quaternion rot = new Quaternion((float)ubitrackPose.rx, (float)ubitrackPose.ry, (float)ubitrackPose.rz, (float)ubitrackPose.rw);
	        Pose data = new Pose();
	
	        coordsysemChange(trans, ref data.pos);
	        coordsysemChange(rot, ref data.rot);        
	        
	        return new Measurement<Pose>(data, ubitrackPose.timestamp);
	    }
	
	    internal static Measurement<ErrorPose> ubitrackToUnity(Ubitrack.SimpleErrorPose ubitrackPose)
	    {
	        Vector3 trans = new Vector3((float)ubitrackPose.tx, (float)ubitrackPose.ty, (float)ubitrackPose.tz);
	        Quaternion rot = new Quaternion((float)ubitrackPose.rx, (float)ubitrackPose.ry, (float)ubitrackPose.rz, (float)ubitrackPose.rw);
	        ErrorPose data = new ErrorPose();
	
	        coordsysemChange(trans, ref data.pos);
	        coordsysemChange(rot, ref data.rot);
	        ubitrackCovarianceToFloatArray(ubitrackPose, ref data.covariance);
	
	        return new Measurement<ErrorPose>(data, ubitrackPose.timestamp);
	    }
	
	    internal static Measurement<float[]> ubitrackToUnity(SimpleMatrix3x4 newMatrix)
	    {
	        float[] data = new float[12];
	        ubitrack3x4MatrixToFloatArray(newMatrix, ref data);
	        return new Measurement<float[]>(data, newMatrix.timestamp);
	    }
	
	    internal static Measurement<float[]> ubitrackToUnity(SimpleMatrix3x3 newMatrix)
	    {
	        float[] data = new float[12];
	        ubitrack3x4MatrixToFloatArray(newMatrix, ref data);
	        return new Measurement<float[]>(data, newMatrix.timestamp);
	    }
	
	    internal static Measurement<List<Vector2>> ubitrackToUnity(SimplePosition2DList position2dList)
	    {
	        List<Vector2> data = new List<Vector2>();
	        foreach (SimplePosition2DValue entry in position2dList.values)
	            data.Add(new Vector2((float)entry.x, (float)entry.y));
	        return new Measurement<List<Vector2>>(data, position2dList.timestamp);
	    }
	
	    internal static Measurement<List<Vector3>> ubitrackToUnity(SimplePositionList3D position3dList)
	    {
	        List<Vector3> data = new List<Vector3>();
	        Vector3 tmp = new Vector3();
	        foreach (SimplePosition3DValue entry in position3dList.values)
	        {
	            tmp.x = (float)entry.x;
	            tmp.y = (float)entry.y;
	            tmp.z = (float)entry.z;
	            Vector3 output = new Vector3();
	            coordsysemChange(tmp, ref output);
	            data.Add(output);
	        }
	
	        return new Measurement<List<Vector3>>(data, position3dList.timestamp);
	    }
		internal static Measurement<Matrix4x4> ubitrackToUnity(SimpleMatrix4x4 matrix4x4)
	    {
	        Matrix4x4 data = new Matrix4x4();
	        ubitrack4x4MatrixToMatrix(matrix4x4, ref data);
			
	
	        return new Measurement<Matrix4x4>(data, matrix4x4.timestamp);
	    }
		
		internal static Measurement<Vector3> ubitrackToUnity(SimplePosition3D position)
	    {
	        Vector3 data = new Vector3();
			Vector3 ubiPos = new Vector3((float)position.x,(float)position.y, (float)position.z);
			
	        coordsysemChange(ubiPos, ref data);
			
	
	        return new Measurement<Vector3>(data, position.timestamp);
	    }
	}

}
