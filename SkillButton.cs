using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SkillButton : MonoBehaviour
{
    public Button skillBtn;
    public Image cdImage;
    public PlayerLocalController plLController;

    [SerializeField]
    private float cdTime;
    private Transform tfPlayer;
    private void Awake()
    {
        cdTime = 0;
    }
    private void OnDisable()
    {
        cdTime = 0;
    }
    private void OnEnable()
    {
        cdTime = 0;
    }
    public void instantiateSkill()
    {
        cdTime = 15;
        skillBtn.interactable = false;
        cdImage.fillAmount = 0;
        tfPlayer = plLController.myLocalPlayer.transform;
        PhotonNetwork.Instantiate("skill", tfPlayer.position + tfPlayer.forward * 22 - tfPlayer.up * 2.8f, tfPlayer.rotation, 0);
    }
    private void Update()
    {
        if (cdTime > 0)
        {
            cdTime -= Time.deltaTime;
            cdImage.fillAmount = 1 - cdTime / 15;
        }
        else
        {
            skillBtn.interactable = true;
        }
    }
}
