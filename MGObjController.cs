using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MGObjController : MonoBehaviour
{
    public GameObject mySelf;
    public void onClickMe()
    {
        mySelf = this.gameObject;
        SaverListChosen.ListChosen.Add(mySelf.transform.Find("Text").GetComponent<Text>().text);
        mySelf.SetActive(false);
    }
}
