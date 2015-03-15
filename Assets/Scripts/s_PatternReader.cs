using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using System.Xml;
using System.Text;

public class s_PatternReader : MonoBehaviour {

    s_Settings Einstellungen;
    ScrollListTest PatternWindow;

    string UbiTrackPfad;

    RectTransform SelectionSpace;

    Dictionary<string,string> DataFound;
    
    
    Dictionary<string,string> PatternsFound;

	// Use this for initialization
	void Start () {
    
        DataFound = new Dictionary<string, string>();
        PatternsFound = new Dictionary<string, string>();

        Einstellungen = GameObject.Find("Settings").GetComponent<s_Settings>();
        PatternWindow = GameObject.Find("NewInventory").GetComponent<ScrollListTest>();
        UbiTrackPfad = Einstellungen.UbiTrackPath;
        FindStartDirectories(UbiTrackPfad);
        
        StartCoroutine("FindAllPatternsInRoot",UbiTrackPfad);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public Dictionary<string,string> GetFoundPatterns()
    {
        return PatternsFound;
    }
    
    public Dictionary<string,string> GetFoundPatters()
    {
        return PatternsFound;
    }
    
    
    /// <summary>
    /// Finds all UbiTrack-Patterns in XML Files in the root directory set in the Settings-Object
    /// </summary>
    /// <returns>The all patterns in root.</returns>
    /// <param name="patternpath">Patternpath.</param>
    IEnumerator FindAllPatternsInRoot(string patternpath)
    {
        Dictionary<string,FileInfo> XMLFiles = new Dictionary<string, FileInfo>();
        int ProcessedXFiles = 0;
        int ProcessedPatterns = 0;
    
        DirectoryInfo Root = new DirectoryInfo(patternpath);
        
        DirectoryInfo[] temp = Root.GetDirectories();
        foreach( DirectoryInfo folder in temp)
        {
            FileInfo[] Files1 = folder.GetFiles();
            foreach(FileInfo file in Files1)
            {
                if(file.Name.Contains(".xml"))
                {
                    XMLFiles.Add(file.Name,file);
                    //
                    ProcessedXFiles++;
                    ProgressBar(ProcessedXFiles,ProcessedPatterns);
                }
            }
            
            DirectoryInfo[] Dir1 = folder.GetDirectories();
            foreach(DirectoryInfo folder2 in Dir1)
            {
                FileInfo[] Files2 = folder2.GetFiles();
                foreach(FileInfo file2 in Files2)
                {
                    if(file2.Name.Contains(".xml"))
                    {
                        XMLFiles.Add(file2.Name,file2);
                        //
                        ProcessedXFiles++;
                        ProgressBar(ProcessedXFiles,ProcessedPatterns);
                    }
                }
            }
            
            yield return new WaitForSeconds(0.2f);
        }
        

        
        foreach(string Key in XMLFiles.Keys)
        {
            List<string> PatternsContainedInXML = PatternParser(XMLFiles[Key].FullName);            
            foreach(string pattern in PatternsContainedInXML)
            {
                PatternsFound.Add(pattern,XMLFiles[Key].FullName);
                //
                ProcessedPatterns++;
                ProgressBar(ProcessedXFiles,ProcessedPatterns);
            }
            
            yield return new WaitForSeconds(0.1f);
        }
        
        Debug.Log("Processed "+ProcessedXFiles+" XML-Files and "+ProcessedPatterns+" Patterns");
    }

    
    /// <summary>
    /// Gets called by each clicked folder.
    /// This Method searches for subfolders and data to add to the DATAFOUND-Dictionary where their paths are stored
    /// </summary>
    /// <param name="name">Name.</param>
    public void FindSubDirectoriesOf(string name)
    {
        DirectoryInfo RootFolderInfo = new DirectoryInfo(DataFound[name]);
        List<DirectoryInfo> FirstFoldersInfo = new List<DirectoryInfo>(RootFolderInfo.GetDirectories());
        
        foreach (DirectoryInfo Ordner in FirstFoldersInfo)
        {
            if(!DataFound.ContainsKey(Ordner.Name))
            {
                DataFound.Add(Ordner.Name,Ordner.FullName);
            }
        }
    }
    
    

    
    /// <summary>
    /// Searches a path for subfolders and subfiles that will spawn to the right column of the main-inventory window
    /// </summary>
    /// <param name="name">Name.</param>
    public void FindUnderlyingData(string name)
    {
    
        //Debug.Log("Looking for data in "+name);
    
        if(!DataFound.ContainsKey(name))
        {
            Debug.Log("Ordner noch nicht entdeckt");
            return;
        }
        else
        {
            DirectoryInfo SubFolderInfo = new DirectoryInfo(DataFound[name]);
            List<DirectoryInfo> SubFoldersInfo = new List<DirectoryInfo>(SubFolderInfo.GetDirectories());
            List<FileInfo> SubFilesInfo = new List<FileInfo>(SubFolderInfo.GetFiles());

            
            foreach( DirectoryInfo Fold in SubFoldersInfo)
            {
                if(!DataFound.ContainsKey(Fold.Name))
                {
                    DataFound.Add(Fold.Name,Fold.FullName);
                }

                //Debug.Log("I found a Folder "+Fold.Name);
                PatternWindow.AddEntry(Fold.Name,"Right","Folder");
            }

            foreach( FileInfo FileD in SubFilesInfo)
            {
                if(!DataFound.ContainsKey(FileD.Name))
                {
                    DataFound.Add(FileD.Name,FileD.FullName);
                }

                //Debug.Log("I found a File  "+FileD.Name);
                List<string> BeinhaltetPatterns = new List<string>(); //PatternParser(FileD.FullName);
                
                foreach(string Key in PatternsFound.Keys)
                {
                    if(PatternsFound[Key] == FileD.FullName)
                    {
                        BeinhaltetPatterns.Add(Key);
                    }
                }
                
                foreach(string PatsName in BeinhaltetPatterns)
                {
                    //Debug.Log(PatsName);
                    PatternWindow.AddEntry(PatsName,"Right","Pattern");
                }
            }
        }
    }



    //COPIED FROM Bachelor's Project Andreas Langbein 2014
public List<string> PatternParser(string patternpath)
    {
        XmlNodeList patterns;


        List<string> FoundPatterns = new List<string>();
        
        StreamReader reader = new StreamReader(patternpath,Encoding.Default);
        string Text = reader.ReadToEnd();
        
        string patternText = Text;
        
        
        patternText = patternText.Replace("xsi:schemaLocation=\"http://ar.in.tum.de/ubitrack/utql http://ar.in.tum.de/files/ubitrack/utql/utql_types.xsd\" xmlns=\"http://ar.in.tum.de/ubitrack/utql\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "");
        patternText = patternText.Replace("xsi:type=\"utql:PrimitiveAttributeType\"", "");
        patternText = patternText.Replace("xmlns:utql=\"http://ar.in.tum.de/ubitrack/utql\"", "");
        
        
        ///////// Im folgenden Block entferne ich einige weitere Aspekte in den Pattern-XML ohne die sich die Files nicht(!!!) korrekt auslesen lassen!!
        /// NACHFRAGEN: Welche davon werden zu kritischen Problemen führen?
        /// 
        
        
        patternText = patternText.Replace ("xmlns='http://ar.in.tum.de/ubitrack/utql'","");
        //patternText = patternText.Replace("xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'","");
        //patternText = patternText.Replace ("xmlns:xi='http://www.w3.org/2001/XInclude'","");
        //patternText = patternText.Replace("xmlns:h=\"http://www.w3.org/1999/xhtml\"","");
        patternText = patternText.Replace("xsi:schemaLocation='http://ar.in.tum.de/ubitrack/utql ../../../schema/utql_templates.xsd'","");
        
        XmlDocument root = new XmlDocument();
        root.LoadXml(patternText);
        patterns = root.SelectNodes("//Pattern");
        
        //// Der folgende Block dursucht das Dokument nach allen Pattern-Nodes samt deren Inhalt.
        /// Zuerst wird eine PatternData erstellt die sich Namen,DisplayNamen und Beschreibung der Pattern sichert
        /// Diese speichern wir später(!!!) in einem Dictionary "Patterns"
        
        
        for ( int i = 0; i < patterns.Count; i++ )
        {
            
            string PatternName = patterns[i].SelectSingleNode("@name").InnerText;
            string PatternDisplayName = patterns[i].SelectSingleNode("@displayName").InnerText;
            string PatternDescription = patterns[i].SelectSingleNode(".//Description").InnerText;
            
            //s_PatternData PatternData = new s_PatternData(PatternName,PatternDisplayName,PatternDescription);

            FoundPatterns.Add (PatternName);
            //Debug.Log("Found Pattern : "+PatternName);

            
        }
        

        return FoundPatterns;
        
    }
    
    
    
    
    
    /// <summary>
    /// Gets called at the start of the application using the UBITRACK-Path set in the Settings-Object
    /// Searches the main Directory for each subfolder and shows them in the left column of the main-inventory window
    /// </summary>
    /// <param name="path">Path.</param>
    public void FindStartDirectories(string path)
    {
        DirectoryInfo RootFolderInfo = new DirectoryInfo(path);
        List<DirectoryInfo> FirstFoldersInfo = new List<DirectoryInfo>(RootFolderInfo.GetDirectories());
        
        foreach (DirectoryInfo Ordner in FirstFoldersInfo)
        {
            DataFound.Add(Ordner.Name,Ordner.FullName);
            PatternWindow.AddEntry(Ordner.Name,"Left","Folder");
        }
        
    }
    
    public void ExtractPatternsFromXML(string name)
    {
        List<string> FoundPatterns = PatternParser(DataFound[name]);
        foreach (string Pattern in FoundPatterns)
        {
            PatternWindow.AddEntry(Pattern,"Right","Pattern");
        }
    }
    
    
    public void ProgressBar(float ProcXFiles, float ProcPatterns)
    {

    
        float res = 0.0f;
        
        float XPart = 0.0f;
        float PatPart = 0.0f;
        
        XPart = ProcXFiles / (Einstellungen.XMLFilesInRoot / 50.0f);
        PatPart = ProcPatterns / (Einstellungen.PatternsInRoot / 50.0f);
        
        res = XPart+PatPart;
        
        res = Mathf.Clamp(res,0,100);
        
        //Debug.Log(res);
        if(res < 100)
        {
            GameObject.Find("LoadingProgress").GetComponent<Text>().text = "Loading.."+(int)res+"%";
        }
        else
        {
            Destroy(GameObject.Find("LoadingProgress"));
        }
    }
    

  
}




