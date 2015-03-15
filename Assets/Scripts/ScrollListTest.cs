using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScrollListTest : MonoBehaviour {

	GameObject cellPrefab;
    GameObject pattern_cellPrefab;

	public GridLayoutGroup grid;
    public GridLayoutGroup grid2;

	public InputField numCellsInput;

	// Use this for initialization
	void Start () {
    
        cellPrefab = GameObject.Find("PrefabButton");
        pattern_cellPrefab = GameObject.Find("PrefabPatternButton");

        cellPrefab.SetActive(false);
        pattern_cellPrefab.SetActive(false);
	
	}
	
	// Update is called once per frame
	void Update () {
    


	}
    
    /// <summary>
    /// Allocates the grid elements to the camera.
    /// Enter 1 for LeftGrid and 2 for RightGrid (Yes it is not ZeroBased)
    /// Sometimes GridElements try to face the camera even when the GUI is willingly dragged in Perspective
    /// To fix this we order the GridElements to allocate themselves just like the MainWindow does.
    /// </summary>
    /// <param name="Grid">Grid.</param>
    public void AllocateGridElementsToCamera(int Grid)
    {
        if(Grid==1)
        {
            for ( int i = 0; i < grid.transform.childCount; i++ )
            {
                grid.transform.GetChild(i).transform.rotation = this.GetComponent<RectTransform>().transform.rotation;
            }
        }
        
        if(Grid==2)
        {
            for ( int i = 0; i < grid2.transform.childCount; i++ )
            {
                grid2.transform.GetChild(i).transform.rotation = this.GetComponent<RectTransform>().transform.rotation;
            }
        }
    }
    
    
    /// <summary>
    /// Adds a new entry to the inventory Window
    /// Enter a NAME for the new item, LEFTRIGHT with "Left" or "Right" to place it in the corresponding column
    /// And "File" or "Folder" to spawn the right kind of item
    /// </summary>
    /// <param name="name">Name.</param>
    /// <param name="LeftRight">Left right.</param>
    /// <param name="Type">Type.</param>
    public void AddEntry(string name,string LeftRight, string Type)
    {
        cellPrefab.SetActive(true);
        pattern_cellPrefab.SetActive(true);
        GameObject cell;

        if (Type == "Folder")
        {
            cell = (GameObject)GameObject.Instantiate(cellPrefab);
        } else if (Type == "Pattern")
        {
            cell = (GameObject)GameObject.Instantiate(pattern_cellPrefab);
        }
        else
        {
            return;
        }

        cell.name = name;
        cell.GetComponentInChildren<Text>().text = name;
        
        if(LeftRight=="Left")
        {
            cell.transform.SetParent(grid.transform);
        }
        else if(LeftRight=="Right")
        {
            cell.transform.SetParent(grid2.transform);
        }
        else
        {
            Debug.Log("AddEntry called with wrong parameter : "+LeftRight);
        }
        
        //RectTransform MainPart = cell.GetComponent<RectTransform>();
        //MainPart.transform.position = new Vector3(MainPart.transform.position.x,MainPart.transform.position.y,-35.0f);
        cellPrefab.SetActive(false);
        pattern_cellPrefab.SetActive(false);
        
        //cell.GetComponent<Button>().
    }
    
    /// <summary>
    /// Deletes the LeftRight list ("Left" for left, "Right" for right) in the main inventory
    /// </summary>
    /// <param name="LeftRight">Left right.</param>
    public void DeleteList(string LeftRight)
    {
       if(LeftRight == "Left")
       {
            for ( int i = 0; i < grid.transform.childCount; i++ )
            {
                  //Destroy(grid.transform.GetChild(i).GetComponent<Image>());
                  Destroy(grid.transform.GetChild(i).gameObject);
            }
        }
        
        if(LeftRight == "Right")
        {
            for ( int i = 0; i < grid2.transform.childCount; i++ )
            {
                Destroy(grid2.transform.GetChild(i).gameObject);
            }
        }
        
    }

    /// <summary>
    /// Able to Spawn a List to the "Left" or "Right" (LeftRight) column of the main inventory window
    /// The Dictionary NameType needs a name for the new item as Key and the fitting Type "Folder" or "File"
    /// </summary>
    /// <param name="LeftRight">Left right.</param>
    /// <param name="NameType">Name type.</param>
    public void SpawnNewList(string LeftRight, Dictionary<string,string> NameType)
    {
        if(LeftRight=="Left")
        {
            DeleteList("Left");
        }
        if(LeftRight=="Right")
        {
            DeleteList("Right");
        }
        else
        {
            Debug.Log("Cannot understand Parameter LeftRight");
            return;
        }
        
        foreach(string K in NameType.Keys)
        {
            AddEntry(K,LeftRight,NameType[K]);
        }
    }
    
    
    
    
    
    
    
    public void OnSearchSubmitted()
    {
        Debug.Log("You submitted");
    }
    
    
    
    
    
    
    
    
    /// <summary>
    /// Gets called by each button when pressed
    /// The method chekcs for the callers name and reacts correspondingly
    /// </summary>
    /// <param name="temp">Temp.</param>
    public void OnButtonClicked(GameObject temp)
    {

        GameObject.Find("Grid2").transform.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;

    
        if(temp.tag=="Pattern")
        {
            //DeleteList("Right");
            Dictionary<string,string> PatF = this.GetComponent<s_PatternReader>().GetFoundPatterns();
            //Debug.Log("Trying to spawn "+temp.name);
            if(PatF.ContainsKey(temp.name))
            {
                //Debug.Log("Found it");
                //Debug.Log("Sending Data : "+ temp.name +" "+ PatF[temp.name]);
                GameObject.Find("Spawner").GetComponent<s_PatternSpawner>().PatternParser(temp.name,PatF[temp.name]);
            }
            //this.GetComponent<s_PatternSpawner>().SpawnPattern(temp.name);
        }
        else
        {
            DeleteList("Right");
            this.GetComponent<s_PatternReader>().FindSubDirectoriesOf(temp.name);
            this.GetComponent<s_PatternReader>().FindUnderlyingData(temp.name);
            AllocateGridElementsToCamera(2);
        }

    }
}
