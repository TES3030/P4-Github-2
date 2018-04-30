

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphControl : MonoBehaviour
{
    Presets presets = new Presets(); //Use curve-presets from Presets script.

    [Range(10, 100)] //This makes xLength a slider in the inspector.
    public int xObjectLength = 10; // Amount of Gameobjects to create to represent a curve in Unity.
    public bool CurveAnimated = true; //Animation of curve on/off.
    public bool CurveLined = true; //LineRenderer on/off.
    public bool LowFrequencyMode = true; //Turn LowFrequency mode on/off. 
    public FreqModeName frequencyMode;
    public bool CurveDotted = false; //Turn gameObjects that create curve on/off.


    public float lowFrequencyScaleFactor = 10; //Determine scalefactor between lowfreqmode and highfreqmode.

    public GameObject xPoint; //Prefab from which all points are made.
    private GameObject wavePrefab; //Prefab form which all waveoutlines are made.
    public float amplitude = 1;//Amplitude of waveform. 
    public float frequency = 2;//Frequency of waveform. 

    public GraphFunctionName curvePresetFunction; //Drop down menu for Presets. 

    List<GameObject> pointsList = new List<GameObject>(); //List of all points in a curve. 

    //LineRenderer colors low/high freq
    public Color lowFrequencyColor = Color.green; //Default color for Low Frequecy mode is green.
    public Color highFrequencyColor = Color.blue;//Default color for High Frequency mode is blue.


    //Code to randomly change points values - only used for developing.
    public void randomChange() //Puts in random values across all Gameobjects
    {
        Vector3 temp; //Temp Vector for temporaily storing the point information!
        for (int i = 0; i < pointsList.Count; i++) //Loop goes through each object of list and change Y value with random factor
        {
            if (pointsList[i] != null) 
            {

                temp = pointsList[i].transform.position; //Into temp temporarily while it is changed. 
                temp.y *= (Random.Range(-1.0f, 1.0f)); //Access and change y value.
                pointsList[i].transform.position = temp; //Back into list.

            }
            else
            {
                Debug.Log("obj is null"); //Debug if empty list. 
            }
        }

        for (int i = 0; i < pointsList.Count; i++) //Go through objects in the pointslist.
        {
            print(pointsList[i]); //Print position of objects to console.

        }
    }

    //Function that creates points for the lsit and instantiates them to it. 
    public void createAndInstantiatePoints()
    {

        GameObject wavePositionParent = new GameObject("WavePosition");//The empty game object containing the position of the curve/wave. 
        wavePositionParent.transform.rotation = Quaternion.Euler(0, 90, 0);//90 degree rotation to align all objects correctly in the X/Y/Z dimensions. 
        wavePositionParent.transform.position = new Vector3(0, 0, 0.2f);//Slight adjustment. 

        GameObject waveOutline = (GameObject)Instantiate(wavePrefab, wavePositionParent.transform.localPosition, wavePositionParent.transform.localRotation, wavePositionParent.transform) as GameObject;//Instantiating the outline around points. 

        float step = 2f / xObjectLength; //X dimension x Object spacing relationship. 
        Vector3 scale = Vector3.one * step;//All cube points are instantiated between -1 and 1. 
        Vector3 pointVec;//Vector needed for the loop.
        pointVec.z = 0;//Z is not needed as we are primarily working with the Y and X dimesions. 
        pointVec.y = 0;//Y dimension. 

        for (int i = 0; i < xObjectLength; i++)//For-loop instantiating points and adding points to the gameobject points list. 
        {
            GameObject point;//This is for reference.
            pointsList.Add(point = (GameObject)Instantiate(xPoint, wavePositionParent.transform.localPosition, Quaternion.Euler(0, 90, 0)) as GameObject);//90 degree rotation to align all objects correctly in the X/Y/Z dimensions. 
            //Adding and instantiating points from the xObjectLength prefab at 90 degrees so it advances along the outline.

            pointVec.x = (i + 0.5f) * step - 1f;//Spacing between X points. 

            switch((int)curvePresetFunction) //Switch for curve presets. 
            {
                case 1: //Square wave. 
                    pointVec.y = presets.squareWave(pointVec.x,frequency,amplitude);
                    break;
                case 2: //Sawtooth wave. 
                    pointVec.y = presets.sawTooth(pointVec.x, frequency, amplitude);
                    break;
                default: //Sine wave. 
                    pointVec.y = presets.sine(pointVec.x, frequency, amplitude);
                    break;
            }
            //pointVec.y = pointVec.x * pointVec.x;//Change this to change y of cubes.
           
            point.transform.localPosition = pointVec; //Change the object position in Unity. 
            point.transform.localScale = scale; //Scale in accordance with scale factor. 
            point.transform.SetParent(wavePositionParent.transform, true); //Hierachical relationship of parrent. 
            //Setting the parent at the end, and stating "true" in order to let the points keep their values.

        }   
    }

    public void ScalePointsOutline(float waveWidth, GameObject wave)//Functions that scales the waveoutline.
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
        wavePrefab = (GameObject)Resources.Load("testWaveOutline", typeof(GameObject));//Loading the prefab from the resources folder in order to access its values.                                               

        //LineRenderer creation.
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>(); //Adds linerenderer to Graph-holder object. 
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));//Creats new material for the linerenderer. 
        lineRenderer.widthMultiplier = 0.1f; //Changes the thickness of the line. 
        lineRenderer.positionCount = xObjectLength;//Puts all the positions of the X objects to the positions in the linerenderer. 

        //LineRenderer Color.
        float alpha = 1.0f; //Alpha variable used to set parameters of gradient. 
         Gradient lowFreqgradient = new Gradient(); //New Gradient so we can change colors
        lowFreqgradient.SetKeys( //Set Keys of the new gradient, they change between two colors, but for now they are set to the same
            new GradientColorKey[] { new GradientColorKey(lowFrequencyColor, 0.0f), new GradientColorKey(lowFrequencyColor, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );

        lineRenderer.colorGradient = lowFreqgradient; //Set the color to our gradient

    }


    void Update()
    {
        if (LowFrequencyMode) //If we have the low frequency mode activated, you can change between the two. 
        {
            switch ((int)frequencyMode) //Switch case for frequency modes. 
            {
                case 0: //Low frequency mode divides the frequncy by a scaling factor, so that the user may see the wave "Slowed down". 
                    amplitude = Hv_pdint1_AudioLib.gain; //PureData intergration of amplitude data. 
                    frequency = Hv_pdint1_AudioLib.freq / lowFrequencyScaleFactor; //PureData intergration of frequncy data. 



                    float alpha = 1.0f; //LOOK in start for explanation..
                    LineRenderer lineRenderer = GetComponent<LineRenderer>();
                    Gradient lowFreqgradient = new Gradient();
                    lowFreqgradient.SetKeys(
                    new GradientColorKey[] { new GradientColorKey(lowFrequencyColor, 0.0f), new GradientColorKey(lowFrequencyColor, 1.0f) },
                    new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
                    );
                    lineRenderer.colorGradient = lowFreqgradient;
                    

                    break;
                case 1: //No scaling of the data from PureData. 
                    amplitude = Hv_pdint1_AudioLib.gain;
                    frequency = Hv_pdint1_AudioLib.freq;

                
                    float alpha02 = 1.0f; //LOOK in start for explanation..
                    LineRenderer lineRenderer02 = GetComponent<LineRenderer>();
                    Gradient highFreqgradient = new Gradient();
                    highFreqgradient.SetKeys(
                   new GradientColorKey[] { new GradientColorKey(highFrequencyColor, 0.0f), new GradientColorKey(highFrequencyColor, 1.0f) },
                   new GradientAlphaKey[] { new GradientAlphaKey(alpha02, 0.0f), new GradientAlphaKey(alpha02, 1.0f) }
                   );
                    lineRenderer02.colorGradient = highFreqgradient;
                    
                    break;
                default:
                    // If LowFrequencyMode is false, return to default variables. 
                    frequency = 2;
                    amplitude = 1;
                    LowFrequencyMode = !LowFrequencyMode;
                    break;

            }
        }

        
          

        if (Input.GetKeyDown("backspace"))
        {
            print("Backspace key was pressed.");
            createAndInstantiatePoints(); //Look at function description. 
        }

        if (Input.GetKeyDown("r"))
        {
            print("Space key was pressed.");
            randomChange(); //Look at function description. 
        }


        if (Input.GetKeyDown("p")) //Prints the position of the points of the list. 
        {
            print("Printing list");
            print("List length: " + pointsList.Count);
            for (int i = 0; i < pointsList.Count; i++) { print(" Position of number " + i + " of the list: " + pointsList[i].transform.position); }
        }

        if (Input.GetKeyDown("s"))
        {
            //ScalePointsOutline((pointSpacing + xPoint.transform.localScale.x) * xObjectLength, waveOutline);
            //keeping the above function here but might not be needed in the future

        }

        //Toggle linerenderer.
        if (CurveLined)
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = xObjectLength;
            Vector3[] pointsVecArray = new Vector3[xObjectLength];
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

        //Toggle GameObject Mesh.
        if (!CurveDotted)
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
        

        //Toggle animation of curve.
        if (CurveAnimated)
        {
            for (int i = 0; i < pointsList.Count; i++)
            {
                //If we use localPosition in this loop it fucks up... I have no idea why, i'll look into it at some point (tobi)
                Vector3 position = pointsList[i].transform.position;
                switch ((int)curvePresetFunction)
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

