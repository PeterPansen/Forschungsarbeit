using UnityEngine;
using System.Collections;

namespace FAR{

    public class CameraAction : MonoBehaviour , Action {
   
        s_Settings Einstellungen;
    
        public float Camera_Rotation_Speed = 5.0f;
        
        public bool MovementBlocked = false;
        
        void Start()
        {
        	Einstellungen = GameObject.Find("Settings").GetComponent<s_Settings>();
        }
        
    
        public void doEvent(InteractionEvent evt)
        {
    
        
            if(evt.GetType() == typeof(KeyboardEvent) && MovementBlocked == false)
            {
                KeyboardEvent k_evt = (KeyboardEvent) evt;
                if(k_evt.key=='a')
                {
                    this.transform.parent.Rotate(Vector3.up,-(Einstellungen.Camera_Rotation_Speed)*Time.deltaTime);
                }
                if(k_evt.key=='d')
                {
                    this.transform.parent.Rotate(Vector3.up,+(Einstellungen.Camera_Rotation_Speed)*Time.deltaTime);
                }
                if(k_evt.key=='w')
                {
                    Vector3 OldPos = this.transform.parent.transform.position;
                    this.transform.parent.transform.position = new Vector3(OldPos.x,OldPos.y+5.0f*Time.deltaTime,OldPos.z);
                }
                if(k_evt.key=='s')
                {
                    Vector3 OldPos = this.transform.parent.transform.position;
                    this.transform.parent.transform.position = new Vector3(OldPos.x,OldPos.y-5.0f*Time.deltaTime,OldPos.z);
                }
            }
        }
    }

}
