using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveTrigger : MonoBehaviour
{
    GameObject R, L;
    GameObject[] presetArray;

    bool rightHand = false;
    bool leftHand = false;

    public string RightButton;//NO PUBLIC
    public string LeftButton;

    float xDistance;
    float yDistance;
    float zDistance;

    //GetDistance getDistanceScript = null;
    float previousDistanceX = 0;
    

    void Start()
    {
        R = GameObject.Find("R");
        L = GameObject.Find("L");
        //getDistanceScript = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<GetDistance>();//accessing class getDistance

        presetArray = GameObject.FindGameObjectsWithTag("PresetFrame");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered!");
        if (other.gameObject.CompareTag("PresetFrame"))//if the colliders gameobject has the tag "presetframe"
        {
            GameObject otherGO = other.transform.gameObject;//shortcut for the gameobject

            //call particlesystem on colliders presets particlesystem
            otherGO.GetComponentInChildren<ParticleSystem>().Clear();
            otherGO.GetComponentInChildren<ParticleSystem>().Play();

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

        //Iff object is not already in the list
        if (other.transform.gameObject.CompareTag("RightHand"))
        {
            rightHand = true;

            Debug.Log("Right Hand Entered");
        }
        if (other.transform.gameObject.CompareTag("LeftHand"))
        {
            leftHand = true;
            Debug.Log("Left Hand Entered");
        }

    }

    void OnTriggerStay(Collider other)//this collider is a preset
    {

        if (other.transform.gameObject.CompareTag("PresetFrame"))//if "other" collider is a preset do this
        {
            GameObject otherGO = other.transform.gameObject;//shortcut for the gameobject aka preset
            R.SendMessage("DropObject");
            L.SendMessage("DropObject");//this isnt the best solution but good for now
            otherGO.SendMessage("ResetPos");

            //set all frames to active
            for (int i = 0; i < presetArray.Length; i++)
            {
                presetArray[i].SetActive(true);
            }
            //then set current to false
            otherGO.SetActive(false);
        }


        //------------- STRETCHING
        //remember to change buttons to triggers :D
        if (leftHand == true && rightHand == true && Input.GetAxis(RightButton) == 1 && Input.GetAxis(LeftButton)== 1) //If right hand and left hand is inside trigger and both button are pressed
        {
            //previousDistance should be set to 0 when grab button has been pressed

            print("X: " + GetDistance.xDist);
            print("Y: " + GetDistance.yDist);
            print("Z: " + GetDistance.zDist);

            if (GetDistance.xDist > previousDistanceX)//retrieving dist from getDistance
            {
                Debug.Log("Increasing X!!");
                previousDistanceX = GetDistance.xDist;
            }
            if (GetDistance.xDist < previousDistanceX)//retrieving dist from getDistance
            {
                Debug.Log("Decreasing X!!");
                previousDistanceX = GetDistance.xDist;
            }




            //Change frequency



        }
        
    }



    void OnTriggerExit(Collider other)
    {
        Debug.Log("BACON");

        if (other.transform.gameObject.CompareTag("RightHand"))
        {
            rightHand = false;

            Debug.Log("Right Hand leaves");
        }
        if (other.transform.gameObject.CompareTag("LeftHand"))
        {
            leftHand = false;
            Debug.Log("Left Hand leaves");
        }
    }


    }







