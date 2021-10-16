using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class turnOffText : MonoBehaviour
{
    public float timeAlive;
    Text t;
    private void Start()
    {
        t = gameObject.GetComponent<Text>();
        timeAlive = 0f;
    }
    void Update()
    {
        timeAlive -= Time.deltaTime;
        if (timeAlive <= 0)
        {
            t.text = "";
        }
    }
}
