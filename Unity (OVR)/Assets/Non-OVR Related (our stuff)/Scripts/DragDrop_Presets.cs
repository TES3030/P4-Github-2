using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop_Presets : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered!");
        if (other.gameObject == GameObject.Find("PresetFrame1"))//this condition should be dynamic as well
        {
            //need to identify the correct preset
            Debug.Log("Triggered PRESET!" + "");

            //then send a message to the script GraphControl and run the "setPreset" with the preset index as a parameter (0,1,2)
            //GameObject.Find("graphHolder(clone)").SendMessage(SetPreset, index);

            //then reset position, depending on what preset index;
            //ResetPos();
        }
    }


}
