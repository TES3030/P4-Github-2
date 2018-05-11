using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItemScript : MonoBehaviour {

    Vector3 startPos;
    
    // Use this for initialization
    void Start () {
        startPos = gameObject.transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void ResetPos()
    {
        gameObject.transform.localPosition = startPos;
    }
}
