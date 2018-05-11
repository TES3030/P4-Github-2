using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presets : MonoBehaviour {

    public float time;
    float yVal;

    public float SineWave(float xVal, float frequency, float amplitude) 
    {
        yVal = amplitude * Mathf.Sin(frequency * xVal);
        //Debug.Log("yVal =" + yVal.ToString());
        return yVal; 
    }

    public float SquareWave(float xVal, float frequency, float amplitude) //Recursive. 
    {
        yVal = amplitude * Mathf.Sign(Mathf.Sin(frequency * xVal));
        
        return yVal;
    }

    public float SawtoothWave(float xVal, float frequency, float amplitude)
    {
        yVal = amplitude * 2 * ((xVal/((2f * Mathf.PI)/frequency))-Mathf.Floor(xVal / ((2f * Mathf.PI) / frequency))-0.5f);
        return yVal;
    }


	// Use this for initialization
	void Start () 
    {
       
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}
}
