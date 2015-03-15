using UnityEngine;
using System;
using System.Collections;
using Ubitrack;


namespace FAR{

	//Vorher ImageSink jetzt auf UnityPerspektive gewechselt
	public class ImageSource : UbiTrackComponent {
		public UbitrackEventType ubitrackEvent = UbitrackEventType.Push;
		
		
		public bool flipVertical = false;
	    public bool flipHorizontal = false;
		
		public Material imageToMaterial;
		public GUITexture imageToGuiTexture;
	
	    
	    protected UnityImageReceiver m_imageReciever = null;
		// Use this for initialization
	    public override void utInit(Ubitrack.SimpleFacade simpleFacade)
	    {
	        base.utInit(simpleFacade);
			
			switch(ubitrackEvent)
			{
			case UbitrackEventType.Pull:{
					
					throw new Exception("Pull not implemented yet");				
				}
			case UbitrackEventType.Push:{
					m_imageReciever = new UnityImageReceiver(imageToGuiTexture, imageToMaterial, flipVertical, flipHorizontal);
	
	
	            
	                if (!simpleFacade.setImageCallback(patternID, m_imageReciever))
					{
						throw new Exception("SimpleImageReceiver could not be set for poseID:");
					}
				
					break;
				}
			default:
			break;
			}    	
	
		}
	
	 
	  
	
		// Update is called once per frame
	    //void FixedUpdate()
		void Update()
	    {
		
			switch(ubitrackEvent)
			{
			case UbitrackEventType.Pull:{				
					break;
				}
			case UbitrackEventType.Push:{
	            
	            m_imageReciever.newImageDataAvailable();
	            // nothing to to when new data is already pushed into the texture
	        	break;
				}
			default:
			break;
			} 
	        	
	    }	
	}

}
