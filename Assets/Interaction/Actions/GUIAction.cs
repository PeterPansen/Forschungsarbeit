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
                    RectTransform RT = this.GetComponent<RectTransform>();
                    //RT.localPosition = new Vector3(devt.newPos.pos.x,devt.newPos.pos.y,RT.localPosition.z);
                    //RT.position = new Vector3(devt.newPos.pos.x,devt.newPos.pos.y,RT.localPosition.z);
                    //RT.anchoredPosition = new Vector2(devt.newPos.pos.x*2,devt.newPos.pos.y*2);
                    //RT.gameObject.transform.position = new Vector3(devt.newPos.pos.x*10,devt.newPos.pos.y*10,RT.gameObject.transform.position.z);
                    
                    //CalculateNewGUIPos();

           
                    
                    RotationCenter.transform.LookAt(devt.newPos.pos);
                    //this.GetComponent<ScrollListTest>().AllocateGridElementsToCamera();
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