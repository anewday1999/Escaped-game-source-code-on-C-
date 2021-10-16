using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class chatFieldManager : MonoBehaviour
{
    public InputField chatInput;
    public Button clickSend;
    public pun_chat punChat;
    private void Start()
    {
    }
    public void sendChat()
    {
        if (chatInput.text.Replace(" ", "") != "")
        {
            punChat.GetComponent<PhotonView>().RPC("SendChat", Photon.Pun.RpcTarget.All, PhotonNetwork.LocalPlayer, chatInput.text);
            chatInput.text = "";
        }
        
    }
}
