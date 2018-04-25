

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphControl : MonoBehaviour
{

    [Range(10, 100)] //this makes the int a slider in the inspector
    public int xLength = 10; // Gameobjects to create in Unity
    public bool isCurveAnimated = false;


    //public float pointSpacing = 1f; //the space in between each point

    public GameObject xPoint; //prefab from which all points are made
    private GameObject wavePrefab; //prefab form which the waveoutline is made
    public GameObject graphHolder;
    public float amplitude = 1;
    public float frequency = 2;


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
        for (int i = 0; i < pointsList.Count; i++) //Loop goes through each object of list and change Y value with random factor
        {
            if (pointsList[i] != null)
            {

                temp = pointsList[i].transform.position;
                temp.y *= (Random.Range(-1.0f, 1.0f)); //access and change y value
                pointsList[i].transform.position = temp;

            }
            else
            {
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
        wavePositionParent.transform.rotation = Quaternion.Euler(0, 90, 0);
        wavePositionParent.transform.position = new Vector3(0, 0, 0.2f);
        Presets presets = new Presets();

        GameObject waveOutline = (GameObject)Instantiate(wavePrefab, wavePositionParent.transform.localPosition, wavePositionParent.transform.localRotation, wavePositionParent.transform) as GameObject;//instantiating the pink outline arround points

        float step = 2f / xLength;
        Vector3 scale = Vector3.one * step;//all cube points are instantiated between -1 and 1
        Vector3 pointVec;//vector needed for the loop
        pointVec.z = 0;//z is not needed
<<<<<<< HEAD
        pointVec.x = 0;
        pointVec.y = 0;

=======
        pointVec.y = 0;
>>>>>>> master
        for (int i = 0; i < xLength; i++)//for-loop instantiating points and adding points to the gameobject points list
        {
            GameObject point;//this is only for reference
            pointsList.Add(point = (GameObject)Instantiate(xPoint, wavePositionParent.transform.localPosition, Quaternion.Euler(0, 90, 0)) as GameObject);
            //adding and instantiating points from the xPoint prefab at 90 degrees so it advances along the pink outline
<<<<<<< HEAD
            pointVec.x = (i + 0.5f) * step - 1f;//
            //pointVec.y = pointVec.x * pointVec.x;//change this to change y of cubes
            //pointVec.y = presets.squareWave(pointVec.x, frequency, amplitude);
            pointVec.y = presets.sawTooth(pointVec.x, frequency, amplitude);
            //pointVec.y = presets.sine(pointVec.x, frequency, amplitude);

=======
            pointVec.x = (i + 0.5f) * step - 1f;//dont change this
            pointVec.y = Mathf.Sin(Mathf.PI * pointVec.x);
>>>>>>> master
            point.transform.localPosition = pointVec;
            point.transform.localScale = scale;
            point.transform.SetParent(wavePositionParent.transform, true);
            //setting the parent at the end, and stating "true" in order to let the points keep their values

        }
        //ScalePointsOutline((pointSpacing + xPoint.transform.localScale.x) * xLength, waveOutline);
        //keeping the above function here but might not be needed in the future
    }

    public void ScalePointsOutline(float waveWidth, GameObject wave)//functions that scales the waveoutline 
    {
        //parameter waveWidth is the distance needed to be between each point - ie. how long should the plane be?
        //Vector3 temp = wave.transform.localScale;
        //temp.z = waveWidth;
        //temp.y *= 3;
        //wave.transform.localScale = temp;
        //wave.transform.localScale = scale;
        //print("planeWidth: "+ pointSpacing + " + " + xPoint.transform.localScale.z + " * " + xLength + " = " + waveWidth);

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
            print("list length: " + pointsList.Count);
            for (int i = 0; i < pointsList.Count; i++) { print(" pos of number " + i + " of the list: " + pointsList[i].transform.position); }
        }

        //Toggle animation
        if (isCurveAnimated)
        {
            for (int i = 0; i < pointsList.Count; i++)
            {
                //using localPosition in this script fucks it up... I have no idea why, i'll look into it at some point (tobi)
                Vector3 position = pointsList[i].transform.position;
                position.y = Mathf.Sin(Mathf.PI * (position.x + Time.time));
                pointsList[i].transform.position = position;
            }
        }
    }
}

