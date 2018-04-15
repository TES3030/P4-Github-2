// doesn't work atm :( 

using UnityEngine;
using System.Collections;

public class mouseDrag : MonoBehaviour
{
    public float freqMove = Hv_pdint1_AudioLib.freq; //find freq variable from other file so it can be changed while moving point
    public float pointX = PointsObjects.xValue;

    private void Start()
    {
        Vector3 objStartPosition = gameObject.transform.position;

    }

    void OnMouseDrag()
    {
        // camera should be tagged as mainCamera, otherwise you should reference it above.
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
        distance_to_screen));
    }
}