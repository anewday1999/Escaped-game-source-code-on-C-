using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public Text ratio;
    private float fulltime = 4.5f;
    public Image bar;
    public Image LoadingStart;
    void Start()
    {
        ratio.text = "0%";
        bar.fillAmount = 0;
        fulltime = 4.5f;
    }

    void OnEnable()
    {
        ratio.text = "0%";
        bar.fillAmount = 0;
        fulltime = 4.5f;
    }

    void Update()
    {
        if (fulltime <= 0)
        {
            LoadingStart.gameObject.SetActive(false);
            fulltime = 4.5f;
        }
        else
        {
            fulltime -= Time.deltaTime;
            bar.fillAmount = (float) 1 - (fulltime / 4.5f);
            ratio.text = Mathf.RoundToInt((float) (1 - (fulltime / 4.5f)) * 100 ).ToString() + "%";
        }
    }
}
