using UnityEngine;
using System.Collections;

namespace FAR{

	class CameraUtils
	{
	    public static Matrix4x4 constructProjectionMatrix3x4(float[] projection, float width, float height, float near, float far)
	    {
	
	        float r = width;
	        float l = 0.0f;
	        float t = height;
	        float b = 0.0f;
	
	        Matrix4x4 m = new Matrix4x4();
	
	        m.m00 = projection[0];
	        m.m01 = projection[1];
	        m.m02 = projection[2];
	        m.m03 = projection[3];
	
	        m.m10 = projection[4];
	        m.m11 = projection[5];
	        m.m12 = projection[6];
	        m.m13 = projection[7];
	
	        m.m20 = projection[8];
	        m.m21 = projection[9];
	        m.m22 = projection[10];
	        m.m23 = projection[11];
	
	        Vector4 row2 = m.GetRow(2);
	        m.SetRow(3, row2);
	
	
	        float norm = Mathf.Sqrt(m.m20 * m.m20 + m.m21 * m.m21 + m.m22 * m.m22);
	
	        float add = far * near * norm;
	
	
	
	
	        row2 = row2 * (-far - near);
	        m.SetRow(2, row2);
	        m.m23 += add;
	
	
	
	
	        Matrix4x4 ortho = new Matrix4x4();
	        ortho.m00 = 2.0f / (r - l);
	        ortho.m01 = 0.0f;
	        ortho.m02 = 0.0f;
	        ortho.m03 = (r + l) / (l - r);
	        ortho.m10 = 0.0f;
	        ortho.m11 = 2.0f / (t - b);
	        ortho.m12 = 0.0f;
	        ortho.m13 = (t + b) / (b - t);
	        ortho.m20 = 0.0f;
	        ortho.m21 = 0.0f;
	        ortho.m22 = 2.0f / (near - far);
	        ortho.m23 = (far + near) / (near - far);
	        ortho.m30 = 0.0f;
	        ortho.m31 = 0.0f;
	        ortho.m32 = 0.0f;
	        ortho.m33 = 1.0f;
	
	        Matrix4x4 res = new Matrix4x4();
	        res = ortho * m;
	
	        return res;
	    }
	
	    public static Matrix4x4 constructProjectionMatrix3x3(float[] i3x3, float width, float height, float near, float far)
	    {
	        Debug.Log("construct projection matrix: m=" + i3x3[0] + " " + i3x3[1] + " " + i3x3[2] + " " + i3x3[3] + "; w=" + width + "; h=" + height + "; n=" + near + "; f=" + far);
	
	        float r = width;
	        float l = 0.0f;
	        float t = height;
	        float b = 0.0f;
	
	
	        float norm = Mathf.Sqrt(i3x3[6] * i3x3[6] + i3x3[7] * i3x3[7] + i3x3[8] * i3x3[8]);
	
	        float add = far * near * norm;
	
	        Matrix4x4 m = new Matrix4x4();
	        m.m00 = i3x3[0];
	        m.m01 = i3x3[1];
	        m.m02 = i3x3[2];
	        m.m10 = i3x3[3];
	        m.m11 = i3x3[4];
	        m.m12 = i3x3[5];
	        m.m20 = i3x3[6] * (-far - near);
	        m.m21 = i3x3[7] * (-far - near);
	        m.m22 = i3x3[8] * (-far - near);
	        m.m23 = add;
	        m.m30 = i3x3[6];
	        m.m31 = i3x3[7];
	        m.m32 = i3x3[8];
	
	        Matrix4x4 ortho = new Matrix4x4();
	        ortho.m00 = 2.0f / (r - l);
	        ortho.m01 = 0.0f;
	        ortho.m02 = 0.0f;
	        ortho.m03 = (r + l) / (l - r);
	        ortho.m10 = 0.0f;
	        ortho.m11 = 2.0f / (t - b);
	        ortho.m12 = 0.0f;
	        ortho.m13 = (t + b) / (b - t);
	        ortho.m20 = 0.0f;
	        ortho.m21 = 0.0f;
	        ortho.m22 = 2.0f / (near - far);
	        ortho.m23 = (far + near) / (near - far);
	        ortho.m30 = 0.0f;
	        ortho.m31 = 0.0f;
	        ortho.m32 = 0.0f;
	        ortho.m33 = 1.0f;
	
	        Matrix4x4 res = new Matrix4x4();
	        res = ortho * m;        
	        res.m23 = -(2 * far * near) / (far - near);
	        return res;
	    }
		
		 public static Matrix4x4 constructProjectionMatrix4x4(Matrix4x4 m, float width, float height, float near, float far)
	    {
	
	        float r = 1.0f;//width;
	        float l = 0.0f;
	        float t = 1.0f;//height;
	        float b = 0.0f;
	
			/*
	        Matrix4x4 m = new Matrix4x4();
	
	        m.m00 = projection[0];
	        m.m01 = projection[1];
	        m.m02 = projection[2];
	        m.m03 = projection[3];
	
	        m.m10 = projection[4];
	        m.m11 = projection[5];
	        m.m12 = projection[6];
	        m.m13 = projection[7];
	
	        m.m20 = projection[8];
	        m.m21 = projection[9];
	        m.m22 = projection[10];
	        m.m23 = projection[11];
	
	        Vector4 row2 = m.GetRow(2);
	        m.SetRow(3, row2);
			 */
			
			/*
			Vector4 row2 = m.GetRow(2);
	        float norm = Mathf.Sqrt(m.m20 * m.m20 + m.m21 * m.m21 + m.m22 * m.m22);
	
	        float add = far * near * norm;
	
	
	
	
	        row2 = row2 * (-far - near);
	        m.SetRow(2, row2);
	        m.m23 += add;
	
			 */
	
	
	        Matrix4x4 ortho = new Matrix4x4();
	        ortho.m00 = 2.0f / (r - l);
	        ortho.m01 = 0.0f;
	        ortho.m02 = 0.0f;
	        ortho.m03 = (r + l) / (l - r);
	        ortho.m10 = 0.0f;
	        ortho.m11 = 2.0f / (t - b);
	        ortho.m12 = 0.0f;
	        ortho.m13 = (t + b) / (b - t);
	        ortho.m20 = 0.0f;
	        ortho.m21 = 0.0f;
	        ortho.m22 = 2.0f / (near - far);
	        ortho.m23 = (far + near) / (near - far);
	        ortho.m30 = 0.0f;
	        ortho.m31 = 0.0f;
	        ortho.m32 = 0.0f;
	        ortho.m33 = 1.0f;
	
	        Matrix4x4 res = new Matrix4x4();
	        res = ortho * m;
	
	        return res;
	    }
	}

}
