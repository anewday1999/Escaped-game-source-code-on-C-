using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeColorA : MonoBehaviour
{
    public Text myself;
    private Color color;
    private void OnEnable()
    {
        color = myself.color;
        color.a = 0;
    }
    private void Update()
    {
        color.a = Mathf.Lerp(color.a, 1, 0.025f);
        myself.color = color;
    }
}
