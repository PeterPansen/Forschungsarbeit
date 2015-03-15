using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;

namespace FAR{

	public class SimpleFacade : MonoBehaviour {
		
		
		public string ubitrackComponentsPath="";	
		public string logConfig="";	
		public TextAsset dataflowFile=null;
		
		public bool DoNotDestroyOnLoad = false;
	    
		protected Ubitrack.SimpleFacade m_facade = null;
	
	    protected DFGParser dfgParser = null;
	    protected HashSet<UbiTrackComponent> appComponents = new HashSet<UbiTrackComponent>();
	    protected bool initialized = false;
		
		protected HashSet<UbiTrackComponent> appComponentsToStart = new HashSet<UbiTrackComponent>();
		
		// Use this for initialization
		
		void Awake(){
			if(DoNotDestroyOnLoad)
				DontDestroyOnLoad(this);
		}
		
	    void OnEnable()
	    {
	        if (initialized) return;
			
			
			
			string dfgString="";	  		
	        if (ubitrackComponentsPath == null )
	        {
	            throw new MissingMemberException("ubitrack component path is empty, ubitrack can't work");
	        }
			
			#if !UNITY_ANDROID || UNITY_EDITOR		
	
			string dir = Path.GetFullPath(Application.dataPath+Path.DirectorySeparatorChar+"..")+Path.DirectorySeparatorChar;
			
			DirectoryInfo dirInfo = new DirectoryInfo(dir);
			FileInfo[] filesInDir = dirInfo.GetFiles();
			
			foreach(FileInfo f in filesInDir) 
			{			
				if(f.Extension.Equals(".dfg")) 
				{
					Debug.Log("Found dfg in project folder:"+f.FullName);
					StreamReader reader = f.OpenText();				
					dfgString = reader.ReadToEnd();
					reader.Close();
					break;
				}
					
			}
			
	       
			
			if(logConfig.Length > 0) {			
				string logfile = dir+logConfig;
				if(File.Exists(logfile))
				{
					Debug.Log("Init UbiTrack Logging with file:" + 	logfile);
	                Ubitrack.ubitrack.initLogging(logfile);
				} else {
					Debug.LogError("UbiTrack log file does not exist: "+logfile);
				}
				
			}
			#else
			var unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			var activity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			// Retrieve ApplicationInfo and nativeLibraryDir (N.B. API-9 or newer only!)
			var info = activity.Call<AndroidJavaObject>("getApplicationInfo");
			ubitrackComponentsPath = info.Get<String>("nativeLibraryDir");		
			
			Ubitrack.ubitrack.initLogging("TRACE");
			#endif
			
			
			
			if (dfgString.Length == 0)
	        {
				if(dataflowFile != null) 
				{
					dfgString = dataflowFile.text;
				} 
				else 
				{
	            	throw new MissingMemberException("ubitrack dataflow is empty, ubitrack can't work");
				}
	        }
			
			
	                
			m_facade = new Ubitrack.SimpleFacade(ubitrackComponentsPath);
	
	        dfgParser = new DFGParser(dfgString);
		
			
			
	
	        
			
	        
	        
		}
	
	    void Start()
	    {
	        if (initialized) return;
	        String dfgString = dfgParser.getDFG();
	        Debug.Log("Loading dfg file:" + dfgString);
	        m_facade.loadDataflowString(dfgString);        
	        
			startUbiTrack();
			
	        initialized = true;
	    }
		
		void Update(){
			if(!initialized) return;
			if(appComponentsToStart.Count == 0) return;
			
			foreach (UbiTrackComponent comp in appComponentsToStart)
	        {
	            comp.utStart();
	        }
			
			appComponentsToStart.Clear();
			
		}
		
		public void startUbiTrack(){
			foreach (UbiTrackComponent comp in appComponents)
	        {
	                comp.utInit(m_facade);                                        
	        }
	           
	
	        m_facade.startDataflow();
	
	        foreach (UbiTrackComponent comp in appComponents)
	        {
	            comp.utStart();
	        }
		}
		
		public void stopUbiTrack(){
			if (m_facade == null)
	            return;
	    
	        Debug.Log("stopUbiTrack, Shutting down ubitrack, components:" + appComponents.Count);
	
	        foreach (UbiTrackComponent comp in appComponents)
	        {
	            if (comp != null)
	            {
	                comp.utStop();
	            }
	
	        }
	
	        m_facade.stopDataflow();
	        
	        
	        
	
	        foreach (UbiTrackComponent comp in appComponents)
	        {
	            if (comp != null)
	            {
	                comp.utDestroy();
	            }
	
	        }
	
	        m_facade.clearDataflow();
	
	
	        //m_facade.killEverything();
	    	m_facade = null;
			Debug.Log("Ubitrack shut down");
		}
	    
	    void OnApplicationQuit()
	    {
			stopUbiTrack();               
	    }	
	
	    internal void registerUbitrackComponent(UbiTrackComponent ubiTrackComponent)
	    {        
	        appComponents.Add(ubiTrackComponent);
			if(initialized){
				ubiTrackComponent.utInit(m_facade);
				appComponentsToStart.Add(ubiTrackComponent);			
			}
	    }
			
	
	    internal System.Xml.XmlDocument getDFG()
	    {
	        return dfgParser.getRoot();
	    }
	}

}

