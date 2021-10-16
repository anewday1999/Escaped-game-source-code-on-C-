using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCollider : MonoBehaviour
{
    public ParticleSystem ring;
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "props")
        {
            ParticleSystem.MainModule ringMain = ring.main;
            ringMain.startColor = Color.red;
        }
    }

}
