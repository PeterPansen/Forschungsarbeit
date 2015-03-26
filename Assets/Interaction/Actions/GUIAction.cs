using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace FAR{

    public class GUIAction : MonoBehaviour , Action {
    
        GameObject RotationCenter;
    
    	// Use this for initialization
    	void Start () {
        
            RotationCenter = new GameObject("RotationCenter");
            RotationCenter.transform.position = Camera.main.transform.position;
            RotationCenter.transform.parent = Camera.main.transform;
            this.gameObject.transform.parent = RotationCenter.transform;
    	
    	}
        
    	
        public void doEvent(InteractionEvent evt)
        {

            if(evt.GetType() == typeof(DragEvent))
            {
                DragEvent devt = (DragEvent) evt;
                if(devt.Obj == this.gameObject)
                {

                    RotationCenter.transform.LookAt(devt.newPos.pos);
                }
                

            }
            
        
        }
        
        void CalculateNewGUIPos()
        {
            Ray MousePosRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance = (MousePosRay.origin.z - this.gameObject.transform.position.z);
            Vector3 NewPosition = MousePosRay.GetPoint(distance);
            this.gameObject.transform.position = new Vector3(-NewPosition.x,-NewPosition.y,this.gameObject.transform.position.z);
        }
    }
}