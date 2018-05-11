using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {

    public OVRInput.Controller controller;
    public string buttonName;

    public float grabRadius;
    public LayerMask grabMask; //So that the cast only registers things in that layer

    private GameObject grabbedObject;
    private bool grabbing;

    void GrabObject()
    {
        grabbing = true;

        RaycastHit[] hits;

        hits = Physics.SphereCastAll(transform.position, grabRadius, transform.forward, 0f, grabMask); //Look up raycast... basicly shoot out sphere from hand
        if(hits.Length > 0)
        {

            int closestHit = 0;
            for(int i=0; i < hits.Length; i++)
            {
                if (hits[i].distance < hits[closestHit].distance) closestHit = i;
            }
            grabbedObject = hits[closestHit].transform.gameObject;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            grabbedObject.transform.position = transform.position;
            grabbedObject.transform.parent = transform;

        }
    }

    void DropObject()
    {
        grabbing = false;

        if(grabbedObject != null)
        {
            grabbedObject.transform.parent = null;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;

            grabbedObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(controller);

            grabbedObject = null;
        }
    }

	// Update is called once per frame
	void Update () {

        if (!grabbing && Input.GetAxis(buttonName) == 1) GrabObject();
        if (grabbing && Input.GetAxis(buttonName) < 1) DropObject();
            
        }
}
