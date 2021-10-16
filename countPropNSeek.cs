using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class countPropNSeek : MonoBehaviour
{
    public Text myself;
    List<Player> listP;
    int props;
    int seek;
    int none;
    void Awake()
    {
        Debug.Log("awake");
        listP = new List<Player>();
    }
    private void Update()
    {
        props = 0;
        seek = 0;
        none = 0;
        listP.Clear();
        foreach(KeyValuePair<int, Player> p in PhotonNetwork.CurrentRoom.Players)
        {
            listP.Add(p.Value);
        }

        foreach(Player p in listP)
        {
            if (p.CustomProperties["code"] != null)
            {
                if ((int)p.CustomProperties["code"] == 2)
                {
                    props++;
                }
                else if ((int)p.CustomProperties["code"] == 1)
                {
                    seek++;
                }
                else
                {
                    none++;
                }
            }
            else
            {
                Debug.Log("havent set properties");
            }
        }
        if (seek != 0 && props != 0)
        {
            myself.text = seek + " seeks : " + props + "props";
        }
        else
            myself.text = none + "players";
    }
}
