using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class s_VisualNode : MonoBehaviour {


    public s_InstantNodeData InstantNodeData;
    Color TypeColor;

    //s_InstantNodeData IND
    Canvas NodeDesc;

    Material Normal;
    Material Selected;
    Material ActiveSelection;

    List<SpringJoint> Springs = new List<SpringJoint>();

    s_Settings Einstellungen;

    bool SpringCoolerCounting = false;



	// Use this for initialization
	void Start () {

        NodeDesc = this.GetComponentInChildren<Canvas>();
        NodeDesc.enabled = false;
        Einstellungen = GameObject.Find("Settings").GetComponent<s_Settings>();
        this.Normal = Einstellungen.NodeNormal;
        this.Selected = Einstellungen.NodeSelected;
        this.ActiveSelection = Einstellungen.NodeActiveSelection;
        this.GetComponent<Rigidbody>().mass = Einstellungen.NodeMass;




	}
	
	// Update is called once per frame
	void Update () {


	}

    void FixedUpdate()
    {
        //Fixes the damping problem
        StopJigglingAfterTime();
        

    }

    /// <summary>
    /// Changes the GUI-Text of a Node
    /// </summary>
    /// <param name="abc">Abc.</param>
    public void SetGuiText(string abc)
    {
        GetComponentInChildren<Text>().text = abc;
    }
    
    public void SetColor (string col)
    {
        switch(col)
        {
        case "Red":
            this.GetComponent<Renderer>().material.color = Color.red;
            TypeColor = this.GetComponent<Renderer>().material.color;
            break;
        case "Blue":
            this.GetComponent<Renderer>().material.color = Color.blue;
            TypeColor = this.GetComponent<Renderer>().material.color;
            break;
        }
    }

    /// <summary>
    /// Shows the description-GUI arround a Node
    /// </summary>
    /// <param name="Swit">If set to <c>true</c> swit.</param>
    public void ShowDesc(bool Swit)
    {
    	if(NodeDesc!=null)
    	{
	        if (Swit == true)
	        {
	            NodeDesc.enabled = true;
	        } else
	        {
	            NodeDesc.enabled = false;
	        }
        }
    }
    /// <summary>
    /// Toggles the desc.
    /// Meaning: It enables/disables the NodeDesc-GUI hovering around it
    /// </summary>
    public void ToggleDesc()
    {
        if (NodeDesc.enabled == true)
        {
            NodeDesc.enabled = false;
        } else
        {
            NodeDesc.enabled = true;
        }
    }

    /// <summary>
    /// Returns true if the description is enabled, else it returns false
    /// </summary>
    /// <returns><c>true</c>, if desc state was gotten, <c>false</c> otherwise.</returns>
    public bool GetDescState()
    {
        if(NodeDesc.enabled == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// Switchs the material.
    /// Enter "Normal" for the default Material (white) and "Selected" for the outlined Material
    /// New Feature: Enter "Toggle" for a simple switch between the two
    /// </summary>
    /// <param name="State">State.</param>
    public void SwitchMaterial(string State)
    {
    
        if (State == "Normal")
        {
            this.GetComponent<Renderer>().material = Normal;
            this.GetComponent<Renderer>().material.color = TypeColor;
        }

        if (State == "Selected")
        {
            this.GetComponent<Renderer>().material = Selected;
            this.GetComponent<Renderer>().material.color = TypeColor;
        }
        
        if (State == "ActiveSelection")
        {
            this.GetComponent<Renderer>().material = ActiveSelection;
            this.GetComponent<Renderer>().material.color = TypeColor;
        }
        
        if (State == "Toggle")
        {
            //Selected zu Normal
            if(this.GetComponent<Renderer>().material.name.Contains("Selected"))
            {
                this.GetComponent<Renderer>().material = Normal;
                this.GetComponent<Renderer>().material.color = TypeColor;
            }
            //Normal zu Selected
            else if(this.GetComponent<Renderer>().material == Normal)
            {
                this.GetComponent<Renderer>().material = Selected;
                this.GetComponent<Renderer>().material.color = TypeColor;
            }
            //Am Anfang ist es keins von beiden. In dem Fall zu Selected wechseln
            else
            {
                this.GetComponent<Renderer>().material = Selected;
                this.GetComponent<Renderer>().material.color = TypeColor;
            }
        }
    }

    /// <summary>
    /// Adds a SpringJoint to this rigidbody connecting it with another.
    /// If the other object does NOT have a rigidbody we return an error.
    /// </summary>
    /// <param name="Connected">Connected.</param>
    public void AddSpring(GameObject Connected)
    {
         Rigidbody Temp;

        if (Connected.GetComponent<Rigidbody>() != null)
        {
            Temp = Connected.GetComponent<Rigidbody>();
            SpringJoint NeuSpring = this.transform.gameObject.AddComponent<SpringJoint>() as SpringJoint;
            NeuSpring.connectedBody = Temp;

            NeuSpring.spring = Einstellungen.GlobalSpring_SpringForce;
            NeuSpring.damper = Einstellungen.GlobalSpring_SpringDamper;
            NeuSpring.minDistance = Einstellungen.GlobalSpring_MinDistance;
            NeuSpring.maxDistance = Einstellungen.GlobalSpring_MaxDistance;

            Springs.Add(NeuSpring);
        } 

        else
        {
            Debug.Log("Cannot connect "+this.transform.name+" to "+Connected.name+" because the second has no rigidbody");
            return;
        }
    }

    /// <summary>
    /// Stops the springs and stops all current force on this Node.
    /// Keep in Mind: Spring Joints can NOT be disabled! They get deleted!
    /// </summary>
    public void StopSprings()
    {
            foreach(SpringJoint Spring in Springs)
            {
                Destroy(Spring);
            }
        this.GetComponent<Rigidbody>().Sleep();
        Springs.Clear();
    }

    /// <summary>
    /// Sets the position of this node
    /// </summary>
    /// <param name="nuPos">Nu position.</param>
    public void SetPos(Vector3 nuPos)
    {
        this.transform.position = new Vector3(nuPos.x, nuPos.y, nuPos.z);
    }

    /// <summary>
    /// Gets the position of this node
    /// </summary>
    /// <returns>The position.</returns>
    public Vector3 GetPos()
    {
        return this.transform.position;
    }

    /// <summary>
    /// Destroys this very handsome node. So sad!
    /// </summary>
    public void DestroyNode()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Reacts if the rigidbody is moved (due to springs) and stops this after a certain amount of time
    /// This amount can be set in "Settings"-"Spring Time"
    /// </summary>
    void StopJigglingAfterTime()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude > 0)
        {
            //Debug.Log("Moving it "+Time.time);
            if(SpringCoolerCounting == false)
            {
                StartCoroutine(SpringCooler(Einstellungen.SpringTime));
            }
        }
    }


    IEnumerator SpringCooler(float waitTime) {
        SpringCoolerCounting = true;
        yield return new WaitForSeconds(waitTime);
        this.GetComponent<Rigidbody>().Sleep();
        SpringCoolerCounting = false;
    }
    



}
