using UnityEngine;
using System.Collections;


namespace FAR{

	public class QuitOnButton : MonoBehaviour {
	
		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
			if(Input.GetKeyDown("q")){
				System.Diagnostics.Process.GetCurrentProcess().Kill();
			}
		}
	}

}
