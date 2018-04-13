/*
 * 
 * GraphControl: 
 * Tries to map all points/gameobjects in function curve (time-domain)
 * All points (gameobjects) has a vector with info on location in 3d-space (x,y,z)
 * X = phase shift of function
 * Y = vertical shift of function
 * Z = n/a - maybe could affect some sort of effect such as reverb
 * 
 * 
 * 
 * Future:
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
        
    public GameObject xPoint;

    List<PointsObjects> pointsList = new List<PointsObjects>();//not completely sure if this is correct
    List<GameObject> pointsGOList = new List<GameObject>();
        
    //private GameObject[] graphObjects; //Old variable not used anymore..
    //private GameObject[] pointsList;
    //public GameObject[] pointCount = new GameObject[100]; //Maximum size of array of X objects


    /*
    public void PrintInitialValue() //Run on start to print initial values
    {
             foreach (GameObject value in graphObjects) //how does this work?
            {
                print(graphObjects[vectorPrint].transform.position); //Print position of object in vector3
            vectorPrint++; //to itterate through objects in array
            }
    }*/

    /*
      public void sineCurve()
    {

        foreach (GameObject value in pointsList)
        {

        }
    }
    */


    public void randomChange() //Puts in random values across all Gameobjects
    {
        //int i = 0;
        //pointsList.Clear();
        for(int i = 0; i<pointsList.Count; i++) //Loop goes through each object of array and change Y value with random factor
        {
            if (pointsList[i] != null)
            {
                /*
                float temp = 0;
                temp * (Random.Range(-1.0f, 1.0f)); //access and change y value
                pointsList[i].zValue = temp;
                */
                pointsList[i].yValue *= (Random.Range(-1.0f, 1.0f)); //access and change y value
                instantiatePoints();
            }
            else {
            Debug.Log("obj is null");
        }
    }
        createPoints();

        for(int i = 0; i < pointsList.Count; i++) //go through obj's in pointslist
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

    public void createPoints()
    {
        float z = 0;
        for (int i = 0; i < xLength; i++)
        {
            //This for loop adds pointsObjects to pointsList 
            //and instantiate the points in world space with each their own values

            pointsList.Add(new PointsObjects(new Vector3(0, 0, z)));
            z += 1; //Amount of increment between points!
        }

    }

    public void instantiatePoints() { 
        foreach (PointsObjects obj in pointsList)
        {
            GameObject pointsGO = Instantiate(xPoint, new Vector3(obj.xValue, obj.yValue, obj.zValue), Quaternion.identity) as GameObject;//swap prefab with something
            pointsGOList.Add(pointsGO);
            
        }

    }

    void Start() 
    {

        //Debug.Log("Initial Array size: "+ pointsList.Count);

    }

    
    void Update()
        {

        if (Input.GetKeyDown("backspace"))
        {
            print("Backspace key was pressed");
            xLength = xRange;//Come back to why 2 variables are important again?

            createPoints();
            instantiatePoints();

            //GraphHolder.SendMessage("createPoints", xLength); //number of points are created according to specified xRange
            //GameObject[] pointsList = new GameObject[xRange]; //Feed variable here to determine ArraySize
            //createPoints();

            //Debug.Log("Current Array Size:"+ this.pointsList.Length);
        }

           if (Input.GetKeyDown("space")) { 
                print("Space key was pressed");
                 randomChange();
        }  
    }
}

