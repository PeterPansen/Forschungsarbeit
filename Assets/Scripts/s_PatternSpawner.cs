using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;
using FAR;

public class s_PatternSpawner : InteractionMethod {

    
    int PatternIDCounter = 0;
    int NodeIDCounter = 0;
    List<string> UsedNodeIDs = new List<string>();
    LayoutManager Layouter;


	// Use this for initialization
	void Start () {
    
        base.Start();
        Layouter = GameObject.Find("LayoutSpawner").GetComponent<LayoutManager>();
	
	}
	
	// Update is called once per frame
	void Update () {
    
	
	}
    
    
    public void InstantiatePattern(string patternname,s_PatternData Norm_Pat)
    {
        s_PatternData ToBeInstanciated = Norm_Pat;
        
        //Erstellen einer Instanziierten Version der Pattern
        s_InstantPatternData InstantPattern = new s_InstantPatternData(ToBeInstanciated);
        Debug.Log ("Instantiating a new Pattern with ID "+ PatternIDCounter);
        InstantPattern.ID = "Pattern_"+PatternIDCounter.ToString();
        PatternIDCounter++;
        
        //Alle InputNodes instantiieren
        foreach(s_NodeData IN_Data in ToBeInstanciated.inputNodes)
        {
            
            s_InstantNodeData InstantNodeInput = new s_InstantNodeData(IN_Data);
            
            //Wir benötigen eindeutige IDs
            InstantNodeInput.ID = "INode_"+NodeIDCounter.ToString();
            UsedNodeIDs.Add ("INode_"+NodeIDCounter);
            NodeIDCounter++;
            InstantPattern.inputNodes.Add(InstantNodeInput);
        }
        
        //Alle OutputNodes instantiieren
        foreach(s_NodeData ON_Data in ToBeInstanciated.outputNodes)
        {
            s_InstantNodeData InstantNodeOutput = new s_InstantNodeData(ON_Data);
            //Wir benötigen eindeutige IDs
            InstantNodeOutput.ID = "ONode_"+NodeIDCounter.ToString();
            NodeIDCounter++;
            UsedNodeIDs.Add ("ONode_"+NodeIDCounter);
            InstantPattern.outputNodes.Add(InstantNodeOutput);
        }
        
        //Alle InputEdges instantiieren 
        foreach(s_EdgeData IE_Data in ToBeInstanciated.inputEdges)
        {
            s_InstantEdgeData InstantEdgeInput = new s_InstantEdgeData(IE_Data);
            //INPUT Edges geben PatternRefs an
            InstantEdgeInput.PatternRef = InstantPattern.ID;
            InstantPattern.inputEdges.Add(InstantEdgeInput);
        }
        
        //Alle OutputEdges instantiieren
        foreach(s_EdgeData OE_Data in ToBeInstanciated.outputEdges)
        {
            s_InstantEdgeData InstantEdgeOutput = new s_InstantEdgeData(OE_Data);
            InstantPattern.outputEdges.Add(InstantEdgeOutput);
        }
        
        
        ///REM
        
        
        Debug.Log ("Building Pattern "+InstantPattern.PatternName);
        s_InstantNodeData CurrNode;
        s_InstantEdgeData CurrEdge;
        GameObject Parent = new GameObject();
        GameObject GO;
        Parent.AddComponent<s_VisualPatternData>();
        Parent.GetComponent<s_VisualPatternData>().InstantPatternData = InstantPattern;
        Parent.name = InstantPattern.PatternName;
        Parent.tag = "Pattern";
        
       // Dictionary<string,GameObject> SpawnedNodes = new Dictionary<string, GameObject>();
       // Dictionary<string,GameObject> SpawnedEdges = new Dictionary<string, GameObject>();
        
        GameObject NodePrefab = (GameObject)Resources.Load ("Node");
        //GameObject EdgePrefab = (GameObject)Resources.Load ("Node");
        Material DottedLine = (Material)Resources.Load ("TranspLine",typeof(Material));
        Material SimpleRed = (Material)Resources.Load ("Red",typeof(Material));
        
        if(NodePrefab == null) //|| EdgePrefab == null)
        {
            Debug.Log ("The required Prefabs for Graph-Construction seem to be missing");
        }
        
        
        //OutputNodes Spawnen
        for ( int i = 0; i < InstantPattern.outputNodes.Count; i++)
        {
            GO = (GameObject)Instantiate(NodePrefab,Layouter.CalcSpawnPosition(),Quaternion.identity);//new Vector3(Random.Range (MinX,MaxX),Random.Range (MinY,MaxY),Z),Quaternion.identity);
            //// 
            // Send SpawnEvent to all Listeners
            SpawnEvent sevt = new SpawnEvent(CreateTimeStamp(),null,this.GetComponent<InteractionMethod>(),
            Enums.GraphElementType.Node,"Output",InstantPattern);
            fireEvent(sevt);
            // End of SpawnEvent sending
            
            HelperFunc.AddActionToAllInteractionMethods(GO);
            
            GO.transform.parent = Parent.transform;
            //Übergabe aller wichtigen Informationen
            CurrNode = InstantPattern.outputNodes[i];
            
            
            
            CurrNode.AttributeUndWerte = InstantPattern.outputNodes[i].AttributeUndWerte;
            CurrNode.ID = InstantPattern.outputNodes[i].ID;
            CurrNode.IncomingEdges = InstantPattern.outputNodes[i].IncomingEdges;
            CurrNode.InputOutput = InstantPattern.outputNodes[i].InputOutput;
            CurrNode.NodeDescription = InstantPattern.outputNodes[i].NodeDescription;
            CurrNode.NodeDisplayName = InstantPattern.outputNodes[i].NodeDisplayName;
            CurrNode.NodeName = InstantPattern.outputNodes[i].NodeName;
            
            CurrNode.OutGoingEdges = InstantPattern.outputNodes[i].OutGoingEdges;
            CurrNode.PatternName = InstantPattern.outputNodes[i].PatternName;
            
            
            //REM
            GO.GetComponent<s_VisualNode>().InstantNodeData = CurrNode;
            //
            
            //Bezeichnung festlegen
            GO.name = CurrNode.NodeName;//+"_Node";
            /*
            TextMesh TM = GO.GetComponent<TextMesh>();
            TM.text = CurrNode.NodeDisplayName; */
            
            GO.GetComponent<s_VisualNode>().SetGuiText(CurrNode.NodeDisplayName);
            
            //GO.transform.renderer.material = (Material) Resources.Load("Blue") as Material;
            GO.GetComponent<s_VisualNode>().SetColor("Blue");
            //Speichern
            
            //SpawnedNodes.Add (CurrNode.ID,GO);
        }
        
        //InputNodes Spawnen
        for ( int i = 0; i < InstantPattern.inputNodes.Count; i++)
        {
            GO = (GameObject)Instantiate(NodePrefab,Layouter.CalcSpawnPosition(),Quaternion.identity);//new Vector3(Random.Range (MinX,MaxX),Random.Range (MinY,MaxY),Z),Quaternion.identity);
            ////
            // Send SpawnEvent to all Listeners
            SpawnEvent sevt = new SpawnEvent(CreateTimeStamp(),null,this.GetComponent<InteractionMethod>(),
            Enums.GraphElementType.Node,"Input",InstantPattern);
            fireEvent(sevt);
            // End of SpawnEvent sending
            HelperFunc.AddActionToAllInteractionMethods(GO);
            
            
            GO.transform.parent = Parent.transform;
            //Übergabe aller wichtigen Informationen
            //CurrNode = GO.GetComponent<s_InstantNodeData>();
            //CurrNode = GO.GetComponent<s_VisualNode>().InstantNodeData;
            CurrNode = InstantPattern.inputNodes[i];
            CurrNode.AttributeUndWerte = InstantPattern.inputNodes[i].AttributeUndWerte;
            CurrNode.ID = InstantPattern.inputNodes[i].ID;
            CurrNode.IncomingEdges = InstantPattern.inputNodes[i].IncomingEdges;
            CurrNode.InputOutput = InstantPattern.inputNodes[i].InputOutput;
            CurrNode.NodeDescription = InstantPattern.inputNodes[i].NodeDescription;
            CurrNode.NodeDisplayName = InstantPattern.inputNodes[i].NodeDisplayName;
            CurrNode.NodeName = InstantPattern.inputNodes[i].NodeName;
            CurrNode.OutGoingEdges = InstantPattern.inputNodes[i].OutGoingEdges;
            CurrNode.PatternName = InstantPattern.inputNodes[i].PatternName;
            
            //REM
            GO.GetComponent<s_VisualNode>().InstantNodeData = CurrNode;
            //
            
            //Bezeichnung festlegen
            GO.name = CurrNode.NodeName;//+"_Node";
            //TextMesh TM = GO.GetComponent<TextMesh>();
            //TM.text = CurrNode.NodeDisplayName;
            GO.GetComponent<s_VisualNode>().SetGuiText(CurrNode.NodeDisplayName);
            //InputNode braucht ColorRed
            //GO.transform.renderer.material = SimpleRed;
            GO.GetComponent<s_VisualNode>().SetColor("Red");
            //Speichern
            //SpawnedNodes.Add (CurrNode.ID,GO);
            
            
        }
        
        ///REM
        
        /*
            TODO:
            Edges Spawnen fehlt noch, da Edges keine eigenen Objekte sind (im Moment)
        */
    }
    
    
    
    
    
    
    
    
    
    
    
