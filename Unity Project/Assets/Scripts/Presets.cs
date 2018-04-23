using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presets : MonoBehaviour {

    public float amplitude = 1;
    public float frequency = 2;
    public float time;
    float yVal;

    public float sine(float xVal)
    {
        yVal = amplitude * Mathf.Sin(frequency * xVal);
        return yVal; 
    }

    public float squareWave(float xVal)
    {
        yVal = amplitude * Mathf.Sign(Mathf.Sin(frequency * xVal));
        Debug.Log("yVal =" + yVal.ToString());
        return yVal;
    }

    public float sawTooth(float xVal)
    {
        yVal = amplitude * ((xVal)/(1/frequency)- Mathf.Floor((xVal)/(1/frequency)));
        return yVal;
    }


    public 

	// Use this for initialization
	void Start () 
    {
       
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}
}
