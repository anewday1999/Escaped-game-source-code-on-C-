using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{
    public InputField nameJoin;
    public Text roomExisting;
    public Text[] button;
    public Button prev;
    public Button next;
    public List<RoomInfo> rI;
    public int lengthRI;
    int curPage = 1;
    int lastPage = 1;

    bool isPrivate(Photon.Realtime.RoomInfo ri)
    {
        if (ri.Name.Contains("private"))
        {
            return true;
        }
        return false;
    }
    public void caculatorPage()
    {
        for (int i = 0; i < 10; i++)
        {
            button[i].text = "";
        }
        lastPage = (int)((lengthRI / 10) + 1);
        if (lengthRI > 0)
        {
            listing(1);
        }
        curPage = 1;
    }
    public void listing(int cP)
    {
        int index;
        if (cP < lastPage)
        {
            for (int i = 0; i < 10; i++)
            {
                index = 10 * (cP - 1) + i;
                if (!isPrivate(rI[index]))
                {
                    button[i].text = rI[index].Name + ",   " + rI[index].PlayerCount + "/" + rI[index].MaxPlayers;
                }
                else
                {
                    button[i].text = "Room is Private";
                }
            }
        }
        else if (cP == lastPage)
        {
            for (int i = 0; i < lengthRI - ((cP - 1) * 10); i++)
            {
                index = 10 * (cP - 1) + i;
                if (index < lengthRI)
                {
                    if (!isPrivate(rI[index]))
                    {
                        button[i].text = rI[index].Name + ",   " + rI[index].PlayerCount + "/" + rI[index].MaxPlayers;
                    }
                    else
                    {
                        button[i].text = "Room is Private";
                    }
                }
                
            }
            for (int i = lengthRI - ((cP - 1) * 10); i < 10; i++)
            {
                button[i].text = "";
            }
        }
    }
    public void slidePrev()
    {
        curPage -= 1;
        listing(curPage);
    }
    public void slideNext()
    {
        curPage += 1;
        listing(curPage);
    }
    void reColor(int j)
    {
        for (int i = 0; i < 10; i++)
        {
            if (i == j)
            {
                button[i].color = Color.red;
            }
            else
            {
                button[i].color = Color.black;
            }
        }
    }
    public void selecting0()
    {
        if (10 * (curPage - 1) + 0 < lengthRI && !isPrivate(rI[10 * (curPage - 1) + 0]))
        {
            nameJoin.text = rI[10 * (curPage - 1) + 0].Name;
        }
        reColor(0);
    }
    public void selecting1()
    {
        if (10 * (curPage - 1) + 1 < lengthRI && !isPrivate(rI[10 * (curPage - 1) + 0]))
        {
            nameJoin.text = rI[10 * (curPage - 1) + 1].Name;
        }
        reColor(1);
    }
    public void selecting2()
    {
        if (10 * (curPage - 1) + 2 < lengthRI && !isPrivate(rI[10 * (curPage - 1) + 0]))
        {
            nameJoin.text = rI[10 * (curPage - 1) + 2].Name;
        }
        reColor(2);
    }
    public void selecting3()
    {
        if (10 * (curPage - 1) + 3 < lengthRI && !isPrivate(rI[10 * (curPage - 1) + 0]))
        {
            nameJoin.text = rI[10 * (curPage - 1) + 3].Name;
        }
        reColor(3);
    }
    public void selecting4()
    {
        if (10 * (curPage - 1) + 4 < lengthRI && !isPrivate(rI[10 * (curPage - 1) + 0]))
        {
            nameJoin.text = rI[10 * (curPage - 1) + 4].Name;
        }
        reColor(4);
    }
    public void selecting5()
    {
        if (10 * (curPage - 1) + 5 < lengthRI && !isPrivate(rI[10 * (curPage - 1) + 0]))
        {
            nameJoin.text = rI[10 * (curPage - 1) + 5].Name;
        }
        reColor(5);
    }
    public void selecting6()
    {
        if (10 * (curPage - 1) + 6 < lengthRI && !isPrivate(rI[10 * (curPage - 1) + 0]))
        {
            nameJoin.text = rI[10 * (curPage - 1) + 6].Name;
        }
        reColor(6);
    }
    public void selecting7()
    {
        if (10 * (curPage - 1) + 7 < lengthRI && !isPrivate(rI[10 * (curPage - 1) + 0]))
        {
            nameJoin.text = rI[10 * (curPage - 1) + 7].Name;
        }
        reColor(7);
    }
    public void selecting8()
    {
        if (10 * (curPage - 1) + 8 < lengthRI && !isPrivate(rI[10 * (curPage - 1) + 0]))
        {
            nameJoin.text = rI[10 * (curPage - 1) + 8].Name;
        }
        reColor(8);
    }
    public void selecting9()
    {
        if (10 * (curPage - 1) + 9 < lengthRI && !isPrivate(rI[10 * (curPage - 1) + 0]))
        {
            nameJoin.text = rI[10 * (curPage - 1) + 9].Name;
        }
        reColor(9);
    }
    void Update()
    {
        if (PhotonNetwork.InLobby)
        {
            roomExisting.text = "RoomExisting: " + (int)(PhotonNetwork.CountOfRooms + (int)2) + " (all of kind)";
        }
        else
        {
            roomExisting.text = "Press let's go";
        }
        if (curPage <= 1)
        {
            curPage = 1;
            prev.interactable = false;
            next.interactable = true;
        }
        else if (curPage >= lastPage)
        {
            curPage = lastPage;
            next.interactable = false;
            prev.interactable = true;
        }
        else
        {
            next.interactable = true;
            prev.interactable = true;
        }
    }

}
