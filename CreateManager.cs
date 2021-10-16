using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateManager : MonoBehaviour
{
    public Toggle isPrivateToggle;
    public void set2()
    {
        RoomSetting.maxPlayer = 2;
        RoomSetting.amountFinder = 1;
        RoomSetting.amountHider = 1;
    }
    public void set7()
    {
        RoomSetting.maxPlayer = 7;
        RoomSetting.amountFinder = 2;
        RoomSetting.amountHider = 5;
    }
    public void set11()
    {
        RoomSetting.maxPlayer = 11;
        RoomSetting.amountFinder = 3;
        RoomSetting.amountHider = 8;
    }
    public void set15()
    {
        RoomSetting.maxPlayer = 15;
        RoomSetting.amountFinder = 4;
        RoomSetting.amountHider = 11;
    }
    public void setPrivate()
    {
        RoomSetting.isPrivate = isPrivateToggle.isOn;
        Debug.Log(RoomSetting.isPrivate);
    }
}
