using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class s_NodeData  {

	public string NodeName;
	public string NodeDisplayName;
	public string NodeDescription;
	public string PatternName;
	public string InputOutput;
	public Dictionary<string,string> AttributeUndWerte;
	public List <s_EdgeData> IncomingEdges = new List<s_EdgeData>();
	public List <s_EdgeData> OutGoingEdges = new List<s_EdgeData>();

	public s_NodeData(){}

	public s_NodeData(string N_Name,string N_DisplayName)
	{
		this.NodeName = N_Name;
		this.NodeDisplayName = N_DisplayName;
	}
	public s_NodeData(string N_Name,string N_DisplayName,string N_Description)
	{
		this.NodeName = N_Name;
		this.NodeDisplayName = N_DisplayName;
		this.NodeDescription = N_Description;
	}
	public s_NodeData(string N_Name,string N_DisplayName,string N_Description,string N_PatternName)
	{
		this.NodeName = N_Name;
		this.NodeDisplayName = N_DisplayName;
		this.NodeDescription = N_Description;
		this.PatternName = N_PatternName;
	}

}
