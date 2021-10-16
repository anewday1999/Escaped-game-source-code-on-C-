using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class pun_chat : MonoBehaviour
{
    public GameObject chatDisplay, textObject;

    [System.Serializable]
    public class ChatMessage
    {
        public string sender = "";
        public string message = "";
        public Color color;
        public Text textObject;
    }

    List<ChatMessage> chatMessages = new List<ChatMessage>();

    void Start()
    {
    }
    void addMessage()
    {
        GameObject newText = Instantiate(textObject, chatDisplay.transform);
        chatMessages[0].textObject = newText.GetComponent<Text>();
        newText.GetComponent<Text>().text = chatMessages[0].sender +": " + chatMessages[0].message;
        newText.GetComponent<Text>().color = chatMessages[0].color;
    }
    [PunRPC]
    void SendChat(Photon.Realtime.Player sender, string message)
    {
        if (sender.CustomProperties["code"] == PhotonNetwork.LocalPlayer.CustomProperties["code"] || (int)PhotonNetwork.LocalPlayer.CustomProperties["code"] == 0 || (int)PhotonNetwork.LocalPlayer.CustomProperties["code"] == 2)
        {
            ChatMessage m = new ChatMessage();
            m.sender = sender.NickName;
            m.message = message;
            if((int)sender.CustomProperties["code"] == 1)
            {
                m.color = Color.blue;
            }
            else if ((int)sender.CustomProperties["code"] == 2)
            {
                m.color = Color.red;
            }
            else
            {
                m.color = Color.black;
            }

            chatMessages.Insert(0, m);
            if (chatMessages.Count > 30)
            {
                Destroy(chatMessages[chatMessages.Count - 1].textObject);
                chatMessages.RemoveAt(chatMessages.Count - 1);
            }
            addMessage();
        }
        else
        {  
        }
        
    }
}