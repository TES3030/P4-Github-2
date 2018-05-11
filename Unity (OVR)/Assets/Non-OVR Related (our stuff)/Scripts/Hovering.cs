using UnityEngine;
using System.Collections;

public class Hovering : MonoBehaviour
{
    GameObject[] goList;

    public float amplitude;          //Set in Inspector 
    public float speed;                  //Set in Inspector 
    public float tempVal;
    public Vector3 tempPos;
    void Start()
    {
        goList = GameObject.FindGameObjectsWithTag("PresetFrame");

    }
    void Update()
    {     
        for (int i = 0; i<goList.Length; i++)
        {
            tempVal = goList[i].transform.localPosition.y;
            tempPos = goList[i].transform.localPosition;
            tempPos.y = tempVal + amplitude * Mathf.Cos(speed * Time.time);
            goList[i].transform.localPosition = tempPos;
        }
        
    }
   
}