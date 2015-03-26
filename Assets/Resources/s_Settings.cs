using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class s_Settings : MonoBehaviour {

    public Camera MainCam;
    public Transform MainCam_Parent;
    public Transform CameraRot_CenterPointPosition;
    public float Camera_Rotation_Speed = 50.0f;
    public float CameraScrollSpeed = 100.0f;
    public float Click_Hold_Timer = 0.2f;
    public Material NodeNormal;
    public Material NodeSelected;
    public Material NodeActiveSelection;
    public float NodeMass = 1.0f;

    //SpringJointSettings
    public float GlobalSpring_SpringForce = 4.0f;
    public float GlobalSpring_SpringDamper = 1.0f;
    public float GlobalSpring_MinDistance = 1.0f;
    public float GlobalSpring_MaxDistance = 10.0f;
    public float SpringTime = 5.0f;

    //Prefabs for the Spawner
    public GameObject NodePrefab;
    public GameObject LR_Prefab;
    
    public Transform InteraktionsMethodenParent;

    //Pfade 
    public string UbiTrackPath = "C:/libraries/UbiTrack/UbiTrack/doc/utql";
    //Geschätzte Anzahl an XML Files und Patterns für Fortschrittsanzeige
    public int PatternsInRoot = 454;
    public int XMLFilesInRoot = 102;
    
    
    //Angaben für das SpawnFeld
    public float SpawnField_MinX = -4.5f;
    public float SpawnField_MaxX = +4.5f;
    public float SpawnField_MinY = +0.5f;
    public float SpawnField_MaxY = +4.5f;
    public float SpawnField_MinZ = -1.0f;
    public float SpawnField_MaxZ = +7.0f;
    
    public int SpawnField_XPrecision = 20;
    public int SpawnField_YPrecision = 20;
    public int SpawnField_ZPrecision = 20;
    
    public float BumpRadius = 2.0f;
    
    public bool ShowSensorNetwork = false;
    
    public string DeleteKey = "x";
	public string SaveKey = "s";
	public string LoadKey = "l";



	// Use this for initialization
	void Start () {
	
	}
    



}
