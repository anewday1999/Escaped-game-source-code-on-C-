using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseChatWhenOpenSetting : MonoBehaviour
{
    public GameObject chat;
    public void onClickOpenSetting()
    {
        if (chat != null)
        {
            chat.SetActive(false);
        }
    }
}
