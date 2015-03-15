using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class s_VisualPatternData : MonoBehaviour {

	public s_InstantPatternData InstantPatternData;
    //REM
    public List<string> DEBUG_InputNodes = new List<string>();
    public List<string> DEBUG_OutputNodes = new List<string>();
    ///REM

	
	//Beinhaltet alle Patterns mit denen diese verbunden wird.
	public List<GameObject> RelatedPatterns = new List<GameObject>();

	// Use this for initialization
	void Start () {
    
        if(InstantPatternData!=null)
        {
            foreach(s_InstantNodeData IND in InstantPatternData.inputNodes)
            {
                DEBUG_InputNodes.Add(IND.NodeName);
            }
            foreach(s_InstantNodeData IND in InstantPatternData.outputNodes)
            {
                DEBUG_OutputNodes.Add(IND.NodeName);
            }
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
