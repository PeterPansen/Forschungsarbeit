using UnityEngine;
using System.Collections;

public class s_InstantNodeData : s_NodeData {

	public string ID;

	/// <summary>
	/// Erstellt eine InstantiatedNode mit den Daten einer NormalNode </summary>
	/// <param name="NormalNodeData">Normal node data.</param>
	public s_InstantNodeData(s_NodeData NormalNodeData)
	{
		this.NodeName = NormalNodeData.NodeName;
		this.NodeDisplayName = NormalNodeData.NodeDisplayName;
		this.NodeDescription = NormalNodeData.NodeDescription;
		this.PatternName = NormalNodeData.PatternName;
		this.InputOutput = NormalNodeData.InputOutput;
		this.AttributeUndWerte = NormalNodeData.AttributeUndWerte;
		this.IncomingEdges = NormalNodeData.IncomingEdges;
		this.OutGoingEdges = NormalNodeData.OutGoingEdges;

	}

	public s_InstantNodeData()
	{

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


	
	}
}
