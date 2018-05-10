

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

    public float amplitude = 1;//Amplitude of waveform. 
    public float frequency = 2;//Frequency of waveform. 

    //enum for curve presets
    public GraphFunctionName curvePresetFunction;
    public int curvePresetFunctionInt;

    List<GameObject> pointsList = new List<GameObject>(); //List of all points in a curve. 

    //colors for the graph
    private Color lowFrequencyColor = Color.green;//Default color for Low Frequecy mode is green.
    private Color highFrequencyColor = Color.blue;//Default color for High Frequency mode is blue.
    private Color fancyColor1 = Color.red;//Default color for High Frequency mode is blue.
    private Color fancyColor2 = Color.yellow;//Default color for High Frequency mode is blue.
    LineRenderer lineRenderer;
    Gradient gradient = new Gradient();//gradient to be used later

    //function that creates points for the list and instantiates them
    public void CreateAndInstantiatePoints(Transform GraphHolderParent)//Need to receive xObjectLength and transfrom from this.
    {
        GameObject waveOutline = (GameObject)Instantiate(wavePrefab, GraphHolderParent.localPosition, GraphHolderParent.localRotation, GraphHolderParent) as GameObject;//Instantiating the pink outline arround points.

        float step = 2f / xLength;//X dimension x Object spacing relationship. 
        Vector3 scale = Vector3.one * step;//All cube points are instantiated between -1 and 1.
        Vector3 pointVec = new Vector3(0, 0, 0); //Vector needed for the loop.
        pointVec.z = 0f;//Z dimension
        pointVec.y = 0;//Y dimension. 
        pointVec.x = 0;//X is not needed as we are primarily working with the Y and Z dimesions. 

        for (int i = 0; i < xLength; i++)//For-loop instantiating points and adding points to the gameobject points list.
        {
            GameObject point;//This is  for reference.
            pointsList.Add(point = (GameObject)Instantiate(xPoint, GraphHolderParent.localPosition, GraphHolderParent.localRotation, GraphHolderParent) as GameObject);
            //Adding and instantiating points from the xPoint prefab at 90 degrees so it advances along the outline.

            pointVec.z = (i + 0.5f) * step - 1f; //Spacing between Z points. 
            pointVec.y = SetPointYPosition(pointVec.z);

            point.transform.localPosition = pointVec;
            point.transform.localScale = scale;
            //point.transform.SetParent(GraphHolderParent, false);//BUG HERE

            //Setting the parent at the end, and stating "true" in order to let the points keep their values.
        }
    }

    void SetXLength(int x) //Function to change the X scaling length. 
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

    Gradient UseNewGradient(Color inColor1, Color inColor2)//Returns gradient. 
    {
        //If there is no linerenderer.
        if ((gameObject.GetComponent("LineRenderer") as LineRenderer) == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        //Perhaps change shader in the future
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        //Set width and line-segment amount
        lineRenderer.widthMultiplier = lineWidth;
        lineRenderer.positionCount = xLength;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;

            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(inColor1, 0.0f), new GradientColorKey(inColor2, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 1.0f), new GradientAlphaKey(alpha, 1.0f) }
                );

        lineRenderer.colorGradient = gradient;

        return gradient;
    }

    void SetPreset(int i)
    {
        curvePresetFunction = (GraphFunctionName)i;
        Debug.Log(curvePresetFunction);
    }

    float SetPointYPosition(float x) //Y points postion. 
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
        wavePrefab = (GameObject)Resources.Load("waveOutlineFrame", typeof(GameObject));//Loading the prefab from the resources folder in order to access its values.    
        SetXLength(GameObject.Find("GraphsManager").GetComponent<GraphsManager>().xLengthForNextGraph);

    }

    void Start()
    {
        CreateAndInstantiatePoints(gameObject.GetComponent<Transform>());
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
                    UseNewGradient(lowFrequencyColor, lowFrequencyColor);
                    break;
                case 1: //High frequency mode and color. 
                    amplitude = Hv_pdint1_AudioLib.gain;
                    frequency = Hv_pdint1_AudioLib.freq;
                    UseNewGradient(highFrequencyColor, highFrequencyColor);
                    break;
                default: //Default scenario resets to default values. 
                    // if isLowFreqMode == false, return to default state
                    frequency = 2;
                    amplitude = 1;
                    UseNewGradient(fancyColor1, fancyColor2);
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
                Vector3 position = pointsList[i].transform.localPosition;
                position.y = SetPointYPosition(position.z + Time.time);

                pointsList[i].transform.localPosition = position;
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
