using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace FAR{

    [System.Serializable]
    [CustomEditor(typeof(InteractionMethod),true)]
    public class IntActCustom : Editor {
    
    	public GameObject Twinkie;
    	
    	private Dictionary<string,Action> MyActions;
    
    	public override void OnInspectorGUI()
    	{
    		
    		if(MyActions==null)
    		{
    			MyActions = new Dictionary<string, Action>();
    		}
    
    		InteractionMethod myInteractionMethod = (InteractionMethod) target;
    		
            
    		Twinkie = EditorGUILayout.ObjectField("Add Object with Actions",Twinkie,typeof(GameObject),true) as GameObject;
    		
    		EditorGUILayout.LabelField("Active Listeners:");
    
    		
    		if(myInteractionMethod.Listeners!=null)
    		{
    			for(int i = 0; i < myInteractionMethod.Listeners.Count; i++)
    			{
    				string Text = myInteractionMethod.Listeners[i].name+"=> ";
    				
    				MonoBehaviour[] Skripts = myInteractionMethod.Listeners[i].GetComponents<MonoBehaviour>();
    				
    				foreach(MonoBehaviour MB in Skripts)
    				{
    					if(MB is Action)
    					{
    						Text += (MB.ToString().Replace(myInteractionMethod.Listeners[i].name,""));
    					}
    				}
    				
    				
    				EditorGUILayout.BeginHorizontal();
    				EditorGUILayout.TextField(Text);
    				if(GUILayout.Button ("Delete"))
    				{
    					DeleteElement(Text,myInteractionMethod);
    				}
    				EditorGUILayout.EndHorizontal();
    			}
    		}
    
    		if(Twinkie != null)
    		{
    			MonoBehaviour[] Skripts = Twinkie.GetComponents<MonoBehaviour>();
    			Debug.Log ("I found "+Skripts.Length+" Skripts");
    			
    			myInteractionMethod.Listeners.Add (Twinkie);
    			
    			foreach(MonoBehaviour MB in Skripts)
    			{
    				if(MB is Action)
    				{
    					string KeyName = MB.ToString();
    					
    					while(MyActions.ContainsKey(KeyName))
    					{
    						KeyName+="_New";
    					}
    					MyActions.Add (KeyName,(Action) MB);
    
    				}
    			}
    			Twinkie = null;
    		}
    
    	EditorGUILayout.Space();
    	EditorGUILayout.TextArea("--------------------------");
    	EditorGUILayout.Space();
    	DrawDefaultInspector();
    
    	}
    	
    	public void DeleteElement(string txt,InteractionMethod target)
    	{
    		Debug.Log (txt);
    		string[] seps = {"=>"};
    		
    		string[] OrigName = txt.Split(seps,System.StringSplitOptions.None);
    		
    		List<GameObject> ToDelete = new List<GameObject>();
    		
    		foreach(GameObject GO in target.Listeners)
    		{
    			if(GO.name == OrigName[0])
    			{
    				ToDelete.Add (GO);
    			} 
    		}
    		
    		foreach(GameObject GO in ToDelete)
    		{
    			target.Listeners.Remove(GO);
    		}
    		
    	}
    
    
    }

}
