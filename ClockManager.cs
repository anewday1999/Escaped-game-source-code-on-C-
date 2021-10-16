using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class ClockManager : MonoBehaviour
{
    private bool changeInMidle;
    private int propAlive;
    private int peopleAlive;

    public Button skillButton;
    public GameObject birds;
    public AdmobManager admobManager;
    public Text countPropsAndSeek;
    //public GameObject roomPannel;
    public Button chatButton;
    public GameObject chatField;
    public Button serverButton;
    public GameObject serverPannel;
    public Button unlock;
    public Dropdown listPlayer;
    public Text youWin;
    public Text youLose;
    public Text timer;
    public float currentTime;
    int minute;
    int second;
    public int byPlayer; //1 nguoi thang, 2 vat thang

    MatchManager mm;
    PlayerLocalController plc;
    // Start is called before the first frame update
    private void Start()
    {
        changeInMidle = true;
        mm = FindObjectOfType<MatchManager>();
        plc = FindObjectOfType<PlayerLocalController>();
    }
    void setTime(float t)
    {
        currentTime = t;
        changeInMidle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mm.Playing)
        {
            //if time out
            if (currentTime < 0)
            {
                timer.text = "Time out";
                Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["code"]);
                if (byPlayer == 1)
                {
                    if ((int)PhotonNetwork.LocalPlayer.CustomProperties["code"] == 2 || (int)PhotonNetwork.LocalPlayer.CustomProperties["code"] == 0)
                    {
                        youLose.gameObject.SetActive(true);
                    }
                    else if ((int)PhotonNetwork.LocalPlayer.CustomProperties["code"] == 1)
                    {
                        youWin.gameObject.SetActive(true);
                    }
                    Debug.Log("Seek won");
                }
                else if (byPlayer == 2)
                {
                    if ((int)PhotonNetwork.LocalPlayer.CustomProperties["code"] == 2 || (int)PhotonNetwork.LocalPlayer.CustomProperties["code"] == 0)
                    {
                        youWin.gameObject.SetActive(true);
                    }
                    else if ((int)PhotonNetwork.LocalPlayer.CustomProperties["code"] == 1)
                    {
                        youLose.gameObject.SetActive(true);
                        Debug.Log("2");

                    }
                    Debug.Log("Props won");

                }
                else if (byPlayer == 0)
                {
                    if ((int)PhotonNetwork.LocalPlayer.CustomProperties["code"] == 2 || (int)PhotonNetwork.LocalPlayer.CustomProperties["code"] == 0)
                    {
                        youWin.gameObject.SetActive(true);
                    }
                    else if ((int)PhotonNetwork.LocalPlayer.CustomProperties["code"] == 1)
                    {
                        youLose.gameObject.SetActive(true);
                        Debug.Log("Finish with time out");

                    }
                }
                
                if (/*PhotonNetwork.player.GetTeam() == PunTeams.Team.blue*/true)
                {
                    plc.changeSpeed(20);
                }
                mm.Playing = false;
                if (admobManager.gameObject.activeSelf == true)
                {
                    admobManager.callShowAd();
                }
                if (PhotonNetwork.IsConnected)
                {
                    PhotonNetwork.LocalPlayer.SetCustomProperties(TeamStaticVar.team0);
                }

            }
            else // if gaming
            {
                youWin.gameObject.SetActive(false);
                youLose.gameObject.SetActive(false);
                minute = (int)(currentTime / 60);
                second = (int)(currentTime % 60);
                if (currentTime >= 150 && currentTime <= 155)
                {
                    timer.color = Color.red;
                }
                else
                {
                    timer.color = Color.black;
                }
                if (currentTime <= 150 && changeInMidle)
                {
                    FindObjectOfType<AppearanceManager>().GetComponent<PhotonView>().RPC("setAppearance", Photon.Pun.RpcTarget.All);
                    changeInMidle = false;
                }
                if (second >= 10)
                {
                    timer.text = "0" + minute.ToString() + ":" + second.ToString();
                }
                else
                {
                    timer.text = "0" + minute.ToString() + ":0" + second.ToString();
                }
                
            }
            //caculation time
            if (currentTime - 0 > 0.00001)
            {
                currentTime -= Time.deltaTime;
            }
            //change player's option.
            if (mm.Playing)
            {
                if ((int)PhotonNetwork.LocalPlayer.CustomProperties["code"] == 1 || (int)PhotonNetwork.LocalPlayer.CustomProperties["code"] == 0)
                {
                    if (currentTime <= 330 && currentTime >= 329)
                    {
                        plc.changeActive(false);
                    }
                    else if (currentTime <= 300 && currentTime >= 299)
                    {
                        plc.changeActive(true);
                    }
                }
                else
                {
                    if (currentTime <= 330 && currentTime >= 329)
                    {
                        plc.changeSpeed(12);
                    }
                    else if (currentTime <= 300 && currentTime >= 299)
                    {
                        plc.changeSpeed(1.5f);
                    }
                    if (currentTime <= 150 && currentTime >= 149)
                    {
                        plc.changeSpeed(12);
                    }
                    else if (currentTime <= 140 && currentTime >= 139)
                    {
                        plc.changeSpeed(1.5f);
                    }
                }
            }
        }
        if (PhotonNetwork.InRoom)
        {
            if ((int)PhotonNetwork.LocalPlayer.CustomProperties["code"] == 2)
            {
                skillButton.gameObject.SetActive(false);
            }
            else
            {
                plc.namelable.gameObject.SetActive(true);
                skillButton.gameObject.SetActive(true);
            }
            countPropsAndSeek.gameObject.SetActive(true);
            chatButton.gameObject.SetActive(true);
            if (mm.Playing == false)
            {
                listPlayer.gameObject.SetActive(false);
                plc.parSys.gameObject.SetActive(false);
            }
            else
            {
                if((int)PhotonNetwork.LocalPlayer.CustomProperties["code"] == 2)
                {
                    if (birds != null)
                    {
                        birds.SetActive(false);
                    }
                    plc.parSys.gameObject.SetActive(true);
                    if (unlock.IsActive())
                    {
                        listPlayer.gameObject.SetActive(true);
                    } 
                    else
                    {
                        listPlayer.gameObject.SetActive(false);
                        plc.GetComponent<PhotonView>().RPC("setCameraToMyself", Photon.Pun.RpcTarget.All);
                    }
                }
                else
                {
                    if (birds != null)
                    {
                        birds.SetActive(true);
                    }
                }
            }
        }
        else
        {
            countPropsAndSeek.gameObject.SetActive(false);
            chatButton.gameObject.SetActive(false);
            chatField.SetActive(false);
        }
    }
    void LateUpdate()
    {
        foreach(Player p in PhotonNetwork.PlayerList)
        {
            if (p.CustomProperties != null)
                Debug.Log(p.NickName + p.CustomProperties["code"]);
        }
        if (PhotonNetwork.InRoom && mm.Playing && currentTime <= 320)
        {
            if (true)
            {
                propAlive = 0;
                peopleAlive = 0;
                foreach(Player p in PhotonNetwork.PlayerList)
                {
                    if ((int)p.CustomProperties["code"] == 2)
                    {
                        propAlive++;
                    }
                    else if((int)p.CustomProperties["code"] == 1)
                    {
                        peopleAlive++;
                    }
                }
                Debug.Log(propAlive + " " + peopleAlive);
                if (propAlive == 0)
                {
                    /*if (PhotonNetwork.player.GetTeam() == PunTeams.Team.blue)
                    {
                        youWin.gameObject.SetActive(true);
                    }
                    else
                    {
                        youLose.gameObject.SetActive(true);
                    }*/
                    currentTime = -1;
                    byPlayer = 1;
                    Debug.Log("byplayer1");
                    /*if (admobManager.gameObject.activeSelf == true)
                    {
                        admobManager.callShowAd();
                    }
                    FindObjectOfType<MatchManager>().Playing = false;*/
                }
                if(peopleAlive == 0)
                {
                    /*
                    if (PhotonNetwork.player.GetTeam() == PunTeams.Team.red)
                    {
                        youWin.gameObject.SetActive(true);
                    }
                    else
                    {
                        youLose.gameObject.SetActive(true);
                    }*/
                    currentTime = -1;
                    byPlayer = 2;
                    Debug.Log("byplayer2");

                    /*
                    if (admobManager.gameObject.activeSelf == true)
                    {
                        admobManager.callShowAd();
                    }
                    FindObjectOfType<MatchManager>().Playing = false;
                    */
                }
            }
        }
    }
}
