using UnityEngine;
using System.Collections;

namespace FAR{

    public class WorldEvents : InteractionMethod,Action {
    
        /*
            This class mainly serves for Eventhandling that targets the entire scene.
            Known examples are situations like SAVING/LOADING or Importing/Exporting a DFG-File.
        */
    
    
        void Start()
        {
            base.Start();
        }
        
        void Update()
        {
            CheckForNewListeners();
        }
    
        public void doEvent(InteractionEvent evt)
        {
            if(evt.GetType() == typeof(SaveLoadEvent))
            {
                SaveLoadEvent SLevt = (SaveLoadEvent) evt;
                /*
                    TODO:
                    Load and Save CurrentSetup
                */
            }
            if(evt.GetType() == typeof(LayoutEvent))
            {
                LayoutEvent levt = (LayoutEvent) evt;
                /*
                    TODO:
                    Change current camera look on the scene
                */
            }
        }
    }
}