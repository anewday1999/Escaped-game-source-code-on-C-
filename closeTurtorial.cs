using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class closeTurtorial : MonoBehaviour
{
    public Image turtorial;
    public void closetut()
    {
        turtorial.gameObject.SetActive(false);
    }
}
