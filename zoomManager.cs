using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zoomManager : MonoBehaviour
{
    public Slider myself;
    public PlayerLocalController plLocalControl;

    public void onChangeValue()
    {
        if (plLocalControl.playLocalUserCamera != null)
        {
            plLocalControl.playLocalUserCamera._deltaDistance = myself.value;
        }
    }
}
