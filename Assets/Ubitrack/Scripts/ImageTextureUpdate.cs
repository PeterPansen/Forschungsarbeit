using UnityEngine;
using System;
using System.Collections;
using Ubitrack;

namespace FAR{

	public class ImageTextureUpdate : UbiTrackComponent {
	
		
		public bool m_flipVertical;
	    public bool m_flipHorizontal;
	
		
		private int textureID;
		
	    private int m_imageWidth = 320;
	    private int m_imageHeight = 240;
		private int m_imageChannels = 3; 
	    private int m_texWidth;
	    private int m_texHeight;
	    private float m_textureInsetWidth;
	    private float m_textureInsetHeight;
		private float m_textureInsetX;
		private float m_textureInsetY;
	
	    protected Texture2D m_texture=null;
	
	    private int m_screenWidth;
	    private int m_screenHeight;
		public GUITexture m_guiTexture;
		public Material m_material;
		
		private bool m_isTextureInitialized = false;
		private SimplePosition3D m_dummyValue = new SimplePosition3D();
	    
	    private SimpleApplicationPullSinkPosition3D m_dummy;
		
	    public override void utInit(Ubitrack.SimpleFacade simpleFacade)
	    {
	        base.utInit(simpleFacade);
	        m_dummy = simpleFacade.getPullSinkPosition3D(patternID);
	        if (m_dummy == null)
	        {
	            throw new Exception("SimpleApplicationPushSourceButton not found for ID:" + patternID);
	        }
			 this.m_screenHeight = Screen.height;
	         this.m_screenWidth = Screen.width;
			//initTexture();
	    }
	
	
		// Update is called once per frame
		void Update () {
	        if(m_isTextureInitialized){			
				//Profiler.StartProfile("updateTextureComponent");          
				ulong dummy = (ulong)textureID;
				m_dummy.getPosition3D(m_dummyValue, dummy);
				//if(m_dummyValue.timestamp == dummy) Profiler.EndProfile("updateTextureComponent");                    				
			} else {
				ulong dummy = (ulong)0;
				m_dummy.getPosition3D(m_dummyValue, dummy);
				if(m_dummyValue.x > 10f){
				m_imageWidth = (int)m_dummyValue.x;
				m_imageHeight = (int)m_dummyValue.y;
				m_imageChannels= (int)m_dummyValue.z;
				initTexture();
				}
			}
			//Profiler.PrintResults();
		}
		void OnApplicationPause(){
			//Profiler.PrintResults();
		}
		private void initTexture(){
					
			m_texWidth = (int)Math.Pow(2, Math.Ceiling(Math.Log(m_imageWidth) / Math.Log(2)));
			m_texHeight = (int)Math.Pow(2, Math.Ceiling(Math.Log(m_imageHeight) / Math.Log(2)));
			
			
			
			m_textureInsetWidth = ((float)m_screenWidth) / m_imageWidth * m_texWidth;
			m_textureInsetHeight = ((float)m_screenHeight) / m_imageHeight * m_texHeight;
			m_textureInsetX = 0;
			m_textureInsetY = 0;		
			
			if(m_flipVertical){
				m_textureInsetHeight = -m_textureInsetHeight;
				m_textureInsetY = m_screenHeight;
			}
			
			if(m_flipHorizontal){
				m_textureInsetWidth = -m_textureInsetWidth;
				m_textureInsetX = m_screenWidth;
			}
			
			Debug.Log("Init Texture2D: " + m_imageWidth + "x" + m_imageHeight + "  on screen:" + m_screenWidth + "x" + m_screenHeight + " with textureSize:" + m_texWidth + "x" + m_texHeight);					
			if(m_imageChannels == 3)
				m_texture = new Texture2D(m_texWidth, m_texHeight, TextureFormat.RGB24, false);
			else
				m_texture = new Texture2D(m_texWidth, m_texHeight, TextureFormat.RGBA32, false);
			
			Debug.Log("TextureID:"+m_texture.GetNativeTextureID());
			m_texture.filterMode = FilterMode.Bilinear;					
			
			if(m_material != null)
			{
				m_material.mainTexture = m_texture;
				float scaleX = (float)m_imageWidth / m_texWidth;
				float scaleY = (float)m_imageHeight / m_texHeight;						
				m_material.SetFloat("_UbiWidthFactor", scaleX);
				m_material.SetFloat("_UbiHeightFactor", scaleY);												
			}
			if(m_guiTexture != null)
			{					
				Debug.Log("m_guiTexture:");
				m_guiTexture.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
			    m_guiTexture.pixelInset = new Rect(m_textureInsetX, m_textureInsetY, m_textureInsetWidth, m_textureInsetHeight);
				m_guiTexture.texture = m_texture;
			}	
			textureID = m_texture.GetNativeTextureID();
			m_isTextureInitialized = true;
	        
		}
	}

}
