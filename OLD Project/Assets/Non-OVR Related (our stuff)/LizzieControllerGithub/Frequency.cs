using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frequency : MonoBehaviour {

    GetDistance getDistance = null;
    float previousDistance = 0;

    // Use this for initialization
    void Start ()
    {
        getDistance = this.transform.GetComponent<GetDistance>();//accessing class getDistance
       
	}
	
	// Update is called once per frame
	void Update () {
        

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger ) && OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger)) {
           // Debug.Log("I was triggered. >:-)");
            if (getDistance.dist > previousDistance)//retrieving dist from getDistance
            {
                Debug.Log("Increasing!!");
                previousDistance = getDistance.dist;
            }
            if (getDistance.dist < previousDistance)//retrieving dist from getDistance
            {
                Debug.Log("Decreasing!!");
                previousDistance = getDistance.dist;
            }
        }
       
		
	}
}