    public List<string> PatternParser(string patternname, string patternpath)
    {
        XmlNodeList patterns;
        XmlNodeList outNodes;
        XmlNodeList outedges;
        XmlNodeList inNodes;
        XmlNodeList inedges; 
        
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
            
            
            if(patterns[i].SelectSingleNode("@name").InnerText == patternname )
            {
            
            string PatternName = patterns[i].SelectSingleNode("@name").InnerText;
            string PatternDisplayName = patterns[i].SelectSingleNode("@displayName").InnerText;
            string PatternDescription = patterns[i].SelectSingleNode(".//Description").InnerText;
            
            s_PatternData PatternData = new s_PatternData(PatternName,PatternDisplayName,PatternDescription);
            
            
            //Problem! inNodes inEdges etc sucht ALLE Input-Nodes und Edges!!!
            /// Lösung : Wir suchen nicht mehr nach //Input/Node sondern nach .//Input/Node wodurch nur die Knoten gesucht werden die dem Pattern zugehörig sind. GENIAL!
            inNodes = patterns[i].SelectNodes(".//Input/Node");
            inedges = patterns[i].SelectNodes(".//Input/Edge");
            outNodes = patterns[i].SelectNodes(".//Output/Node");
            outedges = patterns[i].SelectNodes(".//Output/Edge");
            
            
            
            ///Important Node
            /// The commented aspect beneath this is a point of interest for further developement on this application
            /// Some Patterns, like "PoseErrorPoseMultiplication" have Constraints that limit the combinations of Edges and Nodes
            /// Those Constraints are not implemented in this version but should be implemented in further versions by other developers.
            /// 
            /// Greets,
            /// A.L
            /*
            XmlNodeList Constraints = patterns[i].SelectNodes(".//Constraints");
            Debug.Log ("The Pattern "+PatternName+" has "+ Constraints.Count+" Constraint(s)");
            Debug.Log("It has "+Constraints[0].ChildNodes.Count+" Trigger Group(s)");
            Debug.Log ("The first trigger group has "+Constraints[0].ChildNodes[0].ChildNodes.Count+" Edge-References the application must respect");
            */
            
            //InputNodes werden gespeichert und der PatternData hinzugefügt
            
            for ( int inNodeNumber = 0; inNodeNumber < inNodes.Count; inNodeNumber++ )
            {
                string inNodeDescription="";
                string inNodeName = inNodes[inNodeNumber].SelectSingleNode("@name").InnerText;
                string inNodeDisplayName = inNodes[inNodeNumber].SelectSingleNode("@displayName").InnerText;
                if(inNodes[inNodeNumber].SelectSingleNode(".//Description") != null)
                {
                    inNodeDescription = inNodes[inNodeNumber].SelectSingleNode(".//Description").InnerText;
                }
                //Wir gehen hier davon aus, dass Nodes weder Description noch Attribute haben werden
                s_NodeData inNodeData = new s_NodeData(inNodeName,inNodeDisplayName,inNodeDescription,PatternName);
                inNodeData.InputOutput = "Input";
                
                PatternData.inputNodes.Add (inNodeData);
                
                
            }
            
            
            //Input Edges speichern
            for ( int inEdgeNumber = 0; inEdgeNumber < inedges.Count; inEdgeNumber++ )
            {
                string inEdgeDescription = "";
                
                string inEdgeName = inedges[inEdgeNumber].SelectSingleNode("@name").InnerText;
                string inEdgeDisplayName = inedges[inEdgeNumber].SelectSingleNode("@displayName").InnerText;
                if(inedges[inEdgeNumber].SelectSingleNode(".//Description")!=null)
                {
                    inEdgeDescription = inedges[inEdgeNumber].SelectSingleNode(".//Description").InnerText;
                }
                string source = inedges[inEdgeNumber].SelectSingleNode("@source").InnerText;
                string destination = inedges[inEdgeNumber].SelectSingleNode("@destination").InnerText;
                Dictionary <string,string> inEdgeAttributeUndWerte = new Dictionary<string,string>();
                
                XmlNodeList inEdgeAttribute = inedges[inEdgeNumber].SelectNodes(".//Attribute");
                
                
                for ( int AttributAnzahl = 0; AttributAnzahl < inEdgeAttribute.Count ; AttributAnzahl++ )
                {
                    
                    string keyname = inEdgeAttribute[AttributAnzahl].SelectSingleNode("@name").InnerText;
                    string value = inEdgeAttribute[AttributAnzahl].SelectSingleNode("@value").InnerText;
                    string xsi_type = inEdgeAttribute[AttributAnzahl].SelectSingleNode("@xsi:type").InnerText;
                    
                    inEdgeAttributeUndWerte.Add (keyname,value);
                    //Anm.: Jede Input-Edge hat einen XSI:Type definiert
                    inEdgeAttributeUndWerte.Add ("xsi:type",xsi_type);
                    
                    //Sucht,falls vorhanden, ein UTQL Attribut dieser Edge und fügt es an
                    if(inEdgeAttribute[AttributAnzahl].SelectSingleNode("@xmlns:utql") != null)
                    {
                        string xmlns = inEdgeAttribute[AttributAnzahl].SelectSingleNode("@xmlns:utql").InnerText;
                        inEdgeAttributeUndWerte.Add ("Xmlns:utql",xmlns);
                    }
                    
                    
                }
                
                
                // Siehe 2D6DPoseEstimation.xml. Hier haben wir eine SonderArt von Attributen. Die Predicates.
                // Die XML Rotation Difference hat aufgezeigt dass diese ebenfalls die MODE (Push Pull) Informationen tragen können
                if(inedges[inEdgeNumber].SelectNodes(".//Predicate")!=null)
                {
                    XmlNodeList inEdgePredicates = inedges[inEdgeNumber].SelectNodes (".//Predicate");
                    if(inEdgePredicates.Count > 0)
                    {
                        string Predicate = inEdgePredicates[0].InnerText;
                        Predicate = Predicate.Replace("&&","&");
                        string[] typeAndMode = Predicate.Split(new char[]{'&'},2);
                        if(typeAndMode.Length > 1)
                        {
                            string Type = typeAndMode[0].Replace("type==","");
                            string Mode = typeAndMode[1].Replace("mode==","");
                            Mode = Mode.Replace("\'","");
                            Type = Type.Replace("\'","");
                            
                            inEdgeAttributeUndWerte.Add ("type",Type);
                            inEdgeAttributeUndWerte.Add ("mode",Mode);
                            //  Debug.Log ("EdgeName "+inEdgeName+ " Added Type "+Type+" and Mode "+Mode);
                        }
                        else
                        {
                            Predicate = inEdgePredicates[0].InnerText;
                            Predicate = Predicate.Replace("type==","");
                            inEdgeAttributeUndWerte.Add ("type",Predicate);
                        }
                    }
                }
                
                
                
                
                
                //Sammeln aller Informationen in einem s_EdgeData Konstrukt
                s_EdgeData inEdge = new s_EdgeData(inEdgeName,inEdgeDisplayName,inEdgeDescription,source,destination,inEdgeAttributeUndWerte,PatternName);
                
                
                
                inEdge.InputOutput = "Input";
                //Speichern des EdgeData-Files im Pattern
                PatternData.inputEdges.Add (inEdge);
            }
            
            //Output Nodes speichern
            for ( int outNodeNumber = 0; outNodeNumber < outNodes.Count; outNodeNumber++ )
            {
                string outNodeDescription = "";
                string outNodeName = outNodes[outNodeNumber].SelectSingleNode("@name").InnerText;
                string outNodeDisplayName = outNodes[outNodeNumber].SelectSingleNode("@displayName").InnerText;
                //Wir gehen hier davon aus, dass Nodes weder Description noch Attribute haben werden
                if(outNodes[outNodeNumber].SelectSingleNode(".//Description") != null)
                {
                    outNodeDescription = outNodes[outNodeNumber].SelectSingleNode(".//Description").InnerText;
                }
                s_NodeData outNodeData = new s_NodeData(outNodeName,outNodeDisplayName,outNodeDescription,PatternName);
                outNodeData.InputOutput = "Output";
                
                PatternData.outputNodes.Add (outNodeData);
                
                
            }
            
            //Output Edges speichern
            for ( int outEdgeNumber = 0; outEdgeNumber < outedges.Count; outEdgeNumber++ )
            {           
                string outEdgeDescription="";
                
                string outEdgeName = outedges[outEdgeNumber].SelectSingleNode("@name").InnerText;
                string outEdgeDisplayName = outedges[outEdgeNumber].SelectSingleNode("@displayName").InnerText;
                if(outedges[outEdgeNumber].SelectSingleNode(".//Description")!=null)
                {
                    outEdgeDescription = outedges[outEdgeNumber].SelectSingleNode(".//Description").InnerText;
                }
                string source = outedges[outEdgeNumber].SelectSingleNode("@source").InnerText;
                string destination = outedges[outEdgeNumber].SelectSingleNode("@destination").InnerText;
                Dictionary <string,string> outEdgeAttributeUndWerte = new Dictionary<string,string>();
                
                XmlNodeList outEdgeAttribute = outedges[outEdgeNumber].SelectNodes(".//Attribute");
                
                
                for ( int AttributAnzahl = 0; AttributAnzahl < outEdgeAttribute.Count ; AttributAnzahl++ )
                {
                    
                    string keyname = "";
                    string value = "";
                    keyname = outEdgeAttribute[AttributAnzahl].SelectSingleNode("@name").InnerText;
                    
                    if(outEdgeAttribute[AttributAnzahl].SelectSingleNode("@value")!=null)
                    {
                        value = outEdgeAttribute[AttributAnzahl].SelectSingleNode("@value").InnerText;
                    }
                    
                    outEdgeAttributeUndWerte.Add (keyname,value);
                    
                    // Anm.: Nicht jede OutPut-Edge hat auch einen xsi:type definiert.
                    
                    if(outEdgeAttribute[AttributAnzahl].SelectSingleNode("@xsi:type") != null)
                    {
                        string xsi_type = outEdgeAttribute[AttributAnzahl].SelectSingleNode("@xsi:type").InnerText;
                        outEdgeAttributeUndWerte.Add ("xsi:type",xsi_type);
                    }
                    
                    //Sucht,falls vorhanden, ein UTQL Attribut dieser Edge und fügt es an
                    if(outEdgeAttribute[AttributAnzahl].SelectSingleNode("@xmlns:utql") != null)
                    {
                        string xmlns = outEdgeAttribute[AttributAnzahl].SelectSingleNode("@xmlns:utql").InnerText;
                        outEdgeAttributeUndWerte.Add ("Xmlns:utql",xmlns);
                    }
                    
                    
                    
                    
                    
                    
                    
                    
                }
                
                // Siehe 2D6DPoseEstimation.xml. Hier haben wir eine SonderArt von Attributen. Die Predicates.
                if(outedges[outEdgeNumber].SelectNodes(".//Predicate")!=null)
                {
                    XmlNodeList outEdgePredicates = outedges[outEdgeNumber].SelectNodes (".//Predicate");
                    if(outEdgePredicates.Count > 0)
                    {
                        /* Old
                        string Predicate = outEdgePredicates[0].InnerText;
                        Predicate = Predicate.Replace("type==","");
                        outEdgeAttributeUndWerte.Add ("type",Predicate);
                        */
                        
                        string Predicate = outEdgePredicates[0].InnerText;
                        Predicate = Predicate.Replace("&&","&");
                        string[] typeAndMode = Predicate.Split(new char[]{'&'},2);
                        if(typeAndMode.Length > 1)
                        {
                            string Type = typeAndMode[0].Replace("type==","");
                            string Mode = typeAndMode[1].Replace("mode==","");
                            Type = Type.Replace("\'","");
                            Mode = Mode.Replace("\'","");
                            
                            outEdgeAttributeUndWerte.Add ("type",Type);
                            outEdgeAttributeUndWerte.Add ("mode",Mode);
                            //Debug.Log ("EdgeName "+outEdgeName+ " Added Type "+Type+" and Mode "+Mode);
                        }
                        else
                        {
                            Predicate = outEdgePredicates[0].InnerText;
                            Predicate = Predicate.Replace("type==","");
                            outEdgeAttributeUndWerte.Add ("type",Predicate);
                        }
                        
                        
                    }
                }
                
                
                
                //Sammeln aller Informationen in einem s_EdgeData Konstrukt
                s_EdgeData outEdge = new s_EdgeData(outEdgeName,outEdgeDisplayName,outEdgeDescription,source,destination,outEdgeAttributeUndWerte,PatternName);
                outEdge.InputOutput = "Output";
                //Speichern des EdgeData-Files im Pattern
                PatternData.outputEdges.Add (outEdge);
            }
            //Sichern der gesammelten Informationen als Pattern in unserer Liste Patterns
            //          Debug.Log ("Pattern "+PatternName+" eingelesen");
            
            // Befüllt die Incoming- und OutGoing- Edges jedes Knotens.
            UpdateEdgeConnections(PatternData);
            // Fügt die Pattern der GesamtListe hinzu
            //if(!Patterns.ContainsKey(PatternName))
            //{
            InstantiatePattern(PatternName,PatternData);
        }}
        
        
        
