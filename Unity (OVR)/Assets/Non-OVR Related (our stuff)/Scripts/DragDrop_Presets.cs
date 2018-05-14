using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop_Presets : MonoBehaviour
{
    GameObject R, L;
    void Start()
    {
        R = GameObject.Find("R");
        L = GameObject.Find("L");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered!");
        if (other.gameObject.CompareTag("PresetFrame"))//if the colliders gameobject has the tag "presetframe"
        {
            GameObject otherGO = other.transform.gameObject;//shortcut for the gameobject
            switch (otherGO.name)//check the name of the gameobject
            {
                case "SineFrame"://is the name sineframe?
                    Debug.Log("Triggered PRESET! go: " + otherGO.name);

                    //then send a message to the script GraphControl and run the "setPreset" with the preset index as a parameter (3,1,2)
                    if (gameObject.transform.parent.CompareTag("GraphHolder"))
                        gameObject.transform.parent.SendMessage("SetPreset", 3);

                    //then reset position
                    otherGO.SendMessage("ResetPos");
                    break;

                case "SquareFrame"://is the name squareframe?
                    Debug.Log("Triggered PRESET! go: " + otherGO.name);

                    if (gameObject.transform.parent.CompareTag("GraphHolder"))
                        gameObject.transform.parent.SendMessage("SetPreset", 1);

                    otherGO.SendMessage("ResetPos");
                    break;

                case "SawtoothFrame"://is the name sawtoothframe?
                    Debug.Log("Triggered PRESET! go: " + otherGO.name);

                    if (gameObject.transform.parent.CompareTag("GraphHolder"))
                        gameObject.transform.parent.SendMessage("SetPreset", 2);

                    otherGO.SendMessage("ResetPos");
                    break;

                default:
                    break;

            }

        }
    }

    void OnTriggerStay(Collider other)
    {
        GameObject otherGO = other.transform.gameObject;//shortcut for the gameobject
        R.SendMessage("DropObject");
        L.SendMessage("DropObject");//this isnt the best solution but good for now
        otherGO.SendMessage("ResetPos");

    }
}
