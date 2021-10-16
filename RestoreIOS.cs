using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestoreIOS : MonoBehaviour
{
    public Button mySelf;
    private void Start()
    {
        #if UNITY_IOS
            mySelf.gameObject.SetActive(true);
        #else
            mySelf.gameObject.SetActive(false);
        #endif

    }
}
