using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinButton : MonoBehaviour
{
    public void callJoinUi()
    {
        FindObjectOfType<networkmanager>().callJoinRoom();
    }
}
