using UnityEditor;
using System.Collections;
using UnityEngine;
using System.IO;

using FAR;

public class UbiTrackCalibFileEditor : EditorWindow
{
    string errorPoseDir = "path to error pose calib dir";
    string errorPoseFile = "path to error pose file";
    
    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/UbiTrackCalibFileEditor")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(UbiTrackCalibFileEditor));
    }

    // Add menu named "My Window" to the Window menu

    [MenuItem("Window/UbiTrackCalibFileEditor")]
    public static void Init()
    {



        //s Get existing open window or if none, make a new one:

        UbiTrackCalibFileEditor window = (UbiTrackCalibFileEditor)EditorWindow.GetWindow(typeof(UbiTrackCalibFileEditor));

        window.Show();

    }

    void OnGUI()
    {
        errorPoseDir = EditorGUILayout.TextField("ErrorPose Dir", errorPoseDir);
        if (!Directory.Exists(errorPoseDir))
        {
            EditorGUILayout.LabelField("invalid path");            
        } 
        if(GUILayout.Button("create object"))
        {
            GameObject parentObject = new GameObject();
            parentObject.name = errorPoseDir;

           
            string[] filePaths = Directory.GetFiles(errorPoseDir, "*.cal");
            foreach (string f in filePaths)
            {
                string text = System.IO.File.ReadAllText(f);
                
                Pose pose = UbiFileUtils.readErrorPoseAsPoseFromString(text);
                GameObject obj = new GameObject();
                obj.name = f.Substring(errorPoseDir.Length + 1);
                pose.setTranformValues(obj.transform);                
                obj.transform.parent = parentObject.transform;
            }
            
            
        }

        errorPoseFile = EditorGUILayout.TextField("ErrorPose File", errorPoseFile);
        if (!Directory.Exists(errorPoseFile))
        {
            EditorGUILayout.LabelField("invalid path");
        }
        if (GUILayout.Button("create single object"))
        {
            GameObject parentObject = new GameObject();
            parentObject.name = errorPoseFile;
            
            string text = System.IO.File.ReadAllText(errorPoseFile);
            Pose pose = UbiFileUtils.readErrorPoseAsPoseFromString(text);
            Quaternion qinv = Quaternion.Inverse(pose.rot);
            Vector3 posinv = -(qinv * pose.pos);
            parentObject.transform.position = posinv;
            parentObject.transform.rotation = qinv;
            //pose.setTranformValues(parentObject.transform);
            
         


        }
        /*
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);
        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();
         * */
    }
}
