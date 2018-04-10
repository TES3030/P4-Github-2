using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour {

    public GameObject firstConnection;
    public GameObject secondConnection;

    private LineRenderer line;

  
    void Start()
    {

        line = this.gameObject.AddComponent<LineRenderer>();


        line.SetWidth(0.05F, 0.05f);

        line.SetVertexCount(2);

    }



	
	// Update is called once per frame
	void Update () {
        if (firstConnection != null && secondConnection != null)
        {
            // Update position of the two vertex of the Line Renderer
            line.SetPosition(0, firstConnection.transform.position);
            line.SetPosition(1, secondConnection.transform.position);
        }
    }



}

