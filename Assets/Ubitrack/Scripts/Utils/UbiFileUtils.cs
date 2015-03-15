using UnityEngine;
using System.Collections;
using System.Globalization;
using FAR;

namespace FAR{

	public class UbiFileUtils  {
	
	    public static Pose readErrorPoseAsPoseFromString(string input)
	    {
	        char[] splitter = { ' ' };
	
	        NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
	        nfi.NumberDecimalSeparator = ".";
	
	
	
	        string[] strList = input.Split(splitter);
	        Pose result = new Pose();
	
	        Vector3 pos = new Vector3(float.Parse(strList[0 + 16], nfi), float.Parse(strList[1 + 16], nfi), float.Parse(strList[2 + 16], nfi));
	        Quaternion rot = new Quaternion(float.Parse(strList[0 + 10], nfi), float.Parse(strList[1 + 10], nfi), float.Parse(strList[2 + 10], nfi), float.Parse(strList[3 + 10], nfi));
	        UbiMeasurementUtils.coordsysemChange(pos, ref result.pos);
	        UbiMeasurementUtils.coordsysemChange(rot, ref result.rot);
	        return result;
	    }
	
		public static float[] read3x3MatrixFromFile (string input)
		{
			char[] splitter = { ' ' };
	
			NumberFormatInfo nfi = new CultureInfo ("en-US", false).NumberFormat;
			nfi.NumberDecimalSeparator = ".";
	
	
	
			string[] strList = input.Split (splitter);
			float[] a3x3 = new float[9];
	
			for (int i = 0; i < 9; i++)
				a3x3 [i] = float.Parse (strList [i + 8], nfi);
	
			return a3x3;
		}
	
		public static float[] read3x4MatrixFromFile (string input)
		{
			char[] splitter = { ' ' };
	
			NumberFormatInfo nfi = new CultureInfo ("en-US", false).NumberFormat;
			nfi.NumberDecimalSeparator = ".";
	
			string[] strList = input.Split (splitter);
			float[] a3x4 = new float[12];
	
			for (int i = 0; i < 12; i++)
				a3x4 [i] = float.Parse (strList [i + 8], nfi);
	
			return a3x4;
		}
	}

}
