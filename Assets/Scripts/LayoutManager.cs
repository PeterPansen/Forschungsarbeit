using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LayoutManager : MonoBehaviour {

    s_Settings Einstellungen;
    
    float MinX;
    float MaxX;
    float MinY;
    float MaxY;
    float MinZ;
    float MaxZ;
    float Z;
    
    //Gibt die Abweichungsgröße eines Midpoints nach oben/unten und links/rechts an
    float Abweichung;
    //Zielabstand zwischen Nodes und Edges desselben Patterns
    float BumpRadius;
    
    //Länge und Breite des Spawngebiets
    float WidthOfSpawn = 0;
    float HeightOfSpawn = 0;
    float DepthOfSpawn = 0;
    
    List<Vector3> SpaceSensors;
    
    List<int> UsedSlots;
    
    GameObject TestPrefab;
    
    
	// Use this for initialization
	void Start () {

        TestPrefab = (GameObject) Resources.Load("Test") as GameObject;
    
        Einstellungen = GameObject.Find("Settings").GetComponent<s_Settings>();
        
        SpaceSensors = new List<Vector3>();
        UsedSlots = new List<int>();
        
        
        MinX = Einstellungen.SpawnField_MinX;
        MaxX = Einstellungen.SpawnField_MaxX;
        MinY = Einstellungen.SpawnField_MinY;
        MaxY = Einstellungen.SpawnField_MaxY;
        MinZ = Einstellungen.SpawnField_MinZ;
        MaxZ = Einstellungen.SpawnField_MaxZ;
        Z = 0;
        //Abweichung = Einstellungen.Abweichung;
        //BumpRadius = Einstellungen.BumpRadius;
        
        
        
        //Berechnet das Spawnfeld für uns
        WidthOfSpawn = Mathf.Abs (MaxX-MinX);
        HeightOfSpawn = Mathf.Abs (MaxY-MinY);
        DepthOfSpawn = Mathf.Abs (MaxZ-MinZ);
        BumpRadius = Einstellungen.BumpRadius;
        //      Debug.Log ("Weite "+WidthOfSpawn+" Höhe "+HeightOfSpawn);
        
        CreateSpawnSensorNetwork(Einstellungen.SpawnField_XPrecision,Einstellungen.SpawnField_YPrecision, Einstellungen.SpawnField_ZPrecision);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void CreateSpawnSensorNetwork(int XPoints,int YRows, int ZRows)
    {
        
        //Gibt an um wieviel wir einen Punkt nach rechts bewegen um alle Punkte 
        //in den SpawnRaum hineinzubringen
        float XZuwachs = WidthOfSpawn/XPoints;
        
        //Anmerkung:
        //YRows gibt an wieviele Testreihen wir mit XPoints einrichten
        
        //Gibt an um wieviel jede Reihe nach oben wächst
        float YZuwachs = HeightOfSpawn/YRows;
        
        float ZZuwachs = DepthOfSpawn/ZRows;
        
        for( int j = 0; j < YRows; j++ )
        {
            
            for( int i = 0; i < XPoints; i++ )
            {
                //float xPos = MinX + (i*XZuwachs);
                //float yPos = MinY + (j*YZuwachs);
                //float zPos = MinZ + (
                
                //SpaceSensors.Add (new Vector3(xPos,yPos,zPos));
                for( int k = 0; k < ZRows; k++ )
                {
                    float xPos = MinX + (i*XZuwachs);
                    float yPos = MinY + (j*YZuwachs);
                    float zPos = MinZ + (k*ZZuwachs);
                    SpaceSensors.Add(new Vector3(xPos,yPos,zPos));
                    
                    if(Einstellungen.ShowSensorNetwork)
                    {
                        Instantiate(TestPrefab,new Vector3(xPos,yPos,zPos),Quaternion.identity);
                    }
                }
                
            }
            
        }
        
          //Debug.Log ("We have "+SpaceSensors.Count+" SpaceSensors");
        
        
    }
    
    
    
    /// <summary>
    /// Durchsucht das SpawnNetwork nach der besten Spawnposition und liefert sie als Vector3 zurück
    /// </summary>
    /// <returns>The spawn position.</returns>
    public Vector3 CalcSpawnPosition()
    {
        int[] ObstaclesPerSensor = new int[SpaceSensors.Count];
        
        //Zählt die kleinste Zahl an Obstacles
        int minObstacles = 99;
        int minIndex = 99;
        
        foreach(Vector3 Sensor in SpaceSensors)
        {
            
            //Zählt die aktuellen Hindernisse der Auswahl
            int Obstacles = 0;
            List<Collider> SurroundingElements = new List<Collider>(Physics.OverlapSphere(Sensor,BumpRadius*2));
            
            foreach(Collider Col in SurroundingElements)
            {
                //Ist ein Objekt Knoten oder Kante...
                if(Col.gameObject.tag == "Node" || Col.gameObject.tag == "Edge" || Col.gameObject.tag == "Clickable")
                {
                    //...ist es ein Hindernis
                    Obstacles++;        
                }
            }
            ObstaclesPerSensor[SpaceSensors.IndexOf(Sensor)] = Obstacles;
            
            //Die UND Bedingung verhindert dass zwei Mal der selbe Slot genutzt wird
            if(Obstacles < minObstacles && (!UsedSlots.Contains(SpaceSensors.IndexOf(Sensor))))
            {
                minObstacles = Obstacles;
                minIndex = SpaceSensors.IndexOf(Sensor);
            }
            
            
        }
        
        //Debug.Log ("The lowest amount of obstacles is "+minObstacles+" at the Index "+minIndex);
        
        if(minIndex==99)
        {
            return new Vector3(Random.Range (MinX,MaxX),Random.Range (MinY,MaxY),Z);
        }
        
        //UsedSlots erhält den Index des nächsten belegten Sensors
        UsedSlots.Add (minIndex);
        
        return SpaceSensors[minIndex];
        
    }
    
    
    /// <summary>
    /// Überprüft die belegten SpaceSensors auf mögliche Freigabe.
    /// Sollte nach jeder GraphElement-Löschung gestartet werden
    /// </summary>
    public void ClearSlots()
    {
        
        for( int i = 0; i < UsedSlots.Count-1; i++ )
        {
            Debug.Log ("I "+i+" UsedSlotsCount "+UsedSlots.Count+" SpaceSensorCount "+SpaceSensors.Count);
            if(UsedSlots.Count > i && SpaceSensors.Count > UsedSlots[i])
            { 
                Vector3 Sensor = SpaceSensors[UsedSlots[i]];
                List<Collider> SurroundingElements = new List<Collider>(Physics.OverlapSphere(Sensor,BumpRadius*2));
                List<Collider> FixedSurroundingElements = new List<Collider>();
                
                foreach(Collider Anything in SurroundingElements)
                {
                    if (Anything.tag == "Node" || Anything.tag == "Edge")
                    {
                        //Enthält nur Knoten und Kanten in der Nähe.Keine anderen Objekte
                        FixedSurroundingElements.Add (Anything);
                    }
                }
                
                
                if(FixedSurroundingElements.Count == 0)
                {
                    Debug.Log ("Keine Hindernisse an Slot "+UsedSlots[i]+" gefunden. Der Slot wird freigegeben");
                    //Ein INT-List kann nicht auf null gesetzt werden, deswegen setzen wir es auf einen unbedeutenden Wert
                    UsedSlots[i] = SpaceSensors.Count+1;
                }
                else
                {
                    Debug.Log ("Slot "+UsedSlots[i]+" ist derzeit noch von "+FixedSurroundingElements.Count+" Hindernissen umgeben.Freischaltung nicht möglich");
                    
                }
            }
        }
        
    }
}
