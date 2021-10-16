using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioEvent : MonoBehaviour
{
    private void Start()
    {
        
    }
    void stepFoot()
    {
        FindObjectOfType<FXManager>().callFootStep(this.gameObject.transform.position);
    }
}
