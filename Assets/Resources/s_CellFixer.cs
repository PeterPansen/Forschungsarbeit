using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class s_CellFixer : MonoBehaviour {

    RectTransform Me;

	// Use this for initialization
	void Start () {
    
        Me = this.GetComponent<RectTransform>();
        Vector3 OldPos = Me.transform.position;
        //Me.transform.position = new Vector3(OldPos.x,OldPos.y,400);
        Me.transform.localPosition = new Vector3(OldPos.x,OldPos.y,0);;
        Me.transform.localScale = new Vector3(1,1f,1f);
        
        
        
       
	
	}
	
	// Update is called once per frame
	void Update () {
    

	}
}
