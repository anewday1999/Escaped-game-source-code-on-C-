using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class listBirds : MonoBehaviour
{

    //GameObject
    public Purchaser purchaserScript;
    public GameObject adsManager;
    public bool[] birds = new bool[4];
    //UI
    public Button[] listbuttonBuy;
    public Button Ads;
    public Button[] _listbirds;
    public Image[] _listTick;
    public static int birdEnabled = -1;
    //Infomations
    public static bool[] _listBirds = new bool[4];
    public static int isBoughtAd = 0;
    public void displayBird()
    {
        //display bird
        if (birdEnabled != -1)
        {
            if (birdEnabled == 0 && _listBirds[0])
                birds[birdEnabled] = true;
            if (birdEnabled == 1 && _listBirds[1])
                birds[birdEnabled] = true;
            if (birdEnabled == 2 && _listBirds[2])
                birds[birdEnabled] = true;
            if (birdEnabled == 3 && _listBirds[3])
                birds[birdEnabled] = true;
        }

        for (int i = 0; i < 4; i++)
        {
            if (i != birdEnabled)
            {
                birds[i] = false;
            }
        }
    }
    void setStart()
    {
        //ad
        //isBoughtAd = PlayerPrefs.GetInt("isBoughtAd");
        //selected
        birdEnabled = PlayerPrefs.GetInt("birdEnabled");
        //tick

        /*
        if (PlayerPrefs.GetInt("bird0") == 1)
        {
            _listBirds[0] = true;
        }
        else
        {
            _listBirds[0] = false;
        }
        if (PlayerPrefs.GetInt("bird1") == 1)
        {
            _listBirds[1] = true;
        }
        else
        {
            _listBirds[1] = false;
        }
        if (PlayerPrefs.GetInt("bird2") == 1)
        {
            _listBirds[2] = true;
        }
        else
        {
            _listBirds[2] = false;
        }
        if (PlayerPrefs.GetInt("bird3") == 1)
        {
            _listBirds[3] = true;
        }
        else
        {
            _listBirds[3] = false;
        }*/
        /*
        Debug.Log(birdEnabled);
        for (int i = 0; i < 4; i++)
        {
            Debug.Log(_listBirds[i]);
        }
        */
    }
    public void UpdateFromPurchaser()
    {
        displayBird();
        if (isBoughtAd == 1)
        {
            adsManager.SetActive(false);
            Ads.gameObject.SetActive(false);
        }
        for (int i = 0; i < 4; i++)
        {
            _listbirds[i].interactable = _listBirds[i];
            listbuttonBuy[i].gameObject.SetActive(!_listBirds[i]);
        }
    }
    private void Start()
    {

        setStart();
        displayBird();
        if (birdEnabled != -1)
        {
            _listTick[birdEnabled].gameObject.SetActive(true);
            for (int i = 0; i < 4; i++)
            {
                if (i != birdEnabled)
                    _listTick[i].gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                _listTick[i].gameObject.SetActive(false);
            }
        }
    }
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            PlayerPrefs.SetInt("birdEnabled", birdEnabled);
        }

    }
}
