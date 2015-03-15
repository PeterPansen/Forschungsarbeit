using UnityEngine;
using System.Collections.Generic;
using System;
using FAR;

public class PTAMFeaturePoints : MonoBehaviour {

	public string ListName;
	public GameObject FeaturePoint;
	
	protected Position3DListSource m_FeatureList;
	protected List<GameObject> m_Points;

	protected ulong m_LastTimestamp = 0;
	
	// Use this for initialization
	void Start () {
		if (FeaturePoint == null)
			throw new ArgumentNullException("FeaturePoint");
		
		if (ListName == "")
			throw new ArgumentException("ListName is empty");
		
		m_Points = new List<GameObject>();

        Component[] components = gameObject.GetComponents(typeof(Position3DListSource));
		
		if (components != null && components.Length > 0)
			foreach (Component comp in components)
			{
                Position3DListSource script = (Position3DListSource)comp;
				
				if (script.patternID == ListName)
				{
					if (m_FeatureList == null)
						m_FeatureList = script;
					else
						Debug.LogWarning("Multiple scripts detected");
				}
			}	
		
		if (m_FeatureList == null)
			throw new MissingMemberException("No script with name \"" + ListName + "\" found");
	}
	
	// Update is called once per frame
	void Update () {
		if (m_FeatureList == null)		
			return;

        Measurement<List<Vector3>> mea = m_FeatureList.getList();
        if (mea == null) return;
		/* now new feature points... */
        if (mea.time() == m_LastTimestamp)
			return;

        m_LastTimestamp = mea.time();

        List<Vector3> list = mea.data();
		
		Debug.Log("Number of feature points: " + list.Count);
		
		int index = 0;
		foreach (Vector3 point in list)
		{
			Debug.Log("feature point position:"+point);
			if (index < m_Points.Count)
				m_Points[index].transform.position = point;
			else
				m_Points.Add((GameObject)Instantiate(FeaturePoint, point, Quaternion.identity));		
			index++;
		}
		
		if (index < m_Points.Count)
		{
			for (int i = index; i < m_Points.Count; i++)
				Destroy(m_Points[i]);
			m_Points.RemoveRange(index, m_Points.Count - index);
		}
	}
}
