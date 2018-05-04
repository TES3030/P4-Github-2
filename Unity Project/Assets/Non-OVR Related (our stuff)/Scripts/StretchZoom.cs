using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchZoom : MonoBehaviour {

    void stretchToZoom()
    {
        // Store both touches.
        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        // Find the position in the previous frame of each touch.
        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        // Find the magnitude of the vector (the distance) between the touches in each frame.
        float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

        // Find the difference in the distances between each frame.
        float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

        /*
         On touch (make sure there are 2 touches), store the distance between the two touch points.

        Then until touches are released (or touch count changes):

        scaleFactor = touchDistance / initialTouchDistance;

        initialTouchDistance, ofc, being that distance you stored on touch down.

        That scale factor can then be applied to the gameobject's transform localscale.
         
         */
    }
}
