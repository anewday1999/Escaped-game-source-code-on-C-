using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openShopPannel : MonoBehaviour
{
    public GameObject pannel;
    public void buttonShop()
    {
        pannel.SetActive(!pannel.activeSelf);
    }
    public void onclickExit()
    {
        pannel.SetActive(false);
    }
}
