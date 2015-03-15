using UnityEngine;
using System.Collections;

namespace FAR{

	public class TransformOnGUI : MonoBehaviour {
	
		    public void OnGUI()
	    {
	        GUI.Label(new Rect(0, 0, 200, 50), transform.position.ToString("0.000") + " : " + transform.rotation.ToString());
	    }
	}

}
