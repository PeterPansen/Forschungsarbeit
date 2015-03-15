using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class s_PatternData {

	public string PatternName;
	public string PatternDisplayName;
	public string PatternDescription;

	public List<s_NodeData> inputNodes = new List<s_NodeData>();
	public List<s_NodeData> outputNodes = new List<s_NodeData>();

	public List<s_EdgeData> inputEdges = new List<s_EdgeData>();
	public List<s_EdgeData> outputEdges = new List<s_EdgeData>();

	public s_PatternData(string P_Name,string P_DisplayName,string P_Description)
	{
		this.PatternName = P_Name;
		this.PatternDisplayName = P_DisplayName;
		this.PatternDescription = P_Description;
	}
	public s_PatternData(){}
	

}
