

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphControl : MonoBehaviour
{
    Presets presets = new Presets(); //Use curve-presets from Presets script.

    [Range(10, 100)] //This makes xLength a slider in the inspector.
    public int xLength = 10; // Amount of Gameobjects to create to represent a curve in Unity.
    public bool isCurveAnimated = true; //Animation of curve on/off.
    public bool isCurveLined = true; //LineRenderer on/off.
    public bool isLowFreqMode = true; //Turn LowFrequency mode on/off. 
    public bool isCurveDotted = false; //Turn gameObjects that create curve on/off.

    public float lowFreqScaleFactor = 10; //Determine scalefactor between lowfreqmode and highfreqmode.

    public GameObject xPoint; //Prefab from which all points are made.
    private GameObject wavePrefab; //Prefab form which all waveoutlines are made.
    public float amplitude = 1;//Amplitude of waveform. 
    public float frequency = 2;//Frequency of waveform. 

    public int curvePreset = 0;//Default waveform is sine (0), Square Wave (1), Sawtooth Wave (2).

    List<GameObject> pointsList = new List<GameObject>(); //List of all points in a curve. 

    //LineRenderer colors low/high freq
    public Color lowFrequencyColor = Color.green; //Default color for Low Frequecy mode is green.
    public Color highFrequencyColor = Color.blue;//Default color for High Frequency mode is blue.






    //Code to randomly change points values - only used for developing.
    public void randomChange() //Puts in random values across all Gameobjects
    {
        Vector3 temp; //temp Vector for temporaily storing the point information!
        for (int i = 0; i < pointsList.Count; i++) //Loop goes through each object of list and change Y value with random factor
        {
            if (pointsList[i] != null) 
            {

                temp = pointsList[i].transform.position; //into temp temporarily while it is changed. 
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

        GameObject waveOutline = (GameObject)Instantiate(wavePrefab, wavePositionParent.transform.localPosition, wavePositionParent.transform.localRotation, wavePositionParent.transform) as GameObject;//instantiating the pink outline arround points

        float step = 2f / xLength;
        Vector3 scale = Vector3.one * step;//all cube points are instantiated between -1 and 1
        Vector3 pointVec;//vector needed for the loop
        pointVec.z = 0;//z is not needed
        pointVec.y = 0;

        for (int i = 0; i < xLength; i++)//for-loop instantiating points and adding points to the gameobject points list
        {
            GameObject point;//this is only for reference
            pointsList.Add(point = (GameObject)Instantiate(xPoint, wavePositionParent.transform.localPosition, Quaternion.Euler(0, 90, 0)) as GameObject);
            //adding and instantiating points from the xPoint prefab at 90 degrees so it advances along the pink outline

            pointVec.x = (i + 0.5f) * step - 1f;//

            switch(curvePreset)
            {
                case 1:
                    pointVec.y = presets.squareWave(pointVec.x,frequency,amplitude);
                    break;
                case 2:
                    pointVec.y = presets.sawTooth(pointVec.x, frequency, amplitude);
                    break;
                default:
                    pointVec.y = presets.sine(pointVec.x, frequency, amplitude);
                    break;
            }
            //pointVec.y = pointVec.x * pointVec.x;//change this to change y of cubes
           
            point.transform.localPosition = pointVec;
            point.transform.localScale = scale;
            point.transform.SetParent(wavePositionParent.transform, true);
            //setting the parent at the end, and stating "true" in order to let the points keep their values

        }   
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

        //LineRenderer creation
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.positionCount = xLength;

        //LineRenderer Color
        float alpha = 1.0f; //alpha variable used to set parameters of gradient
         Gradient lowFreqgradient = new Gradient(); //New Gradient so we can change colors
        lowFreqgradient.SetKeys( //Set Keys of the new gradient, they change between two colors, but for now they are set to the same
            new GradientColorKey[] { new GradientColorKey(lowFrequencyColor, 0.0f), new GradientColorKey(lowFrequencyColor, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );

  

        lineRenderer.colorGradient = lowFreqgradient; //Set the color to our gradient
      




    }


    void Update()
    {

        amplitude = Hv_pdint1_AudioLib.gain;

        if (isLowFreqMode) // if isLowFreqMode == true, scale the frequency down by the lowFreqScaleFactor
        {
            frequency = Hv_pdint1_AudioLib.freq / lowFreqScaleFactor;

            float alpha = 1.0f; //LOOK in start for explanation..
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            Gradient lowFreqgradient = new Gradient();
            lowFreqgradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(lowFrequencyColor, 0.0f), new GradientColorKey(lowFrequencyColor, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
            lineRenderer.colorGradient = lowFreqgradient;

            
        }

        if (!isLowFreqMode) // if isLowFreqMode == false, do not scale the frequency down
        {
            frequency = Hv_pdint1_AudioLib.freq;

            float alpha = 1.0f; //LOOK in start for explanation..
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            Gradient highFreqgradient = new Gradient();
            highFreqgradient.SetKeys(
           new GradientColorKey[] { new GradientColorKey(highFrequencyColor, 0.0f), new GradientColorKey(highFrequencyColor, 1.0f) },
           new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
           );
            lineRenderer.colorGradient = highFreqgradient;
        }

        if (Input.GetKeyDown("backspace"))
        {
            print("Backspace key was pressed");
            createAndInstantiatePoints();
        }

        if (Input.GetKeyDown("r"))
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

        if (Input.GetKeyDown("s"))
        {
            //ScalePointsOutline((pointSpacing + xPoint.transform.localScale.x) * xLength, waveOutline);
            //keeping the above function here but might not be needed in the future

        }

        //Toggle linerenderer
        if (isCurveLined)
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = xLength;
            Vector3[] pointsVecArray = new Vector3[xLength];
            for (int i = 0; i < pointsList.Count; i++)
            {
                pointsVecArray[i] = pointsList[i].transform.position;
            }
            lineRenderer.SetPositions(pointsVecArray);
        }
        else
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = 0;
        }

        //Toggle GameObject Mesh
        if (!isCurveDotted)
        {
            for (int i = 0; i < pointsList.Count; i++)
            {
                pointsList[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
            
        } else
        {
            for (int i = 0; i < pointsList.Count; i++)
            {
                pointsList[i].gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }

        //Toggle animation of curve
        if (isCurveAnimated)
        {
            for (int i = 0; i < pointsList.Count; i++)
            {
                //If we use localPosition in this loop it fucks up... I have no idea why, i'll look into it at some point (tobi)
                Vector3 position = pointsList[i].transform.position;
                switch (curvePreset)
                {
                    case 1:
                        position.y = presets.squareWave(position.x + Time.time, frequency, amplitude);
                        break;
                    case 2:
                        position.y = presets.sawTooth(position.x + Time.time, frequency, amplitude);
                        break;
                    default:
                        position.y = presets.sine(position.x + Time.time, frequency, amplitude);
                        break;
                }
                pointsList[i].transform.position = position;
            } 
        }

        
    }
}

