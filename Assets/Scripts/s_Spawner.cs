using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class s_Spawner : MonoBehaviour {

    GameObject NodePrefab;
    GameObject LR_Prefab;

    s_Settings Einstellungen;

    Dictionary<string,GameObject> Nodes = new Dictionary<string, GameObject>();

	// Use this for initialization
	void Start () {

        Einstellungen = GameObject.Find("Settings").GetComponent<s_Settings>();
        NodePrefab = Einstellungen.NodePrefab;
        LR_Prefab = Einstellungen.LR_Prefab;

	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log("There are " + Nodes.Count + " Nodes saved");
	
	}

    /// <summary>
    /// Spawns the desired Item. Enter name "Node" for a node and so on.
    /// The second parameter shows the desired position
    /// </summary>
    /// <param name="ItemName">Item name.</param>
    /// <param name="Pos">Position.</param>
    public void SpawnItem(string ItemType,string ItemName,Vector3 Pos)
    {
        switch (ItemType)
        {
            case "Node":
                GameObject NewNode = Instantiate(NodePrefab,Pos,Quaternion.identity) as GameObject;
                NewNode.name = ItemName;
                Nodes.Add(ItemName,NewNode);
                break;
        }
    }

    public void ConnectNodes(string N1_Name,string N2_Name)
    {
        GameObject Node1 = Nodes [N1_Name];
        GameObject Node2 = Nodes [N2_Name];

        if (Node1 == null || Node2 == null)
        {
            Debug.Log("Trying to connect two nodes "+N1_Name+" and "+N2_Name+" has failed, one of them cannot be found");
            return;
        }

        //Connect both Nodes via SpringJoints
        SpringJoint N1_SJ = Node1.AddComponent<SpringJoint>();
        SpringJoint N2_SJ = Node2.AddComponent<SpringJoint>();

        N1_SJ.connectedBody = N2_SJ.GetComponent<Rigidbody>();
        N2_SJ.connectedBody = N1_SJ.GetComponent<Rigidbody>();

        N1_SJ.damper = Einstellungen.GlobalSpring_SpringDamper;
        N1_SJ.maxDistance = Einstellungen.GlobalSpring_MaxDistance;
        N1_SJ.minDistance = Einstellungen.GlobalSpring_MinDistance;
        N1_SJ.spring = Einstellungen.GlobalSpring_SpringForce;

        N2_SJ.damper = Einstellungen.GlobalSpring_SpringDamper;
        N2_SJ.maxDistance = Einstellungen.GlobalSpring_MaxDistance;
        N2_SJ.minDistance = Einstellungen.GlobalSpring_MinDistance;
        N2_SJ.spring = Einstellungen.GlobalSpring_SpringForce;



        //Check if Node1 has LineRenderers
        LR[] N1_LRs = Node1.GetComponentsInChildren<LR>();
        //LR CurrentSkript = new LR();
        bool FoundFreeLR = false;

        foreach (LR LineSkript in N1_LRs)
        {
            if(LineSkript.Goal == null)
            {
                LineSkript.Start = Node1;
                LineSkript.Goal = Node2;
                FoundFreeLR = true;
            }
        }

        if (FoundFreeLR == false)
        {
            GameObject LR_Host = Instantiate(LR_Prefab,Node1.transform.position,Quaternion.identity) as GameObject;
            LR_Host.transform.parent = Node1.transform;
            LR_Host.GetComponent<LR>().Start = Node1;
            LR_Host.GetComponent<LR>().Goal = Node2;

        }

        //Same procedure for Node2, Bool has to get resettet
        FoundFreeLR = false;
        //Check if Node1 has LineRenderers
        LR[] N2_LRs = Node2.GetComponentsInChildren<LR>();

        foreach (LR LineSkript in N2_LRs)
        {
            if(LineSkript.Goal == null)
            {
                LineSkript.Start = Node2;
                LineSkript.Goal = Node1;
                FoundFreeLR = true;
            }
        }
        
        if (FoundFreeLR == false)
        {
            GameObject LR_Host = Instantiate(LR_Prefab,Node2.transform.position,Quaternion.identity) as GameObject;
            LR_Host.transform.parent = Node2.transform;
            LR_Host.GetComponent<LR>().Start = Node2;
            LR_Host.GetComponent<LR>().Goal = Node1;
            
        }


    }
}
