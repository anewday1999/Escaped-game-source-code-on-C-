using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

//On my local character
public class MinigameController : MonoBehaviour
{
    private Image _cdImage;
    private Button _skillButton;

    public bool isInMG;
    private int countChild;
    public float currentTime = 0;
    private Text countDown;

    private GameObject _myLocalPlayer;
    private void OnDisable()
    {
        isInMG = false;
    }
    private void OnEnable()
    {
        isInMG = false;
    }
    private void OnDestroy()
    {
        isInMG = false;
    }
    private void Start()
    {
        isInMG = false;
    }
    public void resetAll()
    {
        FindObjectOfType<PlayerLocalController>().ObjectsMiniGame.SetActive(false);
        for (int i = 0; i < countChild; i++)
        {
            FindObjectOfType<PlayerLocalController>().ObjectsMiniGame.transform.GetChild(i).gameObject.SetActive(false);
        }
        if (countDown == null)
        {
            countDown = FindObjectOfType<PlayerLocalController>().CountDown;
        }
        countDown.gameObject.SetActive(false);
    }
    private void releaseObject(Color cl, int num)
    {
        GameObject tmpButton = FindObjectOfType<PlayerLocalController>().ObjectsMiniGame.transform.GetChild(Random.Range(0, countChild)).gameObject;
        while (tmpButton.activeSelf == true)
        {
            tmpButton = FindObjectOfType<PlayerLocalController>().ObjectsMiniGame.transform.GetChild(Random.Range(0, countChild)).gameObject;
        }
        tmpButton.SetActive(true);
        tmpButton.GetComponent<Image>().color = cl;
        tmpButton.transform.Find("Text").GetComponent<Text>().text = num.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isInMG && (int)PhotonNetwork.LocalPlayer.CustomProperties["code"] == 2)
        {
            if (SaverListChosen.ListChosen != null)
            {
                SaverListChosen.ListChosen.Clear();
            }
            countChild = FindObjectOfType<PlayerLocalController>().ObjectsMiniGame.transform.childCount;
            resetAll();

            FindObjectOfType<PlayerLocalController>().ObjectsMiniGame.SetActive(true);
            if (countDown == null)
            {
                countDown = FindObjectOfType<PlayerLocalController>().CountDown;
            }
            countDown.gameObject.SetActive(true);
            currentTime = 7.5f;

            //red

            releaseObject(Color.red, 1);
            releaseObject(Color.red, 1);

            //blue

            releaseObject(Color.blue, 2);
            releaseObject(Color.blue, 2);

            //yellow

            releaseObject(Color.yellow, 3);
            releaseObject(Color.yellow, 3);

            //green

            releaseObject(Color.green, 4);
            releaseObject(Color.green, 4);

            //white

            releaseObject(Color.white, 5);
            releaseObject(Color.white, 5);
        }
    }
    public bool isTrueResult()
    {
        string[] listChosen = SaverListChosen.ListChosen.ToArray();
        if (listChosen.Length == 10)
        {
            if (listChosen[0] != listChosen[1])
                return false;
            if (listChosen[2] != listChosen[3])
                return false;
            if (listChosen[4] != listChosen[5])
                return false;
            if (listChosen[6] != listChosen[7])
                return false;
            if (listChosen[8] != listChosen[9])
                return false;
        }
        else
        {
            return false;
        }
        return true;
    }
    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            isInMG = true;
        }
        else
        {
            resetAll();
            if (isInMG == true)
            {
                if (!isTrueResult())
                {
                    //hinhphat
                    FindObjectOfType<FXManager>().GetComponent<PhotonView>().RPC("failMiniGame", Photon.Pun.RpcTarget.All, this.gameObject.transform.position);
                    _myLocalPlayer = FindObjectOfType<PlayerLocalController>().myLocalPlayer;
                    if (Random.Range(0, 2) == 0)
                    {
                        _myLocalPlayer.transform.localScale = new Vector3(_myLocalPlayer.transform.localScale.x * 2.5f, _myLocalPlayer.transform.localScale.y, _myLocalPlayer.transform.localScale.z * 2.5f);
                    }
                    else
                    {
                        _myLocalPlayer.transform.localScale = new Vector3(_myLocalPlayer.transform.localScale.x , _myLocalPlayer.transform.localScale.y * 2.5f, _myLocalPlayer.transform.localScale.z);
                    }
                    
                }
            }
            
            isInMG = false;
        }
        if ((int)(currentTime % 60) != 0)
        {
            if (countDown == null)
            {
                countDown = FindObjectOfType<PlayerLocalController>().CountDown;
            }
            countDown.text = ((int)(currentTime % 60)).ToString();
        }
        else
        {
            if (countDown == null)
            {
                countDown = FindObjectOfType<PlayerLocalController>().CountDown;
            }
            countDown.text = "Time out";
        }
        
    }
}
