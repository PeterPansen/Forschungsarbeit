using UnityEngine;
using System;
using System.Collections.Generic;

namespace FAR{

	public class PTAMTrailDrawer : MonoBehaviour {
		
		public string TrailStartName;
		public string TrailEndName;
		
		public int CameraWidth;
		public int CameraHeight;
		
		public GameObject ListObject;
		public GameObject LineObject;
		
		protected Position2DListSource m_TrailsStart;
	    protected Position2DListSource m_TrailsEnd;
		
		protected List<LineRenderer> m_RendererObjects;
		
		
		// Use this for initialization
		void Start () {
			
			GameObject obj;
			
			if (ListObject == null)
				obj = gameObject;
			else
				obj = ListObject;
			
			if (CameraWidth <= 0)
				CameraWidth = 640;
			
			if (CameraHeight <= 0)
				CameraHeight = 480;
			
			if (LineObject == null)
				throw new ArgumentNullException("LineObject");
			
			if (TrailStartName == "")
				throw new ArgumentException("TrailStartName is empty");
			
			if (TrailEndName == "")
				throw new ArgumentException("TrailEndName is empty");
	
	
	        Component[] components = obj.GetComponents(typeof(Position2DListSource));
			
			if (components != null && components.Length > 0)
				foreach (Component comp in components)
				{
	                Position2DListSource script = (Position2DListSource)comp;
					
					if (script.patternID == TrailStartName)
					{
						if (m_TrailsStart == null)
							m_TrailsStart = script;
						else
							Debug.LogWarning("Multiple TrailStart scripts detected");
					}
	
	                if (script.patternID == TrailEndName)
					{
						if (m_TrailsEnd == null)
							m_TrailsEnd = script;
						else
							Debug.LogWarning("Multiple TrailEnd scripts detected");
					}
				}	
			
			if (m_TrailsStart == null)
				throw new MissingMemberException("No script with name \"" + TrailStartName + "\" found");
			if (m_TrailsEnd == null)
				throw new MissingMemberException("No script with name \"" + TrailEndName + "\" found");
			
			m_RendererObjects = new List<LineRenderer>();
		}
		
		void LateUpdate () {
			
			if (m_TrailsStart == null || m_TrailsEnd == null)
			{
				//Debug.LogWarning("Either Start or End sink is Null");
				return;
			}
	
	        float factorwidth = Screen.width / CameraWidth;
	        float factorheight = Screen.height / CameraHeight;
	
	        Measurement<List<Vector2>> liststart = m_TrailsStart.getList();
			Measurement<List<Vector2>> listend = m_TrailsEnd.getList();
	
	        if (liststart == null || listend == null)
	        {            
	            return;
	        }
	        if (liststart.time() != listend.time())
	        {
	            Debug.Log("mismatch:" + liststart.time() + " to " + listend.time());
	            return;
	        }
			
			IEnumerator<Vector2> enumstart = liststart.data().GetEnumerator();
			IEnumerator<Vector2> enumend = listend.data().GetEnumerator();
			
			LineRenderer renderer;
			Vector3 position;
	
			int c = 0;
			
		
			while (enumstart.MoveNext() && enumend.MoveNext())
			{
				
				if (c < m_RendererObjects.Count)
					renderer = m_RendererObjects[c];
				else
				{
					GameObject obj = (GameObject)Instantiate(LineObject);
					renderer = (LineRenderer)obj.GetComponent<LineRenderer>();
					m_RendererObjects.Add(renderer);
				}
				
				/* just to be on the safe side... as there were some spurious null reference exceptions -.- */
				if (enumstart == null || enumend == null || Camera.main == null){
					Debug.Log("suff --------------------");
					continue;
				}
	            float zPosition = Camera.main.nearClipPlane;
	            position = new Vector3(enumstart.Current.x * factorwidth, (CameraHeight - enumstart.Current.y) * factorheight, zPosition);
				renderer.SetPosition (0, Camera.main.ScreenToWorldPoint(position));
	
	            position = new Vector3(enumend.Current.x * factorwidth, (CameraHeight - enumend.Current.y) * factorheight, zPosition);
				renderer.SetPosition (1, Camera.main.ScreenToWorldPoint(position));
							
				c++;
				
			}
			
			//Debug.Log("Number of trails:"+c);
			
			if (c < m_RendererObjects.Count)
				for (int i = m_RendererObjects.Count - 1; i >= c; i--)
				{
					Destroy(m_RendererObjects[i].gameObject);
					m_RendererObjects.RemoveAt(i);
				}
	
		}
		
		
		
	}

}
