using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reloadRoom : MonoBehaviour
{
    float timeReload;
    networkmanager netma;
    private void Start()
    {
        timeReload = 0;
        netma = FindObjectOfType<networkmanager>();
    }
    public void reLoad()
    {
        netma.printListRoom();
    }
    private void Update()
    {
        
        if (timeReload <= 0)
        {
            netma.printListRoom();
            timeReload = 2f;
        }
        else
        {
            timeReload -= Time.deltaTime;
        }
    }
}
