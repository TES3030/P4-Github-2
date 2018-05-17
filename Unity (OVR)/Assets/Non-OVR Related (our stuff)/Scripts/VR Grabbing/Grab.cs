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

    private Quaternion lastRotation, currentRotation;

    void GrabObject()
    {
        grabbing = true;

        RaycastHit[] hits;

        hits = Physics.SphereCastAll(transform.position, grabRadius, transform.forward, 0f, grabMask); //Look up raycast... basicly shoot out sphere from hand
        if (hits.Length > 0)
        {

            int closestHit = 0;
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].distance < hits[closestHit].distance) closestHit = i;
            }

            grabbedObject = hits[closestHit].transform.gameObject;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true;

            GrabHint(grabbedObject);

            //grabbedObject.transform.position = transform.position;
            //grabbedObject.transform.parent = transform;
            grabbedObject.transform.SetParent(transform, true);

        }
    }

    void DropObject()
    {
        grabbing = false;

        if (grabbedObject != null)
        {
            grabbedObject.transform.parent = null;
            //grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(controller);
            grabbedObject.GetComponent<Rigidbody>().angularVelocity = GetAngularVeclocity();

            ExitGrabHint(grabbedObject);
            
            grabbedObject = null;
        }
    }

    void GrabHint(GameObject objectGrabbed)
    {
        if (grabbedObject.CompareTag("PresetFrame"))
        {
            grabbedObject.SendMessage("Grabbed", true);
        }
        /*
        else
        {
            grabbedObject.SendMessage("Grabbed", false);
        }*/
    }

    void ExitGrabHint(GameObject objectGrabbed)
    {
        if (grabbedObject.CompareTag("PresetFrame"))
        {
            grabbedObject.SendMessage("NotGrabbed", true);
        }

        /*
            else
            {
                grabbedObject.SendMessage("NotGrabbed", false);
            }*/
    }

    void HoverGlow(GameObject hover)
    {
        hover.SendMessage("GrabbableTarget");
    }

    void ExitHoverGlow(GameObject hover)
    {
        hover.SendMessage("NotGrabbableTarget");
    }

    Vector3 GetAngularVeclocity()
    {
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(lastRotation);
        return new Vector3(Mathf.DeltaAngle(0, deltaRotation.eulerAngles.x), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.y), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.z));
    }

	// Update is called once per frame
	void Update () {
        

        if (grabbedObject !=null)
        {
            lastRotation = currentRotation;
            currentRotation = grabbedObject.transform.rotation;
        }


        if (!grabbing && Input.GetAxis(buttonName) == 1)
        {
            GrabObject();
        }
            
        if (grabbing && Input.GetAxis(buttonName) < 1)
        {
            DropObject();
        }
            
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!grabbing && Input.GetAxis(buttonName) < 1 || grabbing)
        {
            HoverGlow(other.transform.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.gameObject.CompareTag("WaveEditArea"))
        {
            //check if both controllers in here...

            print("I'm Altering the wave!");

            //alter ampl / freq
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ExitHoverGlow(other.transform.gameObject);
    }
}
