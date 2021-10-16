using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestruct : MonoBehaviour
{
    public float timeAlive = 10;
    void Update()
    {
        timeAlive -= Time.deltaTime;
        if (timeAlive <= 0)
        {
            Destroy(gameObject);
        }
    }
}
