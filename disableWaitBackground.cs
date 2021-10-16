using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class disableWaitBackground : MonoBehaviour
{
    public Image waitBg;
    void Update()
    {
        waitBg.gameObject.SetActive(false);
    }
}
