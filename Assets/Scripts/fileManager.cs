using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine.UI;

namespace FAR{

	public class fileManager : InteractionMethod {
	
	
		public GameObject PrefabButton;
		
		public GridLayoutGroup GGroup;
		
		public InputField IpField;
		
		List<GameObject> ActiveButtons = new List<GameObject>();
		
		Enums.SL_State CurrentState;
		
		
		
		string CurrentPath;
		string LastPath;
		
		// Use this for initialization
		override public void Start () {
		
			base.Start();
			GGroup = GetComponentInChildren<GridLayoutGroup>();
			PrefabButton.GetComponentInChildren<Text>().text = "Back";
			ScanPath("C:\\Users\\");
		}
		
		public void ResetPaths()
		{
			Debug.Log ("CALL ME MAYBE");
			CurrentPath = "C:\\Users\\";
			IpField.text = "C:\\Users\\";
			for( int i = 0; i < ActiveButtons.Count; i++ )
			{
				if(ActiveButtons[i]!=PrefabButton)
				{
					Destroy (ActiveButtons[i]);
				}
			}
			ActiveButtons.Clear();
			ScanPath("C:\\Users\\");
		}
		

		// Update is called once per frame
		void Update () {
		
			CheckForNewListeners();
		
		}
		
		//Updates the Path corresponding to changes in the InputField
		public void FieldEdit (InputField I)
		{
			CurrentPath = I.text;
		}
		
		void ScanPath(string path)
		{
	
			LastPath = CurrentPath;
			CurrentPath = path;
			IpField.text = CurrentPath;
			
			for(int i = 0; i < ActiveButtons.Count; i++)
			{
				Destroy(ActiveButtons[i]);
			}
			ActiveButtons.Clear();
			
			DirectoryInfo dir = new DirectoryInfo(path);
			FileInfo[] info = dir.GetFiles();
			DirectoryInfo[] dinfo = dir.GetDirectories();
			foreach (FileInfo f in info)
			{
				GameObject Button = (GameObject)GameObject.Instantiate(PrefabButton);
				Button.transform.position = PrefabButton.transform.position;
				Button.transform.localScale = PrefabButton.transform.localScale;
				Button.transform.parent = PrefabButton.transform.parent;
				Button.GetComponent<RectTransform>().localScale = PrefabButton.GetComponent<RectTransform>().localScale;
				Button.transform.SetParent(GGroup.transform);
				ActiveButtons.Add(Button);
				Button.GetComponentInChildren<Text>().text = f.Name;
			}
			foreach (DirectoryInfo d in dinfo)
			{
				//Ordner wie .ssh werden hier herausgefiltert und ignoriert
				if(!d.Name.StartsWith("."))
				{
					GameObject Button = (GameObject)GameObject.Instantiate(PrefabButton);
					Button.transform.position = PrefabButton.transform.position;
					Button.transform.localScale = PrefabButton.transform.localScale;
					Button.transform.parent = PrefabButton.transform.parent;
					Button.GetComponent<RectTransform>().localScale = PrefabButton.GetComponent<RectTransform>().localScale;
					Button.transform.SetParent(GGroup.transform);
					Button.GetComponentInChildren<Text>().text = d.Name;
					ActiveButtons.Add(Button);
				}
			}
		}
		
		public void ButtonClicked(GameObject But)
		{
			Text txt = But.GetComponentInChildren<Text>();
			if(txt.text.EndsWith(".dfg"))
			{
				//ImportDFG
				CurrentPath = CurrentPath+txt.text;
				CurrentState = Enums.SL_State.LOAD;
				FileBrowserEvent fevt = new FileBrowserEvent(CurrentPath,Enums.SL_State.SAVE,CreateTimeStamp(),this.GetComponent<InteractionMethod>(),null);
				fireEvent(fevt);
				Debug.Log ("Loading DFG File from "+CurrentPath);
				this.gameObject.SetActive(false);
				
			}
			else if(txt.text=="Back")
			{
				ScanPath(LastPath);
			}
			else
			{
				ScanPath(CurrentPath+txt.text+"\\");
			}
		}
		
		public void Confirm()
		{
			if(!IpField.text.EndsWith(".dfg"))
			{
				CurrentPath = IpField.text+"_T3D.dfg";
			}
			
			FileBrowserEvent fevt = new FileBrowserEvent(CurrentPath,Enums.SL_State.SAVE,CreateTimeStamp(),this.GetComponent<InteractionMethod>(),null);
			fireEvent(fevt);
			
			Debug.Log ("Creating a DFG at "+CurrentPath);
			
			this.gameObject.SetActive(false);
			
			

		}
		
		
		
		public void CloseWindow()
		{
			this.gameObject.SetActive(false);
		}
		
	
	}

}
