using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class s_VisualEdge : MonoBehaviour {
    
    
    public s_InstantEdgeData InstantEdgeData;
    
    s_Settings Einstellungen;

    
    
    
    // Use this for initialization
    void Start () {
        
        Einstellungen = GameObject.Find("Settings").GetComponent<s_Settings>();
        
   
    }
    
    // Update is called once per frame
    void Update () {
        
        
    }

    
    
    
}
