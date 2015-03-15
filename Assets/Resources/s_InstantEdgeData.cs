using UnityEngine;
using System.Collections;

public class s_InstantEdgeData : s_EdgeData {

	//Beschreibt für InputEdges welche Edge sie überlagert
	public string EdgeRef;
	//Beschreibt für alle InputEdges zu welchen Patterns sie gehören
	public string PatternRef;
	public string PatternID;
	//ACHTUNG! OutPut-Edges haben keine EDGEREF und keine PATTERNREF in dfgs.


	/// <summary>
	/// Erstellt eine Instantiierte EdgeData für eine ausgewählte Pattern </summary>
	/// <param name="NormalEdgeData">Normal edge data.</param>
	public s_InstantEdgeData(s_EdgeData NormalEdgeData)
	{
		this.EdgeName = NormalEdgeData.EdgeName;
		this.EdgeDisplayName = NormalEdgeData.EdgeDisplayName;
		this.EdgeDescription = NormalEdgeData.EdgeDescription;
		this.source = NormalEdgeData.source;
		this.destination = NormalEdgeData.destination;
		this.PatternName = NormalEdgeData.PatternName;
		this.InputOutput = NormalEdgeData.InputOutput;
		this.AttributeUndWerte = NormalEdgeData.AttributeUndWerte;
	}
	
	public s_InstantEdgeData(){}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
