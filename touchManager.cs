using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchManager : MonoBehaviour
{
    private float deltaDistance;
    PlayerLocalController plc;

    // Use this for initialization
    void Start()
    {
        plc = FindObjectOfType<PlayerLocalController>();
    }

    void Update()
    {
        // Pinch to zoom
        if (Input.touchCount == 2)
        {
            // get current touch positions
            Touch tZero = Input.GetTouch(0);
            Touch tOne = Input.GetTouch(1);
            // get touch position from the previous frame
            Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
            Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;
            float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
            float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);
            // get offset value
            deltaDistance = oldTouchDistance - currentTouchDistance;
            plc.playLocalUserCamera._deltaDistance = deltaDistance;
        }
    }
}