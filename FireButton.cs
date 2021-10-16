using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireButton : MonoBehaviour
{
    public playerShooting plerShooting;
    public void clickfire()
    {
        plerShooting.callfire();
    }
}
