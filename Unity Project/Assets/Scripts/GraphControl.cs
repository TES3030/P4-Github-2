

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphControl : MonoBehaviour {
    
    public float amplitudeFactor; //This var is not used atm
    [Range(10,100)] //this makes the int a slider in the inspector
    public int xLength = 10; // Gameobjects to create in Unity

    public float pointSpacing = 1f; //the space in between each point
        
    public GameObject xPoint; //prefab from which all points are made
    private GameObject wavePrefab; //prefab form which the waveoutline is made

    List<GameObject> pointsList = new List<GameObject>(); //the list of all points in a curve

    //code not needed at the moment (NEVER DELETE CODE)
    /*
    public void PrintInitialValue() //Run on start to print initial values
    {
             foreach (GameObject value in graphObjects) //how does this work?
            {
                print(graphObjects[vectorPrint].transform.position); //Print position of object in vector3
            vectorPrint++; //to itterate through objects in array
            }
    }*/


    //Code to randomly change points values - only used for developing
    public void randomChange() //Puts in random values across all Gameobjects
    {
        Vector3 temp;      
        for(int i = 0; i<pointsList.Count; i++) //Loop goes through each object of list and change Y value with random factor
        {
            if (pointsList[i] != null)
            {
                
                temp = pointsList[i].transform.position;
                temp.y *= (Random.Range(-1.0f, 1.0f)); //access and change y value
                pointsList[i].transform.position = temp;

            }
            else {
            Debug.Log("obj is null");
        }
    }

        for (int i = 0; i < pointsList.Count; i++) //go through obj's in pointslist
        {
            print(pointsList[i]); //Print position of objects to console
            
        }
    }

    //fucntion that creates points for the lsit and instantiates them
    public void createAndInstantiatePoints()
    {
        GameObject wavePositionParent = new GameObject("WavePosition");//the empty game object containing the position of the curve/wave
        wavePositionParent.transform.rotation = Quaternion.Euler(0,90,0);
        wavePositionParent.transform.position = new Vector3(0, 0, 0.2f);
        //Vector3 tempPlayerPos = GameObject.Find("Player").transform.localPosition;//the position of the player in order to spawn curve in fornt of player
        //tempPlayerPos.x -= 10; //offsetting curve to be 10 units in x in front
        //wavePositionParent.transform.localPosition = tempPlayerPos; //changing the position of curve/wave
        GameObject waveOutline = (GameObject)Instantiate(wavePrefab, wavePositionParent.transform.localPosition, wavePositionParent.transform.localRotation, wavePositionParent.transform) as GameObject;//instantiating the pink outline arround points
        
        float step = 2f / xLength *10;
        Vector3 scale = Vector3.one * step; 
        Vector3 pointVec;
        pointVec.z = 0;
        for (int i = 0; i < xLength; i++)//for loop instantiating points and adding them to the list
        {
            GameObject point;
            pointsList.Add(point = (GameObject)Instantiate(xPoint, wavePositionParent.transform.localPosition, Quaternion.Euler(0,90,0)) as GameObject);
            //adding and instantiating points from the xPoint prefab, setting "waveOutline" as parent
            pointVec.x = (i + 0.5f) / step - 1f;
            pointVec.y = pointVec.x;
            pointVec.z = 0;
            point.transform.localPosition = pointVec;
            point.transform.localScale = scale;
            point.transform.SetParent(wavePositionParent.transform, true);

        }
        ScalePointsOutline((pointSpacing + xPoint.transform.localScale.x) * xLength, waveOutline);
    }

    public void ScalePointsOutline(float waveWidth, GameObject wave)//functions that scales the waveoutline 
    {
        //parameter waveWidth is the distance needed to be between each point - ie. how long should the plane be?
        //Vector3 temp = wave.transform.localScale;
        //temp.z = waveWidth;
        //temp.y *= 3;
        //wave.transform.localScale = temp;
        //wave.transform.localScale = scale;
        print("planeWidth: "+ pointSpacing + " + " + xPoint.transform.localScale.z + " * " + xLength + " = " + waveWidth);

    }


    void Start() 
    {
        wavePrefab = (GameObject)Resources.Load("testWaveOutline", typeof(GameObject));//loading the prefab from the resources folder in order to access its values
        //step = 2f / xLength;
        //scale = Vector3.one * step;
    }


    void Update()
        {
        
        if (Input.GetKeyDown("backspace"))
        {
            print("Backspace key was pressed");
            createAndInstantiatePoints();
            
        }

        if (Input.GetKeyDown("space"))
        {
            print("Space key was pressed");
            randomChange();
        }
        if (Input.GetKeyDown("p"))
        {
            print("printing list");
            print("list length: "+pointsList.Count);
            for (int i = 0; i < pointsList.Count; i++) { print(" pos of number " + i + " of the list: " + pointsList[i].transform.position); }
        }
    }
}

