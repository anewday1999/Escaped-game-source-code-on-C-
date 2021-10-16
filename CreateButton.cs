using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateButton : MonoBehaviour
{
    public Text info;
    public Text nameroom;
    public void callCreateUi()
    {
        FindObjectOfType<networkmanager>().callCreateRoom();
    }
    private void Update()
    {
        if (nameroom.text != "")
        {
            info.text = nameroom.text + ", " + RoomSetting.maxPlayer + ", " + (RoomSetting.isPrivate ? "private room" : "not private");
        }
        else
        {
            info.text = RoomSetting.maxPlayer + ", " + (RoomSetting.isPrivate ? "private room" : "not private");
        }
        
    }
}
