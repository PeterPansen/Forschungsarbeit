using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class s_EdgeData {

	public string EdgeName;
	public string EdgeDisplayName;
	public string EdgeDescription;
	public string source;
	public string destination;
	public string PatternName;
	public string InputOutput;
	//First string = KEY zu einer Liste mit value und xsi-type
	public Dictionary<string,string> AttributeUndWerte;

	public s_EdgeData(){}

	public s_EdgeData ( string E_Name,string E_DisplayName, string E_Description, string E_source, string E_Destination, Dictionary<string,string> E_AttUWerte,string E_PatternName)
	{
		EdgeName = E_Name;
		EdgeDisplayName = E_DisplayName;
		EdgeDescription = E_Description;
		source = E_source;
		destination = E_Destination;
		AttributeUndWerte = E_AttUWerte;
		PatternName = E_PatternName;
	}
	
}
