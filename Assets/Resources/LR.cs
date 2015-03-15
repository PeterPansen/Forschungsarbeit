

using UnityEngine;
using System.Collections;
[RequireComponent (typeof (LineRenderer))]

public class LR : MonoBehaviour {


    public GameObject Start;
    public GameObject Goal;



	// Update is called once per frame
	void Update () {

        if (Start != null && Goal != null)
        {
            this.GetComponent<LineRenderer>().SetPosition(0, Start.transform.position);
            this.GetComponent<LineRenderer>().SetPosition(1, Goal.transform.position);
        }
	
	}
}