        return FoundPatterns;
        
    }
    
    
    public void UpdateEdgeConnections(s_PatternData PatternData)
    {
        // EXTREM EMPFINDLICHER BEREICH:  Gemäß dem erhaltenen Grundplan des UTQL-Parsers MUSS jede NODE genau wissen welche EDGES zu ihr, und welche von ihr weg führen!
        // Dies wird hier in einigen Schritten erledigt
        
        
        foreach ( s_EdgeData EDGE in PatternData.inputEdges )
        {
            string Source = EDGE.source;
            string Destination = EDGE.destination;
            
            foreach( s_NodeData NODE in PatternData.inputNodes )
            {
                
                if( NODE.NodeName == Source )
                {
                    NODE.OutGoingEdges.Add (EDGE);
                }
                if( NODE.NodeName == Destination )
                {
                    NODE.IncomingEdges.Add (EDGE);
                }
                
            }
            
            
            foreach( s_NodeData NODE in PatternData.outputNodes )
            {
                if( NODE.NodeName == Source )
                {
                    NODE.OutGoingEdges.Add (EDGE);
                }
                if( NODE.NodeName == Destination )
                {
                    NODE.IncomingEdges.Add (EDGE);
                }
            }
            
            
        }
        
        foreach ( s_EdgeData EDGE in PatternData.outputEdges )
        {
            string Source = EDGE.source;
            string Destination = EDGE.destination;
            
            
            foreach( s_NodeData NODE in PatternData.inputNodes )
            {
                if( NODE.NodeName == Source )
                {
                    NODE.OutGoingEdges.Add (EDGE);
                }
                if( NODE.NodeName == Destination )
                {
                    NODE.IncomingEdges.Add (EDGE);
                }
            }
            
            foreach( s_NodeData NODE in PatternData.outputNodes )
            {
                if( NODE.NodeName == Source )
                {
                    NODE.OutGoingEdges.Add (EDGE);
                }
                if( NODE.NodeName == Destination )
                {
                    NODE.IncomingEdges.Add (EDGE);
                }
            }
        }
        
        //
    }
}
