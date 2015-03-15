using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ARWindow : MonoBehaviour {
	public LineRenderer lineRenderer;
	public LineRenderer debugLine;
	
	public Camera cam;
	
	public float left = -0.2F;
    public float right = 0.2F;
    public float top = 0.2F;
    public float bottom = -0.2F;
	
	private List<Vector3> tmpArray = new List<Vector3>();
	private List<Vector3> tmpArray2 = new List<Vector3>();
	
	public List<Vector3> displayCorners = new List<Vector3>();
	
	
	
	// Use this for initialization
	void Start () {
		
		float offset = 10;
		tmpArray.Add(new Vector3(offset,offset,0));
		tmpArray.Add(new Vector3(Screen.width-offset,offset,0));
		tmpArray.Add(new Vector3(Screen.width-offset,Screen.height-offset,0));
		tmpArray.Add(new Vector3(0,Screen.height-offset,0));
		
		/*
		tmpArray.Add(new Vector3(0,0,0));
		tmpArray.Add(new Vector3(Screen.width,0,0));
		tmpArray.Add(new Vector3(Screen.width,Screen.height,0));
		tmpArray.Add(new Vector3(0,Screen.height,0));
		*/
		for(int i=0;i<4;i++)
			lineRenderer.SetPosition(i, displayCorners[i]);
		lineRenderer.SetPosition(4, displayCorners[0]);
		
		
	}	
	
    void Update() {
		
        //Camera cam = camera;
		/*
		Vector3 pa = displayCorners[3];
		Vector3 pb = displayCorners[0];
		Vector3 pc = displayCorners[2];
		*/
		
		Vector3 pa = displayCorners[0];
		Vector3 pb = displayCorners[3];
		Vector3 pc = displayCorners[1];
		
		
		Vector3 pe = transform.position;
		
		//if(pe.magnitude < 0.05f)
		//	return;
		
		//transform.position = Vector3.zero;
		pe.z = -pe.z;
		
		Vector3 va = pa - pe;
		Vector3 vb = pb - pe;
		Vector3 vc = pc - pe;
		Vector3 vr = (pb - pa).normalized;
		Vector3 vu = (pc - pa).normalized;
		Vector3 vn = Vector3.Cross(vr, vu).normalized;
		
		
		float distance = -Vector3.Dot(vn, va);
		float n = 0.01f;
		float f = 40f;
		float left = Vector3.Dot(vr,va)*n/distance;
		float right = Vector3.Dot(vr,vb)*n/distance;
		float bottom = Vector3.Dot(vu,va)*n/distance;
		float top = Vector3.Dot(vu,vc)*n/distance;
		
		Debug.Log("distance"+distance);
		Debug.Log("left"+left);
		
		
        Matrix4x4 m = PerspectiveOffCenter(left, right, bottom, top, n, f);
		
		Matrix4x4 rotMat = new Matrix4x4();
		rotMat[0,0] = vr[0]; rotMat[0,1] = vu[0]; rotMat[0,2] = vn[0]; 
		rotMat[1,0] = vr[1]; rotMat[1,1] = vu[1]; rotMat[1,2] = vn[1]; 
		rotMat[2,0] = vr[2]; rotMat[2,1] = vu[2]; rotMat[2,2] = vn[2]; 
		rotMat[3,3]=1f;
		
		Matrix4x4 transMat = new Matrix4x4();
		transMat[0,0] = 1.0f;
		transMat[1,1] = 1.0f;
		transMat[2,2] = 1.0f;
		transMat[0,3] = -pe[0];
		transMat[1,3] = -pe[1];
		transMat[2,3] = -pe[2];
		transMat[3,3] = 1.0f;
		
		Debug.Log("rotMat:"+rotMat);
		Debug.Log("M:"+m);
		rotMat =  Matrix4x4.Transpose(rotMat);		
        cam.projectionMatrix = m  * rotMat * transMat;		
		//cam.projectionMatrix = transMat * rotMat * m;
		
		Debug.Log(cam.projectionMatrix);
		
		//Vector3 scale =new Vector3(5,5,5);
		
		for(int i=0;i<tmpArray.Count;i++){
			//tmpArray2.Add(camera.ScreenPointToRay(tmpArray[i]).direction.normalized);
			//tmpArray2.Add(camera.projectionMatrix.MultiplyVector(tmpArray[i]).normalized);
			tmpArray2.Add(cam.ScreenPointToRay(tmpArray[i]).direction.normalized);
			//tmpArray2[i].Scale(scale);
			
		}
		
		
		debugLine.SetPosition(0, Vector3.zero);//transform.localPosition);
		debugLine.SetPosition(1, tmpArray2[0]);
		debugLine.SetPosition(2, tmpArray2[1]);
		debugLine.SetPosition(3, Vector3.zero);//transform.localPosition);
		debugLine.SetPosition(4, tmpArray2[2]);
		debugLine.SetPosition(5, tmpArray2[3]);
		debugLine.SetPosition(6, Vector3.zero);//transform.localPosition);
		//camera.projectionMatrix
		
    }
	
	private void updateWidthLineRenderer(){
		
	}
	
    static Matrix4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far) {
        float x = 2.0F * near / (right - left);
        float y = 2.0F * near / (top - bottom);
        float a = (right + left) / (right - left);
        float b = (top + bottom) / (top - bottom);
        float c = -(far + near) / (far - near);
        float d = -(2.0F * far * near) / (far - near);		
        float e = -1.0F;
        Matrix4x4 m = new Matrix4x4();
        m[0, 0] = x;
        m[0, 1] = 0;
        m[0, 2] = a;
        m[0, 3] = 0;
        m[1, 0] = 0;
        m[1, 1] = y;
        m[1, 2] = b;
        m[1, 3] = 0;
        m[2, 0] = 0;
        m[2, 1] = 0;
        m[2, 2] = c;
        m[2, 3] = d;
        m[3, 0] = 0;
        m[3, 1] = 0;
        m[3, 2] = e;
        m[3, 3] = 0;
        return m;
    }
}


