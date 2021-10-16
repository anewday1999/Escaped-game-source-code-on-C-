using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSlideColor : MonoBehaviour
{
    private float timeOut = 22;
    [SerializeField]
    Text time;
    [SerializeField]
    Text connectingText;
    private float timmer = 1;
    private int dots = 1;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (timeOut >= 0)
        {
            if (timmer >= 0)
            {
                timmer -= Time.deltaTime;
            }
            else
            {
                if (dots == 1)
                {
                    connectingText.text = "Connecting to server.";
                }
                else if (dots == 2)
                {
                    connectingText.text = "Connecting to server..";
                }
                else
                {
                    connectingText.text = "Connecting to server...";
                }

                if (dots == 3)
                {
                    dots = 1;
                }
                else
                {
                    dots++;
                }

                timmer = 1;
                time.text = timeOut.ToString();
                timeOut -= 1;
            }
        }
        else
        {
            connectingText.text = "Can't connect to server, check your connect and reset app !!!";
            time.text = "Time out.";
            time.color = Color.red;
            connectingText.color = Color.red;
        }
    }
}
