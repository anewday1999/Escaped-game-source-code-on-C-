using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameLable : MonoBehaviour
{
    public GameObject nameLable;
    private void Start()
    {
    }
    private void LateUpdate()
    {
        nameLable.transform.rotation = Camera.main.transform.rotation;
    }
}
