using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphControl : MonoBehaviour {
    
    public float amplitudeFactor; //To increase volume you multiply by value, not used yet.
    public int xRange = 10; //Array range + numbers of gameObjects on X axis..
    //What is the difference between these??
    public int xLength = 10; // Gameobjects to create in Unity
        
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
                temp.z *= (Random.Range(-1.0f, 1.0f)); //access and change y value
                pointsList[i].transform.position = temp;

            }
            else {
            Debug.Log("obj is null");
        }
    }
        instantiatePoints();

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

    public void createPoints()
    {
        //float z = 0;
        Vector3 temp;
        xPoint.transform.position = new Vector3(0,0,0); //Amount of increment between points!

        for (int i = 0; i < xLength; i++)
        {
            //This for loop adds pointsObjects to pointsList 
            //and instantiate the points in world space with each their own values

            //GameObject obj = xPoint(xPoint.transform.position += Vector3.up);
            //pointsList.Add(obj);
            
            temp = xPoint.transform.position; //Amount of increment between points!
            temp.z += 0.5f;
            xPoint.transform.position = temp;
            

            pointsList.Add(xPoint);

        }
        instantiatePoints();
    }

    public void instantiatePoints()
    {
        for (int i = 0; i < pointsList.Count; i++)
        {
            GameObject point = Instantiate(pointsList[i]) as GameObject;
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

            createPoints();

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

