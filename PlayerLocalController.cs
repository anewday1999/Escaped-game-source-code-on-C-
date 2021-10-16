using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLocalController : MonoBehaviour
{
    public Image cdImage;
    public Button skillButton;
    public GameObject ObjectsMiniGame;
    public Text CountDown;

    public TextMeshPro namelable;
    public GameObject myLocalPlayer;
    public UserMovement playLocalUserMovement;
    public UserCamera playLocalUserCamera;
    public ParticleSystem parSys;

    public Text mySpped;
    public Image waitBg;

    public GameObject cm;
    ClockManager cmCmponent;
    float curTime;
    void Start()
    {
        cmCmponent = cm.GetComponent<ClockManager>();
    }
    [PunRPC]
    public void setCameraToMyself()
    {
        playLocalUserCamera.target = myLocalPlayer.transform;
    }
    [PunRPC]
    public void resetScale()
    {
        Debug.Log("rescale");
        myLocalPlayer.transform.localScale = new Vector3(1, 1, 1);
    }
    public void changeSpeed(float sp)
    {
        if (FindObjectOfType<MatchManager>().Playing)
        {
            mySpped.text = playLocalUserMovement.runSpeed + "km/h";
        }
        else
        {
            mySpped.text = "";
        }
        playLocalUserMovement.runSpeed = sp;
    }
    public void changeActive(bool isActive)
    {
        waitBg.gameObject.SetActive(!isActive);
        playLocalUserMovement.enabled = isActive;
    }

}
