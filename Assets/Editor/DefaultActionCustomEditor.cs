using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System.Reflection;
using UnityEditor;


namespace FAR{

    [System.Serializable]
    [CustomEditor(typeof(DefaultAction),true)]
    public class DefaultActionCustomEditor : Editor {

    
        public override void OnInspectorGUI()
        {

        
            DefaultAction myDefaultAction = (DefaultAction) target;

            //Startet Größenvergleiche zwischen den verschiedenen Listen um sie ggf. anzugleichen.
            FixOutOfRangeErrors(myDefaultAction);
            
            //The Twinkie-Variable. Throw in a gameobject, get the scripts and methods attached to it
            myDefaultAction.Placeholder = EditorGUILayout.ObjectField("Add Object with Callscripts",myDefaultAction.Placeholder,typeof(GameObject),true) as GameObject;
            
            if(myDefaultAction.Placeholder!=null)
            {
                AddCaller(myDefaultAction);
            }
            
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space();
            
            
            for(int i = 0; i < myDefaultAction.Callers.Count; i++ )
            {
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                
                if(GUILayout.Button("Remove"))
                {
                    myDefaultAction.Callers.RemoveAt(i);
                    myDefaultAction.SelectedScriptNumber.RemoveAt(i);
                    myDefaultAction.SelectedMethodNumber.RemoveAt(i);
                    myDefaultAction.MethodsToCall.RemoveAt(i);
                }

                
                    
                    //Fixes possible nullerrors due to deletion of elements
                    if(i > myDefaultAction.Callers.Count-1)
                    {
                        continue;
                    }
                
                    MonoBehaviour[] AttachedScripts = myDefaultAction.Callers[i].GetComponents<MonoBehaviour>();
                    
                    string[] EditorAttachedScripts = new string[AttachedScripts.Length];
                    
                    for( int x = 0; x < EditorAttachedScripts.Length; x++ )
                    {
                        
                        EditorAttachedScripts[x] = AttachedScripts[x].ToString();
                        
                        
                    }
    
                    //Skriptenauswahl
                    myDefaultAction.SelectedScriptNumber[i] = EditorGUILayout.Popup("", myDefaultAction.SelectedScriptNumber[i], EditorAttachedScripts); 
                    
                    //Holen wir uns die Methoden aus dem Skript dessen Nummer wir ausgesucht haben
                    string[] FoundMethods = HelperFunc.GetMethodsInScript(AttachedScripts[myDefaultAction.SelectedScriptNumber[i]]);
                    
                    
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.BeginVertical();
                    EditorGUILayout.LabelField("Choose method");
                    
                    
                    //Methodenauswahl
                    myDefaultAction.SelectedMethodNumber[i] = EditorGUILayout.Popup("",myDefaultAction.SelectedMethodNumber[i],FoundMethods);
                    
                    if(FoundMethods.Length > 0)
                    {
                        myDefaultAction.MethodsToCall[i] = FoundMethods[myDefaultAction.SelectedMethodNumber[i]];
                    }
                    
                    EditorGUILayout.EndVertical();

            }
            
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();

       
        }
        

        
        public void FixOutOfRangeErrors(DefaultAction myDA)
        {
            while(myDA.Callers.Count > myDA.SelectedMethodNumber.Count || myDA.Callers.Count > myDA.SelectedScriptNumber.Count)
            {
                if(myDA.Callers.Count > myDA.SelectedMethodNumber.Count)
                {
                    myDA.SelectedMethodNumber.Add(0);
                }
                if(myDA.Callers.Count > myDA.SelectedScriptNumber.Count)
                {
                    myDA.SelectedScriptNumber.Add(0);
                }
                if(myDA.Callers.Count > myDA.MethodsToCall.Count)
                {
                    myDA.MethodsToCall.Add("");
                }
            }
            
            while(myDA.Callers.Count < myDA.SelectedMethodNumber.Count || myDA.Callers.Count < myDA.SelectedScriptNumber.Count)
            {
                if(myDA.Callers.Count < myDA.SelectedMethodNumber.Count)
                {
                    myDA.SelectedMethodNumber.RemoveAt(myDA.SelectedMethodNumber.Count-1);
                }
                if(myDA.Callers.Count < myDA.SelectedScriptNumber.Count)
                {
                    myDA.SelectedScriptNumber.RemoveAt(myDA.SelectedScriptNumber.Count-1);
                }
                if(myDA.Callers.Count < myDA.MethodsToCall.Count)
                {
                    myDA.MethodsToCall.RemoveAt(myDA.MethodsToCall.Count-1);
                }
            }
        }
        
        /// <summary>
        /// Gets called when the twinkie-field is unempty.
        /// We add the Object in the field to the callers-list and make room
        /// for its choice of Script and Method.
        /// </summary>
        /// <param name="myDef">My def.</param>
        public void AddCaller(DefaultAction myDef)
        {
            myDef.SelectedScriptNumber.Add(0);
            myDef.SelectedMethodNumber.Add(0);
            myDef.MethodsToCall.Add("");
            myDef.Callers.Add(myDef.Placeholder);
            myDef.Placeholder = null;
        }
        
    }

}
