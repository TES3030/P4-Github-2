using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDistance : MonoBehaviour
{

    private Transform transformL;
    private Transform transformR;
    public static float xDist;
    public static float yDist;
    public static float zDist;
    //private Vector3 dist;

    // Use this for initialization
    void Start()
    {
        transformL = GameObject.Find("L").transform;
        transformR = GameObject.Find("R").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dist = transformL.localPosition - transformR.localPosition;
        xDist = Mathf.Abs(dist.x);
        yDist = Mathf.Abs(dist.y);
        zDist = Mathf.Abs(dist.z);

        // Debug.DrawLine(transformA.position, transformB.position, Color.red, Time.deltaTime);
        //Debug.Log(dist);

    }
}