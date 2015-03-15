using UnityEngine;
using System.Collections;
using Ubitrack;
using System;
using UnityTextureUpdate;
using System.Runtime.InteropServices;

namespace FAR{

	public class UnityImageReceiver : SimpleImageReceiver
	    {        		
			protected bool m_flipVertical;
	        protected bool m_flipHorizontal;
	
			protected Color32[] m_colorsCurrent;
			protected Color32[] m_colorsNext;		
			
			
	        public int m_imageWidth;
	        public int m_imageHeight;
	        public int m_texWidth;
	        public int m_texHeight;
	        public float m_textureInsetWidth;
	        public float m_textureInsetHeight;
			public float m_textureInsetX;
			public float m_textureInsetY;
	
	        protected Texture2D m_texture=null;
	
	        private bool m_newImageData = false;
			
	        public int m_screenWidth;
	        public int m_screenHeight;
			private GUITexture m_guiTexture;
			private Material m_material;
			
			private bool m_isTextureInitialized = false;
	
	        private System.Object thisLock = new System.Object();    
		
			
			
	        public UnityImageReceiver(GUITexture guiTexture, Material material, bool flipVertical, bool flipHorizontal)
	        {
	            
	            this.m_screenHeight = Screen.height;
	            this.m_screenWidth = Screen.width;
				m_flipVertical = flipVertical;
				m_flipHorizontal = flipHorizontal;
				m_guiTexture = guiTexture;
				m_material = material;			
	            
	        }		
					
	        public bool newImageDataAvailable()
	        {
	            if(!m_isTextureInitialized && m_newImageData){	                
		                Debug.Log("Init Texture2D: " + m_imageWidth + "x" + m_imageHeight + "  on screen:" + m_screenWidth + "x" + m_screenHeight + " with textureSize:" + m_texWidth + "x" + m_texHeight);					
						
						m_texture = new Texture2D(m_texWidth, m_texHeight, TextureFormat.RGBA32, false);
						
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
							m_guiTexture.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
		                    m_guiTexture.pixelInset = new Rect(m_textureInsetX, m_textureInsetY, m_textureInsetWidth, m_textureInsetHeight);
							m_guiTexture.texture = m_texture;
						}						
						m_isTextureInitialized = true;
	            	}
	
				 lock (thisLock)
				{
	            	if (m_newImageData)
	            	{
								
	                    m_newImageData = false;
						
						//Profiler.StartProfile("Set Pixels");                    				
						m_texture.SetPixels32(m_colorsCurrent, 0);					
						//Profiler.EndProfile("Set Pixels");
	                    //Profiler.StartProfile("Apply call");
						m_texture.Apply(false);
						//Profiler.EndProfile("Apply call");
						
						
						return true;
					}
	                
	            }
	            return false;
	        
	        }
	     
	        public override void receiveImage(SimpleImage theImage)
	        {
	            
	           if(!m_isTextureInitialized){
	                m_imageWidth = theImage.width;
	                m_imageHeight = theImage.height;
	                m_texWidth = (int)Math.Pow(2, Math.Ceiling(Math.Log(m_imageWidth) / Math.Log(2)));
	                m_texHeight = (int)Math.Pow(2, Math.Ceiling(Math.Log(m_imageHeight) / Math.Log(2)));
					
					
					m_colorsCurrent = new Color32[m_texWidth * m_texHeight];
					m_colorsNext = new Color32[m_texWidth * m_texHeight];				
	
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
	            }
				
					//Profiler.StartProfile("updateTextureUsingPointer");     
		            		
					//TextureUpdate.updateTextureUsingPointerFlipVertical(m_colorsNext, theImage, m_texWidth, m_texHeight, 255 );					
					TextureUpdate.updateTextureUsingPointer(m_colorsNext, theImage, m_texWidth, m_texHeight, 255 );					
		            
					//Profiler.EndProfile("updateTextureUsingPointer");     
			
	                lock (thisLock)
				{				
									
		            m_newImageData = true;                      
					Color32[] tmp = m_colorsCurrent;
					m_colorsCurrent = m_colorsNext;
					m_colorsNext = tmp;
						
					
				}
			
				}
				
				
			        
	    }
    
}
