﻿/*
 * 
 * GraphControl: 
 * Tries to map all points/gameobjects in function curve (time-domain)
 * Manages functions mathematically(?) and the shape of curve (sine, square, triangle etc)
 * 
 * All points (gameobjects) comes from an array. (manual as of now)
 * They are vectors with info on location in 3d-space (x,y,z)
 * X = phase shift of function
 * Y = vertical shift of function
 * Z = n/a - maybe could implement zoom for camera to go closer to curve
 * 
 * 
 * 
 * Future: maybe ArrayList for integrating array values better
 * Comments:
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphControl : MonoBehaviour {
    
    public float amplitudeFactor; //To increase volume you multiply by value, not used yet.
    //public int xRange = 10; //Array range + numbers of gameObjects on X axis..
    //What is the difference between these??
    public int xLength = 10; // Gameobjects to create in Unity

    public float pointSpacing = 0.25f;
        
    public GameObject xPoint;
    private GameObject testWavePrefab;

    List<GameObject> pointsList = new List<GameObject>();
   

    /*
    public void PrintInitialValue() //Run on start to print initial values
    {
             foreach (GameObject value in graphObjects) //how does this work?
            {
                print(graphObjects[vectorPrint].transform.position); //Print position of object in vector3
            vectorPrint++; //to itterate through objects in array
            }
    }*/

   

    public void randomChange() //Puts in random values across all Gameobjects
    {
        Vector3 temp;        //pointsList.Clear();
        for(int i = 0; i<pointsList.Count; i++) //Loop goes through each object of array and change Y value with random factor
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
        //createAndInstantiatePoints();

        for (int i = 0; i < pointsList.Count; i++) //go through obj's in pointslist
        {
            print(pointsList[i]); //Print position of objects to console
            
        }
    }

    /*
    public void graphSize(int Size, ref GameObject[] Group) //First argument is the array size you want, 2. is the array that you want to pass into it to change
    {
        GameObject[] temp = new GameObject[Size];
        for(int c=0; c<Mathf.Min(Size, Group.Length); c++)
        {
            temp[c] = Group[c]; //temp variable to store array values
        }
        Group = temp; //Back to group which is send back into main

    }
    */

    public void createAndInstantiatePoints()
    {
        Transform parentWave = InstantiatePointsOutline(10f).transform;
        Vector3 pointVec = new Vector3(parentWave.position.x, parentWave.position.y, parentWave.position.z-(xLength/2));
        for (int i = 0; i < xLength; i++)
        {
            //This for loop adds GameObjects to pointsList 
            //and instantiate the points in world space with each their own values
            
            pointsList.Add( (GameObject)Instantiate(xPoint, pointVec, Quaternion.identity, parentWave));
            pointVec.z += pointSpacing;
        }
    }

    public GameObject InstantiatePointsOutline(float var)
    {
        //parameter var is the distance needed to be between each point - ie. how long should the plane be?
        GameObject planeOutline = (GameObject)Instantiate(testWavePrefab) as GameObject;
        
        return planeOutline;
    }


    void Start() 
    {
        //Debug.Log("Initial Array size: "+ pointsList.Count);
        testWavePrefab = (GameObject)Resources.Load("testWaveOutline", typeof(GameObject));

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

