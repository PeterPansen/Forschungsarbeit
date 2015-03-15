using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FAR{

	//Vorher NetworkPoseSink jetzt auf UnityPerspektive geändert
	public class NetworkPoseSource_LightWeight : MonoBehaviour {
	    public UbitrackRelativeToUnity relative = UbitrackRelativeToUnity.World;
		protected Thread receiveThread;    
	    protected UdpClient client; 
		public int port = 21844;
		
		protected bool m_keepRunning = true;
		
		protected Pose m_newData = null;	
	
		// Use this for initialization
		void Start () {	
	        
	
	        receiveThread = new Thread(
	
	            new ThreadStart(ReceiveData));
	
	        receiveThread.IsBackground = true;
	
	        receiveThread.Start();
		}
		
		void OnDisable() {
			m_keepRunning = false;
		}
			
	    private void ReceiveData()
	    {
	        client = new UdpClient(port);
	        while (m_keepRunning)
	        {
	            try
	            {
	
	                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
	
	                byte[] data = client.Receive(ref anyIP);
	
	 
	
	                // Bytes mit der UTF8-Kodierung in das Textformat kodieren.
	
	                string text = Encoding.UTF8.GetString(data);
					
					Debug.Log(text);
					
					string[] words = text.Split(' ');
	
	 				
					Quaternion q = new Quaternion(float.Parse(words[12]),
					                              float.Parse(words[13]),
					                              float.Parse(words[14]),
					                              float.Parse(words[15]));
					Vector3 t = new Vector3(float.Parse(words[18]),
					                        float.Parse(words[19]),
					                        float.Parse(words[20]));
	
	                m_newData = new Pose();
	                UbiMeasurementUtils.coordsysemChange(t, ref m_newData.pos);
	                UbiMeasurementUtils.coordsysemChange(q, ref m_newData.rot);
	
	            }
	
	            catch (Exception err)
	
	            {
	
	                Debug.Log(err.ToString());
	
	            }
	
	        }
			client.Close();
	
	    }
		// Update is called once per frame
		void Update () {
	        if (m_newData != null)
	        {
	            UbiUnityUtils.setGameObjectPose(relative, gameObject, m_newData);            
	            m_newData = null;
			}
		}
	}

}
