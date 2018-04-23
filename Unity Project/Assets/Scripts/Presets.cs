using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presets : MonoBehaviour {

    public float amplitude;
    public float frequency;
    public float time; 

    public float pointSpacing = 1f; //the space in between each point

    public GameObject xPoint; //prefab from which all points are made
    private GameObject wavePrefab; //prefab form which the waveoutline is made

    public int xLength; // Gameobjects to create in Unity

    List<GameObject> pointsList = new List<GameObject>();

    public void createAndInstantiatePoints()
    {
        GameObject wavePositionParent = new GameObject();//the empty game object containing the position of the curve/wave
        Vector3 tempPlayerPos = GameObject.Find("Player").transform.position;//the position of the player in order to spawn curve in fornt of player
        tempPlayerPos.x -= 10; //offsetting curve to be 10 units in x in front
        wavePositionParent.transform.position = tempPlayerPos; //changing the position of curve/wave
        GameObject waveOutline = (GameObject)Instantiate(wavePrefab, wavePositionParent.transform.position, Quaternion.identity, wavePositionParent.transform) as GameObject;//instantiating the pink outline arround points
        Vector3 pointVec = new Vector3(waveOutline.transform.position.x, waveOutline.transform.position.y, waveOutline.transform.position.z - ((pointSpacing + xPoint.transform.localScale.z) * xLength) / 2 + pointSpacing);//creating vector of the points created in the for loop below
        for (int i = 0; i < xLength; i++)//for loop instantiating points and adding them to the list
        {
            //This for loop adds GameObjects to pointsList 
            //and instantiate the points in world space with each their own values

            pointsList.Add((GameObject)Instantiate(xPoint, pointVec, Quaternion.identity, wavePositionParent.transform));//adding and instantiating points from the xPoint prefab, setting "waveOutline" as parent
            pointVec.z += pointSpacing;
        }
        ScalePointsOutline((pointSpacing + xPoint.transform.localScale.x) * xLength, waveOutline);
    }

    public void ScalePointsOutline(float waveWidth, GameObject wave)//functions that scales the waveoutline 
    {
        //parameter waveWidth is the distance needed to be between each point - ie. how long should the plane be?
        Vector3 temp = wave.transform.localScale;
        temp.z = waveWidth;
        temp.y *= 3;
        wave.transform.localScale = temp;
        print("planeWidth: " + pointSpacing + " + " + xPoint.transform.localScale.z + " * " + xLength + " = " + waveWidth);

    }

    public void squareWave()
    {
        Vector3 tempa;

        for (int i = 0; i < pointsList.Count; i++)
        {
            if (pointsList[i] != null)
            {
                //tempa = pointsList[i].get
            }
        }

        Vector3 temp;
        for (int i = 0; i < pointsList.Count; i++)
        {
            if (pointsList[i] != null)
            {
                temp = pointsList[i].transform.position;
                temp.y += Mathf.Sign(Mathf.Sin(frequency * time));
                pointsList[i].transform.position = temp;
            }

        }
    }
	// Use this for initialization
	void Start () {
        wavePrefab = (GameObject)Resources.Load("testWaveOutline", typeof(GameObject));//loading the prefab from the resources folder in order to access its values
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("backspace"))
        {
            print("Backspace key was pressed");
            createAndInstantiatePoints();

        }

        if (Input.GetKeyDown("space"))
        {
            print("Space key was pressed");

        }
        if (Input.GetKeyDown("p"))
        {
            print("printing list");
            print("list length: " + pointsList.Count);
            for (int i = 0; i < pointsList.Count; i++) { print(" pos of number " + i + " of the list: " + pointsList[i].transform.position); }
        }
	}
}
