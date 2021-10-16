using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSettingOnOpenChat : MonoBehaviour
{
    public GameObject setting;
    public void onClickOpenChat()
    {
        if (setting != null)
        {
            setting.SetActive(false);
        }
    }
}
