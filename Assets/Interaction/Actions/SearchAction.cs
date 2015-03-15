using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace FAR{

    public class SearchAction : MonoBehaviour , Action {
    
        
        
        bool Active = false;
        
        bool ShowingResults = false;
    
        
        public void doEvent(InteractionEvent evt)
        {
    
            
            if(evt.GetType() == typeof(SingleKeyboardEvent))
            {
            
                if(Active == true)
                {
                    
                    SingleKeyboardEvent skevt = (SingleKeyboardEvent) evt;                
                    HandleKeyInput(skevt);
                    
                }
            }
                
            
            if(evt.GetType() == typeof(SelectionEvent))
            {
                SelectionEvent sevt = (SelectionEvent) evt;
                if(sevt.Obj == this.gameObject)
                {
                    this.GetComponent<InputField>().text = "";
                    Active = true;
                    SetCameraMovementOn(false);
                }
                else
                {
                    Active = false;
                    SetCameraMovementOn(true);
                }
            }
            
            if(evt.GetType() == typeof(MouseEvent))
            {
                MouseEvent mevt = (MouseEvent) evt;
                
                if(mevt.UpDown == Enums.MouseState.Down)
                {
                    Active = false;
                    SetCameraMovementOn(true);
                }
                
            }
            
        }
    
    
        /// <summary>
        /// Searchs the already parsed patterns in the Menu.
        /// Uses the same mechanic as the original TrackMan, splitting the search request into substrings
        /// and searching for every Pattern that contains all of them.
        /// </summary>
        /// <param name="srch">Srch.</param>
        void SearchLibrary(string srch)
        {
            char[] Seperatoren = {' '};
            string[] PartStrings = srch.Split(Seperatoren);
            
            //Beide items werden benötigt. Das erste zeigt welche Patterns wir haben...
            s_PatternReader PatternReader = GameObject.Find("NewInventory").GetComponent<s_PatternReader>();
            //...das zweite item erlaubt die Darstellung der neuen Patterns im InventoryGUI
            ScrollListTest Menu = GameObject.Find("NewInventory").GetComponent<ScrollListTest>();
            
            
            //Hierbei handelt es sich um die bisher geparsten Patterns. Aufteilung 1) Name 2) Adresse auf der Festplatte
            Dictionary<string,string> ParsedPatterns = PatternReader.GetFoundPatterns();
            //Hier werden alle Patterns gespeichert die dem Suchstring entsprechen
            List<string> SearchResults = new List<string>();
            
            foreach(string Key in ParsedPatterns.Keys)
            {
                bool ContainsAll = true;
                
                foreach(string TeilDerEingabe in PartStrings)
                {
    			if(!Key.Contains(TeilDerEingabe))
    			{
    	                        char[] Teil = TeilDerEingabe.ToCharArray();
    	                        
    	                        int Value = (int) Teil[0];
    	                        
    	
    	                        //Groß geschrieben
    	                       	if(Value >= 65 && Value <= 90)
    	                        {
    	                            Value += 0x20;
    	                        }
    	                        
    	                        Teil[0] = (char) Value;
    	                        
    	                        string GroßKleinText ="";
    	                        for( int i = 0; i < Teil.Length; i++ )
    	                        {
    	                            GroßKleinText += Teil[i];
    	                        }
    	                        
    	                           //Debug.Log("Could not find any for "+TeilDerEingabe+" and changed it to "+GroßKleinText);
    	                        
    	                        if(!Key.Contains(GroßKleinText))
    	                        {
    	                            ContainsAll = false;
    	                        }
                            }
         
                }
                
                
                if(ContainsAll)
                {
                    SearchResults.Add(Key);
                }
                
            }
            
            Menu.DeleteList("Right");
            
            foreach(string K in SearchResults)
            {
                Menu.AddEntry(K,"Right","Pattern");
            }
            
            ShowingResults = true;
            
    
        }
       
        
            public void HandleKeyInput(SingleKeyboardEvent kevt)
            {
                InputField IF = this.GetComponent<InputField>();
                
                if(Active == false)
                {
                    return;
                }
                
                string Key = kevt.key.ToString();
                
                if(Key == "#")
                {
                    SearchLibrary(IF.text);
                    IF.text = "";
                }
                else if(Key == "*")
                {
                    string NewText = IF.text;
                    if(NewText.Length > 0)
                    {
                        NewText = NewText.Remove(NewText.Length-1);
                    }
                    IF.text = NewText;
                }
                else
                {
                    IF.text += Key;
                    if(ShowingResults)
                    {
                        ScrollListTest Menu = GameObject.Find("NewInventory").GetComponent<ScrollListTest>();
                        Menu.DeleteList("Right");
                        ShowingResults = false;
                    }
                }
                
            }
        
        
        void SetCameraMovementOn(bool swit)
        {
            CameraAction MyCameraAction = Camera.main.GetComponent<CameraAction>();
            
            if(swit == false)
            {
                MyCameraAction.MovementBlocked = true;
            }
            else
            {
                MyCameraAction.MovementBlocked = false;
            }
        }
    }

}