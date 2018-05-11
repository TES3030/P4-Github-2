using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItemScript : MonoBehaviour
{
    GameObject mom;
    Renderer[] childColors;

    Vector3 startPos;
    ParticleSystem ps1;
    ParticleSystem ps2;
    ParticleSystem ps3;
    ParticleSystem ps4;

    ParticleSystem.EmissionModule em1;
    ParticleSystem.EmissionModule em2;
    ParticleSystem.EmissionModule em3;
    ParticleSystem.EmissionModule em4;
    //public ParticleSystem.EmissionModule[] psEm = new ps.emission[4];

    // Use this for initialization
    void Start()
    {
        startPos = gameObject.transform.localPosition;

        ps1 = GameObject.Find("ParticleEmitter1").GetComponent<ParticleSystem>();
        ps2 = GameObject.Find("ParticleEmitter2").GetComponent<ParticleSystem>();
        ps3 = GameObject.Find("ParticleEmitter3").GetComponent<ParticleSystem>();
        ps4 = GameObject.Find("ParticleEmitter4").GetComponent<ParticleSystem>();

        em1 = ps1.emission;
        em2 = ps2.emission;
        em3 = ps3.emission;
        em4 = ps4.emission;

        mom = GameObject.Find("WaveFrame");

        childColors = mom.GetComponentsInChildren<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.localPosition != startPos)
        {
            //Material mat = mom.GetComponentInChildren<Renderer>().material;
            foreach (Renderer color in childColors)
            {
                color.material.color = Color.yellow;
            }
            //mat.color = Color.yellow;
            //also add particles
            em1.enabled = true;
            em2.enabled = true;
            em3.enabled = true;
            em4.enabled = true;

        }
        else
        {
            em1.enabled = false;
            em1.enabled = false;
            em1.enabled = false;
            em1.enabled = false;

            foreach (Renderer color in childColors)
            {
                color.material.color = Color.gray;
            }

            /*
            for (int i = 0; i < 3; i++)
            {
                psEm[i].enabled = false;
            }
            */
        }

    }

    void ResetPos()
    {
        gameObject.transform.localPosition = startPos;
    }
}
