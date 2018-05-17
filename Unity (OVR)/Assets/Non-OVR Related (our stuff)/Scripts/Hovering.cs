using UnityEngine;
using System.Collections;

public class Hovering : MonoBehaviour
{
    GameObject[] goList;

    public float amplitude = 0.01f;          //Set in Inspector 
    public float speed = 1;                  //Set in Inspector 
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
            tempVal = goList[i].transform.position.y;
            tempPos = goList[i].transform.position;
            tempPos.y = tempVal + amplitude * Mathf.Cos(speed * Time.time);
            goList[i].transform.position = tempPos;
        }
        
    }
   
}