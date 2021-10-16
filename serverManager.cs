using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class serverManager : MonoBehaviour
{
    public Button buttonSetting;
    public GameObject pannel;
    public void openPannel()
    {
        pannel.SetActive(!pannel.activeSelf);
    }
}
