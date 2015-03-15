using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class s_InstantPatternData : s_PatternData {

	public string ID;


	public List<s_InstantNodeData> inputNodes = new List<s_InstantNodeData>();
	public List<s_InstantNodeData> outputNodes = new List<s_InstantNodeData>();
	
	public List<s_InstantEdgeData> inputEdges = new List<s_InstantEdgeData>();
	public List<s_InstantEdgeData> outputEdges = new List<s_InstantEdgeData>();

	public s_InstantPatternData(){}

	public s_InstantPatternData(s_PatternData PD)
	{
		this.PatternName = PD.PatternName;
		this.PatternDisplayName = PD.PatternDisplayName;
		this.PatternDescription = PD.PatternDescription;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
