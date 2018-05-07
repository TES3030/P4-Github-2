using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphsManager : MonoBehaviour {

    public GameObject GraphHolder; //Prefab form which all grapholders are made.
   
    [Range(10, 100)] //This makes xLength a slider in the inspector.
    public int xLengthForNextGraph = 80; // Amount of Gameobjects to create to represent a curve in Unity.

    private float amplitude = 1;//Amplitude of waveform. 
    private float frequency = 2;//Frequency of waveform. 

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown("backspace"))
        {
            print("Backspace key was pressed");
            createGraph();
        }
    }

    void createGraph()
    {
        //instantiate
        //sendmessage
        GameObject GraphHolderParent = (GameObject)Instantiate(GraphHolder, GraphHolder.transform.position, GraphHolder.transform.rotation) as GameObject;//instanstiating an object of graphholder for each graph
        //GraphHolderParent.transform.rotation = Quaternion.Euler(0, 90, 0);
        //GraphHolderParent.transform.position = new Vector3(0, 0, 0.2f);

        //GraphHolderParent.SendMessage("setXLength", xLengthForNextGraph);//Data needed to be sent is: GraphHolderParent.transform, xLength, 
        //GraphHolderParent.SendMessage("createAndInstantiatePoints", GraphHolderParent.transform);//Data needed to be sent is: GraphHolderParent.transform, xLength, 

        //GameObject waveOutline = (GameObject)Instantiate(wavePrefab, GraphHolderParent.transform.localPosition, GraphHolderParent.transform.localRotation, GraphHolderParent.transform) as GameObject;//instantiating the pink outline arround points


    }

}
