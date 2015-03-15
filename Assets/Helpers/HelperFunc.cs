using UnityEngine;
using System.Collections;
using System.Reflection;

namespace FAR{

    public static class HelperFunc {
    
        /// <summary>
        /// Adds the object with its action to all interaction methods in the scene
        /// allowing it to react to incoming events of all types
        /// </summary>
        /// <param name="ActionHolder">Action holder.</param>
    	public static void AddActionToAllInteractionMethods(GameObject ActionHolder)
        {
            s_Settings Einstellungen = GameObject.Find("Settings").GetComponent<s_Settings>();
            InteractionMethod[] InteractionMethods = Einstellungen.InteraktionsMethodenParent.GetComponentsInChildren<InteractionMethod>();
            //Debug.Log("I found "+InteractionMethods.Length+" IntAkts");
            
            foreach(InteractionMethod IM in InteractionMethods)
            {
                IM.Listeners.Add(ActionHolder);
            }
            
            
        }
        
        /// <summary>
        /// Returns a string-array with all public methods contained in this script
        /// and all its Instances
        /// </summary>
        /// <returns>The methods in script.</returns>
        /// <param name="Script">Script.</param>
        public static string[] GetMethodsInScript(MonoBehaviour Script)
        {
            
            
            // get all public static methods of MyClass type
            MethodInfo[] methodInfos = Script.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.NonPublic);//BindingFlags.Public | BindingFlags.Static);
            
            /*Debug.Log("Public Static: --------------------");
            
            foreach(MethodInfo MI in methodInfos)
            {
                Debug.Log(MI.Name);
            }*/
            
            string[] Methods = new string[methodInfos.Length];
            
            for( int i = 0; i < methodInfos.Length; i++ )
            {
                Methods[i] = methodInfos[i].Name;
            }
            
            
            return Methods;
        }
        
        
 
    }

}
