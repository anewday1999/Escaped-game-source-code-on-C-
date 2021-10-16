using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class changeServerOnClick : MonoBehaviour
{
    public string nameServerWantToSwitch;

    public void changeServer()
    {
        Debug.Log(nameServerWantToSwitch + PhotonNetwork.CloudRegion);
        reConnect();
    }
    void reConnect()
    {
        if (nameServerWantToSwitch == "eu")
        {
            FindObjectOfType<networkmanager>().nameServer = "eu";
        }
        if (nameServerWantToSwitch == "us")
        {
            FindObjectOfType<networkmanager>().nameServer = "us";
        }
        if (nameServerWantToSwitch == "asia")
        {
            FindObjectOfType<networkmanager>().nameServer = "asia";
        }
        if (nameServerWantToSwitch == "jp")
        {
            FindObjectOfType<networkmanager>().nameServer = "jp";
        }
        if (nameServerWantToSwitch == "au")
        {
            FindObjectOfType<networkmanager>().nameServer = "au";
        }
        if (nameServerWantToSwitch == "usw")
        {
            FindObjectOfType<networkmanager>().nameServer = "usw";
        }
        if (nameServerWantToSwitch == "sa")
        {
            FindObjectOfType<networkmanager>().nameServer = "sa";
        }
        if (nameServerWantToSwitch == "cae")
        {
            FindObjectOfType<networkmanager>().nameServer = "cae";
        }
        if (nameServerWantToSwitch == "kr")
        {
            FindObjectOfType<networkmanager>().nameServer = "kr";
        }
        if (nameServerWantToSwitch == "@in")
        {
            FindObjectOfType<networkmanager>().nameServer = "@in";
        }
        if (nameServerWantToSwitch == "ru")
        {
            FindObjectOfType<networkmanager>().nameServer = "ru";
        }
        if (nameServerWantToSwitch == "rue")
        {
            FindObjectOfType<networkmanager>().nameServer = "rue";
        }

    }

}
