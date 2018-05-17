using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbedGlow : MonoBehaviour
{

    //GameObject outlineObject;
    Material startPresetMat;
    Material startFrameMat;
    Material glowPresetMat;
    Material glowFrameMat;


    // Use this for initialization
    void Start()
    {

        startPresetMat = Resources.Load("Materials/MAT_DefaultPresetOutline") as Material;
        startFrameMat = Resources.Load("Materials/MAT_DefaultFrameOutline") as Material;
        glowPresetMat = Resources.Load("Materials/MAT_Outline") as Material;
        glowFrameMat = Resources.Load("Materials/MAT_FrameOutline") as Material;

    }

    void GrabbableTarget()
    {
        gameObject.GetComponentInChildren<Renderer>().material = glowPresetMat;//changes material of preset
    }

    void NotGrabbableTarget()
    {
        gameObject.GetComponentInChildren<Renderer>().material = startPresetMat;
    }

    void Grabbed(bool isPreset)
    {


        //if the gameobject with this script on it is a preset run following code
        if (isPreset)
        {

            //show outline on waveframe
            Renderer[] children;
            children = GameObject.FindGameObjectWithTag("WaveFrame").GetComponentsInChildren<Renderer>();
            for (int i = 0; i < children.Length; i++)
            {
                children[i].material = glowFrameMat;
            }
        }
    }

    void NotGrabbed(bool isPreset)
    {


        if (isPreset)
        {

            //show outline on waveframe
            Renderer[] children;
            children = GameObject.FindGameObjectWithTag("WaveFrame").GetComponentsInChildren<Renderer>();
            for (int i = 0; i < children.Length; i++)
            {
                children[i].material = startFrameMat;
            }

        }

    }
}
