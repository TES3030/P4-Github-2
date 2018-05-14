using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphsManager : MonoBehaviour {

    public GameObject GraphHolder; //Prefab form which all grapholders are made.
    public GameObject PresetMenuFab; //Prefab form which all grapholders are made.

    [Range(10, 100)] //This makes xLength a slider in the inspector.
    public int xLengthForNextGraph = 80; // Amount of Gameobjects to create to represent a curve in Unity.

    private float amplitude = 1;//Amplitude of waveform. 
    private float frequency = 2;//Frequency of waveform. 

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown("backspace"))
        {
            print("Backspace key was pressed");
            CreateGraph();
        }

        if (Input.GetKeyDown("o"))
        {
            print("1 key was pressed");
            SpawnPresetMenu();
        }
    }

    void CreateGraph()
    {
        GameObject GraphHolderParent = (GameObject)Instantiate(GraphHolder, GraphHolder.transform.position, GraphHolder.transform.rotation) as GameObject;//instanstiating an object of graphholder for each graph
    }

    void SpawnPresetMenu()
    {
        GameObject PresetMenu = (GameObject)Instantiate(PresetMenuFab, PresetMenuFab.transform.position, PresetMenuFab.transform.rotation) as GameObject;
    }

}
