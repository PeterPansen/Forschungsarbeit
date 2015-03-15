using UnityEngine;
using System.Collections;

namespace FAR{

    public class NodeAction : MonoBehaviour , Action {
    
        bool Blocked = false;
        bool ActivelySelected = false;


    
        public void doEvent(InteractionEvent evt)
        {
            
            if(evt.GetType() == typeof(SelectionEvent))
            {
                SelectionEvent sevt = (SelectionEvent) evt;
                if(sevt.Obj == this.gameObject)
                {
                        if(Blocked == false)
                        {
                            this.GetComponent<s_VisualNode>().SwitchMaterial("ActiveSelection");
                            this.GetComponent<s_VisualNode>().ShowDesc(true);
                            StartCoroutine("WaitToUnlock");
                            ActivelySelected = true;
                        }

                }
                else
                {
                       this.GetComponent<s_VisualNode>().SwitchMaterial("Normal");
                       this.GetComponent<s_VisualNode>().ShowDesc(false);
                       ActivelySelected = false;
                }
            }
            
            if(evt.GetType() == typeof(HoverEvent))
            {
                if(ActivelySelected == false)
                {
                    HoverEvent hevt = (HoverEvent) evt;
                    s_VisualNode sVis = this.GetComponent<s_VisualNode>();
                    
                    //ShowDescription on Hover and Highlight
                    if(hevt.Obj == this.gameObject)
                    {
            			sVis.ShowDesc(false);
            			sVis.SwitchMaterial("Selected");
                    }
                    if(hevt.Obj != this.gameObject)
                    {
            			sVis.ShowDesc(false);
            			sVis.SwitchMaterial("Normal");
                    }
                }

            }
            
            if(evt.GetType() == typeof (DragEvent))
            {
                DragEvent devt = (DragEvent) evt;

                if(devt.Obj==this.gameObject)
                {
                    SetPos(devt.newPos);
                }
            }
            
            
            if(evt.GetType() == typeof(DeleteEvent))
            {
                Debug.Log("Received DeleteEvent");
                DeleteEvent devt = (DeleteEvent) evt;
                if(devt.DeletedObj == this.gameObject)
                {
                    Debug.Log("ITS ME");
                    Destroy(this.gameObject);
                }
            }
            

            
        }
        
        IEnumerator WaitToUnlock ()
        {
            Blocked = true;
            yield return new WaitForSeconds(0.2f);
            Blocked = false;
        }
        
        public void SetPos(Pose NewPose)
        {
            this.transform.position = NewPose.pos;
            this.transform.rotation = NewPose.rot;
        }
    }

}
