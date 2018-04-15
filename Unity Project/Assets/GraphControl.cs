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
    public int xRange = 10; //Array range + numbers of gameObjects on X axis..
    //What is the difference between these??
    public int xLength = 10; // Gameobjects to create in Unity

    public int pointSpacing = 1;
        
    public GameObject xPoint;

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

        Vector3 pointVec = new Vector3(0, 0, 0);
        for (int i = 0; i < xLength; i++)
        {
            //This for loop adds pointsObjects to pointsList 
            //and instantiate the points in world space with each their own values
            
            pointsList.Add( (GameObject)Instantiate(xPoint, pointVec, Quaternion.identity));
            pointVec.z += pointSpacing;

        }
    }


    void Start() 
    {
        //xPoint = GameObject.Find("xPoint");
        //Debug.Log("Initial Array size: "+ pointsList.Count);

    }

    
    void Update()
        {

        if (Input.GetKeyDown("backspace"))
        {
            print("Backspace key was pressed");
            xLength = xRange;//Come back to why 2 variables are important again?

            createAndInstantiatePoints();

            //GraphHolder.SendMessage("createPoints", xLength); //number of points are created according to specified xRange
            //GameObject[] pointsList = new GameObject[xRange]; //Feed variable here to determine ArraySize
            //createPoints();

            //Debug.Log("Current Array Size:"+ this.pointsList.Length);
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

