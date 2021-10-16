using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectButton : MonoBehaviour
{
    [SerializeField]
    Text connectingText;
    public GameObject playernamee;
    public void connectcall()
    {
        connectingText.gameObject.SetActive(true);
        FindObjectOfType<networkmanager>().callConnect();
        playernamee.SetActive(false);
    }
}
