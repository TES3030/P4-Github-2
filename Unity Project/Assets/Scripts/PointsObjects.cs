/*
 * 
 * PointsObjects:
 * Class that instantiates three variables and a constructor for use
 * in graphcontrol script.
 * 
 * 
 * Future:
 * Comments:
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsObjects : MonoBehaviour
{

    public static float xValue = 0;
    public float yValue = 0;
    public float zValue = 0;

    public float amplitudeFactor;

    public PointsObjects(Vector3 vector) {
        xValue = vector.x;
        yValue = vector.y;
        zValue = vector.z;

    }

}
