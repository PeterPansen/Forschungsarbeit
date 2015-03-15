using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Ubitrack;
using Enums;

namespace FAR{

    public class MouseObjectSelector : InteractionMethod, Action {
    
    	MouseEvent LastMouseEvent;
    	ulong dragCounter = 0;
    	string LastKind = "";
        
        bool ChoiceRingActive = false;
        
        GameObject ChoiceRing;
        
        bool Dragging = false;
        GameObject CurrDragged;
        
    
    	public void doEvent(InteractionEvent evt)
    	{
    		if(evt.GetType() == typeof(MouseEvent))
            {
                MouseEvent mevt = (MouseEvent) evt;
                CheckInteractionType(mevt);
            }
    	}
    	
        override public void Start()
        {
            base.Start();
        }
    	
    	// Update is called once per frame
    	void Update () {
    	
    		CheckForNewListeners();
    	
    	}
    	
    	
    	/// <summary>
    	/// Checks for CursorSelection with any Collider-Objekt and returns the gameobject.
    	/// Requires CAMERA.MAIN to work
    	/// </summary>
    	/// <returns>The I click on something.</returns>
    	/// <param name="evt">Evt.</param>
    	public GameObject DidIClickOnSomething (MouseEvent evt)
    	{	
    		
    		//REM
    		Camera MainCam = Camera.main;
    		
    		Vector3 AimingAt = MainCam.ScreenToWorldPoint (new Vector3 (evt.xPos, evt.yPos, 100));
    		
    		Ray ray = new Ray (MainCam.transform.position, AimingAt - MainCam.transform.position);
    		
    		RaycastHit Hitinfo = new RaycastHit ();
    		
    		if (Physics.Raycast (ray,out Hitinfo,10000f))
    		{
    			return Hitinfo.transform.gameObject;
    		}
    		
    		else
    		{
    			return null;
    		}
    	}
    	
    	/// <summary>
    	/// Checks whether a MouseEvent drags or clicks a selected object
    	/// Doesn't return a value but triggers the adequate events (Selection,Drag)
    	/// </summary>
    	/// <param name="mevt">Mevt.</param>
    	/// <param name="Obj">Object.</param>
    	public void CheckForDrag (MouseEvent mevt,GameObject Obj)
    	{
    		
    
    	}
    	
        
        public void CheckInteractionType(MouseEvent mevt)
        {
            //HOVERING
            if(mevt.mouseButton == MouseButton.NONE)
            {
                GameObject HoverObj = DidIClickOnSomething(mevt);
                if(HoverObj != null)
                {
                    HoverEvent hevt = new HoverEvent(HoverObj,CreateTimeStamp(),this.GetComponent<InteractionMethod>(),mevt);
                    fireEvent(hevt);
                }
            }
            //ENDHOVERING
        
            if(DidIClickOnSomething(mevt)!=null)
            {
                //Zweite Möglichkeit "Wir halten gedrückt"
                if(mevt.mouseButton == MouseButton.Left && mevt.UpDown == MouseState.Down)
                {
                    dragCounter++;
                }
            } 
            //Dritte Möglichkeit "Wir lassen los"
            if(mevt.mouseButton == MouseButton.Left && mevt.UpDown == MouseState.Up)
            {
                //Lassen wir los wenn der DragCounter niedrig ist war es ein Klick...
                if(dragCounter < 16)
                {
                    GameObject SelObj = DidIClickOnSomething(mevt);
                    SelectionEvent sevt = new SelectionEvent(SelObj,CreateTimeStamp(),this.GetComponent<InteractionMethod>(),mevt);
                    fireEvent(sevt);
                }
                //...sollten wir es gedragged und dann losgelassen haben ist es vermutlich eine Combination
                else
                {
                    DragEvent devt = new DragEvent(GetDragPose(CurrDragged,mevt),CurrDragged,CreateTimeStamp(),this.GetComponent<InteractionMethod>(),mevt);
                    HandleCombination(CurrDragged,CheckForInteraction(devt,3.0f),devt);
                    Destroy(ChoiceRing);
                }
            
                dragCounter = 0;
                Dragging = false;
                CurrDragged = null;
            }
            
            
            
            if(dragCounter > 16)
            {
                
                if(Dragging == false)
                    {
                        
                        Dragging = true;
                        CurrDragged = DidIClickOnSomething(mevt);
                        ChoiceRing = Instantiate(Resources.Load("ChoiceRing",typeof(GameObject))) as GameObject;
                        ChoiceRing.transform.position = CurrDragged.transform.position;
                    }
                    
               DragEvent devt = new DragEvent(GetDragPose(CurrDragged,mevt),CurrDragged,CreateTimeStamp(),this.GetComponent<InteractionMethod>(),mevt);
               fireEvent(devt);
               
               if(ChoiceRing!=null)
                   {
                        ChoiceRing.transform.position = CurrDragged.transform.position;
                   }
            }
        }
    	
    	
    	/// <summary>
    	/// Gibt die neue Position eines Objektes durch die Verschiebung eines Cursors an
    	/// </summary>
    	/// <returns>The drag vector.</returns>
    	/// <param name="obj">Object.</param>
    	/// <param name="mevt">Mevt.</param>
    	public Pose GetDragPose(GameObject obj,MouseEvent mevt)
    	{
    		Vector3 screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    		
    		Vector3 AimingAt = Camera.main.ScreenToWorldPoint (new Vector3 (mevt.xPos, mevt.yPos,screenPoint.z));
    	
            if(AimingAt!=null)
            {
        		Pose NewPose = new Pose(AimingAt,obj.transform.rotation);
        		
        		return NewPose;
            }
            else
            {
                return null;
            }
    	}
        
        
        /// <summary>
        /// Wird ausgelöst wenn wir ein gezogenes Objekt loslassen.
        /// Wir überprüfen die Umgebung nach dem nächsten Objekt für eine Interaktion
        /// </summary>
        /// <param name="evt">Evt.</param>
        /// <param name="radius">Radius.</param>
        public GameObject CheckForInteraction(DragEvent evt, float radius)
        {
           GameObject Primary = evt.Obj;
           GameObject Selected = null;
           float Distance = 999.0f;
        
           Collider[] SurroundingColliders = Physics.OverlapSphere(evt.newPos.pos,radius);
           
            for ( int i = 0; i < SurroundingColliders.Length; i++ )
           {
                if(SurroundingColliders[i].gameObject == Primary)
                {
                    continue;
                }
                else
                {
                        float MyDistanceToObj = Vector3.Distance(SurroundingColliders[i].transform.position,Primary.transform.position);
                        if(MyDistanceToObj < Distance)
                        {
                            Distance = MyDistanceToObj;
                            Selected = SurroundingColliders[i].gameObject;
                        }
                }
           }
    
        if(Selected==null)
        {
            return null;
        }
        else
        {
            return Selected;
        }
    
        }
        
        
        
        public void HandleCombination(GameObject Dragged, GameObject Onto,DragEvent devt)
        {
            string CombinationType = null;
            
            if(Dragged.GetComponent<s_VisualNode>()!=null)
            {
                CombinationType = "Node";   
            }
            else
            {
                CombinationType = "Edge";
            }
            
            MergeEvent mevt = new MergeEvent(Dragged,Onto,CombinationType,CreateTimeStamp(),this.GetComponent<MouseObjectSelector>(),devt);
            fireEvent(mevt);
        }
    }

}
