using UnityEngine;
using System.Collections;

public class CallableClass : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    
	}
    
    
    public void Hoot()
    {
        //Debug.Log("HOOT-Method from Callable Class reporting in");
    }
    public void Tester()
    {
        //Debug.Log("Tester-Method from Callable Class reporting in");
    }
    public void HootHoot()
    {
        //Debug.Log("HootHoot-Method from Callable Class reporting in");
    }
    public void Regenbogen()
    {
        //Debug.Log("Regenbogen-Method from Callable Class reporting in");
    }
    
    private void PrivateMethode()
    {
        //Debug.Log("Private Methode aufgerufen");
    }
}
