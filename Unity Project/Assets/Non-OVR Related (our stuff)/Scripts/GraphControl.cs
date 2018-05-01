

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphControl : MonoBehaviour
{
    Presets presets;//Use curve-presets from Presets script.

    private float lineWidth = 0.1f;
    private int xLength; // Amount of Gameobjects to create to represent a curve in Unity.
    public bool isCurveAnimated = true; //Animation of curve on/off.
    public bool isCurveLined = true; //LineRenderer on/off.
    public bool isLowFrequencyMode = true; //Turn LowFrequency mode on/off. 
    public FreqModeName frequencyMode;
    public bool isCurveDotted = false; //Turn gameObjects that create curve on/off.

    public float lowFrequencyScaleFactor = 10; //Determine scalefactor between lowfreqmode and highfreqmode.

    public GameObject xPoint; //Prefab from which all points are made.
    private GameObject wavePrefab; //Prefab form which all waveoutlines are made.
                                   //private GameObject graphHolder; //Prefab form which all grapholders are made.

    public float amplitude = 1;//Amplitude of waveform. 
    public float frequency = 2;//Frequency of waveform. 

    public GraphFunctionName curvePresetFunction;

    List<GameObject> pointsList = new List<GameObject>(); //List of all points in a curve. 

    private Color lowFrequencyColor = Color.green;//Default color for Low Frequecy mode is green.
    private Color highFrequencyColor = Color.blue;//Default color for High Frequency mode is blue.
    LineRenderer lineRenderer;
    Gradient gradient = new Gradient();



    //fucntion that creates points for the lsit and instantiates them

    public void createAndInstantiatePoints(Transform GraphHolderParent)//Need to receive xObjectLength and transfrom from this.
    {
        GameObject waveOutline = (GameObject)Instantiate(wavePrefab, GraphHolderParent.localPosition, GraphHolderParent.localRotation, GraphHolderParent) as GameObject;//Instantiating the pink outline arround points.

        float step = 2f / xLength;//X dimension x Object spacing relationship. 
        Vector3 scale = Vector3.one * step;//All cube points are instantiated between -1 and 1.
        Vector3 pointVec;//Vector needed for the loop.
        pointVec.z = 0;////Z is not needed as we are primarily working with the Y and X dimesions. 
        pointVec.y = 0;//Y dimension. 

        for (int i = 0; i < xLength; i++)//For-loop instantiating points and adding points to the gameobject points list.
        {
            GameObject point;//This is  for reference.
            pointsList.Add(point = (GameObject)Instantiate(xPoint, GraphHolderParent.localPosition, Quaternion.Euler(0, 90, 0)) as GameObject);
            //Adding and instantiating points from the xPoint prefab at 90 degrees so it advances along the outline.

            pointVec.x = (i + 0.5f) * step - 1f; //Spacing between X points. 
            pointVec.y = setPointYPosition(pointVec.x);

            point.transform.localPosition = pointVec;
            point.transform.localScale = scale;
            point.transform.SetParent(GraphHolderParent.transform, true);
            //Setting the parent at the end, and stating "true" in order to let the points keep their values.
        }
    }

    public void ScalePointsOutline(float waveWidth, GameObject wave)//Function. that scales the waveoutline. 
    {
        //parameter waveWidth is the distance needed to be between each point - ie. how long should the plane be?
        //Vector3 temp = wave.transform.localScale;
        //temp.z = waveWidth;
        //temp.y *= 3;
        //wave.transform.localScale = temp;
        //wave.transform.localScale = scale;
        //print("planeWidth: "+ pointSpacing + " + " + xPoint.transform.localScale.z + " * " + xObjectLength  + " = " + waveWidth);

    }

    void setXLength(int x) //Function to change the X scaling length. 
    {
        xLength = x;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.positionCount = xLength;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(lowFrequencyColor, 0.0f), new GradientColorKey(lowFrequencyColor, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
        lineRenderer.colorGradient = gradient;
        
    }

    Gradient useNewGradient(Color color)//Returns gradient. 
    {
        //If there is no linerenderer.

        if ((gameObject.GetComponent("LineRenderer") as LineRenderer) == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        //Set in depending on parameter
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        //Make width final variable.
        lineRenderer.widthMultiplier = lineWidth;
        lineRenderer.positionCount = xLength;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
      
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(color, 0.0f), new GradientColorKey(color, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
                );
      
    

        lineRenderer.colorGradient = gradient;

        return gradient;
    }

    float setPointYPosition(float x) //Y points postion. 
    {
        float y;
        switch ((int)curvePresetFunction)
        {
            case 1: //Square wave.
                y = presets.squareWave(x, frequency, amplitude);
                break;
            case 2: //Sawtooth wave. 
                y = presets.sawTooth(x, frequency, amplitude);
                break;
            default: //Sine wave. 
                y = presets.sine(x, frequency, amplitude);
                break;
        }
        return y;
    }

    private void Awake()//this goes before start
    {
        presets = new Presets(); //Use curve-presets from Presets script.
        wavePrefab = (GameObject)Resources.Load("waveOutline", typeof(GameObject));//Loading the prefab from the resources folder in order to access its values.    
        setXLength(GameObject.Find("GraphsManager").GetComponent<GraphsManager>().xLengthForNextGraph);

    }

    void Start()
    {
        createAndInstantiatePoints(gameObject.GetComponent<Transform>());


    }


    void Update()
    {



        if (isLowFrequencyMode)
        {

            switch ((int)frequencyMode)
            {

                case 0: //Low frequency mode and color. 
                    amplitude = Hv_pdint1_AudioLib.gain;
                    frequency = Hv_pdint1_AudioLib.freq / lowFrequencyScaleFactor;
                    useNewGradient(lowFrequencyColor);
                    break;
                case 1: //High frequency mode and color. 
                    amplitude = Hv_pdint1_AudioLib.gain;
                    frequency = Hv_pdint1_AudioLib.freq;
                    useNewGradient(highFrequencyColor);
                    break;
                default: //Default scenario resets to default values. 
                    // if isLowFreqMode == false, return to default state
                    frequency = 2;
                    amplitude = 1;
                    isLowFrequencyMode = !isLowFrequencyMode;// dont touch this D:
                    break;

            }
        }


        if (Input.GetKeyDown("backspace"))
        {
            print("Backspace key was pressed");
            //createAndInstantiatePoints();
        }

        if (Input.GetKeyDown("r"))
        {
            print("Space key was pressed");
            //randomChange();
        }


        if (Input.GetKeyDown("p"))
        {
            print("printing list");
            print("list length: " + pointsList.Count);
            for (int i = 0; i < pointsList.Count; i++) { print(" pos of number " + i + " of the list: " + pointsList[i].transform.position); }
        }

        if (Input.GetKeyDown("s"))
        {
            //ScalePointsOutline((pointSpacing + xPoint.transform.localScale.x) * xObjectLength , waveOutline);
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

        }
        else
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
                position.y = setPointYPosition(position.x + Time.time);

                pointsList[i].transform.position = position;
            }
        }


    }
}

//Code to randomly change points values - only used for developing.
/*
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
*/
