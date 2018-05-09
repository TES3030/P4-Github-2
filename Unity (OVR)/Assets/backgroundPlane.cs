using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundPlane : MonoBehaviour {

    

    


    // Use this for initialization
    void Start () {
        Texture2D texture = Resources.Load("Textures/mountains_placeholder") as Texture2D; //No need to specify extension.

        Material material = new Material(Shader.Find("Diffuse"));

        material.mainTexture = texture;
        gameObject.GetComponent<Renderer>().material = material;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
