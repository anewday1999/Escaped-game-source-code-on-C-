using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ListPlayerManager : MonoBehaviourPunCallbacks, IMatchmakingCallbacks, IInRoomCallbacks
{
    public UiListPlayerController UiList;
    private float timeUpdate;
    private bool isUpdate = false;

    void IMatchmakingCallbacks.OnJoinedRoom()
    {
        UiList.getListPlayer();
        timeUpdate = 2;
        isUpdate = true;
        UiList.setSlots();
    }
    void IInRoomCallbacks.OnPlayerEnteredRoom(Player newPlayer)
    {
        UiList.getListPlayer();
        timeUpdate = 2;
        isUpdate = true;
        UiList.setSlots();
    }
    private void Update()
    {
        if (isUpdate)
        {
            if (timeUpdate <= 0)
            {
                UiList.listPlayerInLocal = GameObject.FindGameObjectsWithTag("Player");
                isUpdate = false;
            }
            if (timeUpdate >= 0)
            {
                timeUpdate -= Time.deltaTime;
            }
        }
        
    }
}
