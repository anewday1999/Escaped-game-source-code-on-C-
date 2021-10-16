using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Photon.Pun;

public class AppearanceManager : MonoBehaviour
{
    public CharacterController characterController;
    public GameObject[] props;
    public GameObject[] characters;
    public GameObject[] guns;
    public int countProps = 34;
    public int countCharacters = 8;
    public int countGuns = 1 + 1;//barrel: 1
    private void Start()
    {
        props = new GameObject[countProps];
        characters = new GameObject[countCharacters];
        guns = new GameObject[countGuns];
    }
    public void destroy()
    {
        for (int i = 0; i < countProps; i++)
        {
            props[i].SetActive(false);
        }
        for (int i = 0; i < countCharacters; i++)
        {
            characters[i].SetActive(false);
        }
        for (int i = 0; i < countGuns; i++)
        {
            guns[i].SetActive(false);
        }
    }
    public void getCharaControl(CharacterController cc)
    {
        characterController = cc;
    }
    public void getLocalProps(GameObject p, int index)
    {
        props[index] = p;
    }
    public void getLocalChacracters(GameObject c, int index)
    {
        characters[index] = c;
    }
    public void getLocalGuns(GameObject g, int index)
    {
        guns[index] = g;
    }
    [PunRPC]
    public void setAppearance()
    {
        //reset
        for (int i = 0; i < countProps; i++)
        {
            props[i].SetActive(false);
        }
        for (int i = 0; i < countCharacters; i++)
        {
            characters[i].SetActive(false);
        }
        for (int i = 0; i < countGuns; i++)
        {
            guns[i].SetActive(false);
        }
        //set
        if ((int)PhotonNetwork.LocalPlayer.CustomProperties["code"] == 1)
        {
            characterController.radius = 0.35f;
            characters[Random.Range(0, countCharacters)].SetActive(true);
            guns[Random.Range(0, countGuns - 1)].SetActive(true);
            guns[countGuns - 1].SetActive(true);
        }
        else if ((int)PhotonNetwork.LocalPlayer.CustomProperties["code"] != 0)
        {
            characterController.radius = 0.1f;
            props[Random.Range(0, countProps)].SetActive(true);
        }
    }
}
