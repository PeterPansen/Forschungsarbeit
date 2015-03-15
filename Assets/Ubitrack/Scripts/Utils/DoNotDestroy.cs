using UnityEngine;
using System.Collections;

namespace FAR{

	public class DoNotDestroy : MonoBehaviour {
		
		void Awake(){
			DontDestroyOnLoad(this);
		}
	}

}
