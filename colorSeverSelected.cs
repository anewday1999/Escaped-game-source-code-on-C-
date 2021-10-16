using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colorSeverSelected : MonoBehaviour
{
    public Button[] servers;
    private void changeColor(string kromeButton)
    {
        for (int i = 0; i < 12; i++)
        {
            if (servers[i].name == kromeButton)
            {
                servers[i].image.color = Color.red;
            }
            else
            {
                servers[i].image.color = Color.green;
            }
        }
    }
    private void Update()
    {

        changeColor(FindObjectOfType<networkmanager>().nameServer.ToString());
    }
}
