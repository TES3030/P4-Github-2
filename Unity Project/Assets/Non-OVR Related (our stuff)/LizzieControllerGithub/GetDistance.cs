using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDistance : MonoBehaviour {

    public Transform transformA;
    public Transform transformB;
    public float dist;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

       dist = Vector3.Distance(transformA.position, transformB.position);

       // Debug.DrawLine(transformA.position, transformB.position, Color.red, Time.deltaTime);

        //Debug.Log(dist);
		
	}
}
