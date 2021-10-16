using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class activeImageBird : MonoBehaviour
{
    public int stt;
    public Image[] listImage;
    public Image myself;
    public void activeMyself()
    {
        if (myself.gameObject.activeSelf)
        {
            myself.gameObject.SetActive(false);
            listBirds.birdEnabled = -1;
            FindObjectOfType<listBirds>().displayBird();
            return;
        }
        myself.gameObject.SetActive(true);
        listBirds.birdEnabled = stt; //selected
        for (int i = 0; i < listImage.Length; i++)
        {
            listImage[i].gameObject.SetActive(false);
        }
        //display bird
        //FindObjectOfType<listBirds>().displayBird();
    }
}
