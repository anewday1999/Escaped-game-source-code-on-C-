using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Photon.Pun.UtilityScripts;
//using UnityEngine.UIElements;
//using UnityScript.Steps;

public class networkmanager : MonoBehaviourPunCallbacks, IConnectionCallbacks, ILobbyCallbacks, IMatchmakingCallbacks, IInRoomCallbacks
{
    public GameObject turtorial;
    public serverManager svManger;
    public Text log;
    [SerializeField]
    Text connectingText;
    public GameObject timer;

    public Button skillButton;
    public Slider zoomUI;
    public InputField playerNameInputField;
    public InputField nameRoomToJoinInputField;
    public InputField nameRoomToCreateField;

    public GameObject joinObj;
    public GameObject createObj;

    public Button firebutton;
    public Button jumpButton;
    public GameObject RotateArea;
    public GameObject joyStickMove;
    public Text countPlayer;
    public string nameServer;
    public GameObject lockAndUnlock;
    public Dropdown listPlayer;
    public Button startButton;
    public Text win;
    public Text lose;
    public Camera standbyCamera;
    public SpawnSpot[] spawnSpots;
    public Text roomExistText;
    public GameObject roomListPannel;
    public Button _shop;
    public GameObject shopPannel;
    public string nameRoomToJoin;
    public bool offline;
    public float respawnTime = 0;

    bool connecting = false;
    //List<string> noticese;
    List<RoomInfo> roomList;
    bool createRoom;
    bool joinRoom;
    float delayLoadRoom;
    bool onJoinedLobby;
    //PhotonPlayer[] playerList;
    string playerName;


    void Start()
    {
        roomList = new List<RoomInfo>();
        nameServer = "none";
        spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
        playerName = PlayerPrefs.GetString("Player's name:");
        playerNameInputField.text = playerName;
        //noticese = new List<string>();
        createRoom = false;
        joinRoom = false;
    }

    // 4 off mode
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            PlayerPrefs.SetString("Player's name:", playerName);
        }
        
    }
    void connect()
    {
        PhotonNetwork.PhotonServerSettings.AppSettings.Protocol = ExitGames.Client.Photon.ConnectionProtocol.Tcp;
        PhotonNetwork.PhotonServerSettings.AppSettings.Port = 0;
        
        PhotonNetwork.GameVersion = "v2.5";
        PhotonNetwork.OfflineMode = false;
        if (nameServer != "none")
        {
            Debug.Log("connecting by chosen");
            PhotonNetwork.NetworkingClient.AppId = "16e1e2b5-410c-4ad3-9c09-a9f257674eb8";
            PhotonNetwork.ConnectToRegion(nameServer);
        }
        else
        {
            Debug.Log("connecting to best region");
            PhotonNetwork.ConnectUsingSettings();
        }
        //PhotonNetwork.gameVersion = "1.0.3";
        //PhotonNetwork.ConnectToMaster("195.19.34.170", 5055, "ded30ffb-e7d4-4937-bddd-f6de86cd9944", "1.0.3");
    }

    [PunRPC]
    void sendRoomSeting(bool iPri, byte maxPl, string name)
    {
        RoomSetting.isPrivate = iPri;
        RoomSetting.roomName = name;
        RoomSetting.maxPlayer = maxPl;
    }
    void resetRoomSetting()
    {
        RoomSetting.isPrivate = false;
        RoomSetting.roomName = null;
        RoomSetting.maxPlayer = 0;
    }
    /*
    void addNotice(string n)
    {
        GetComponent<PhotonView>().RPC("addNotice_RPC", PhotonTargets.AllBuffered, n);
    }
    [PunRPC]
    void addNotice_RPC(string n)
    {
        
        while (noticese.Count >= 5)
        {
            noticese.RemoveAt(0);
        }
        noticese.Add(n);
    }*/
    public override void OnRoomListUpdate(List<RoomInfo> rL)
    {
        if (PhotonNetwork.InLobby)
        {
            if (roomList != null)
            {
                roomList.Clear();
            }
            foreach (RoomInfo room in rL)
            {
                if (room.MaxPlayers != 0)
                {
                    roomList.Add(room);
                }
            }
        }
        
    }
    
    bool checkNameRoomExist(string s)
    {
        //roomList = PhotonNetwork.GetRoomList();
        if (roomList != null)
        {
            foreach(RoomInfo room in roomList)
            {
                if (room.Name == s)
                {
                    return true;
                }
            }
        }
        return false;
    }

    bool isOpen(string s)
    {
        if (roomList != null)
        {
            foreach (RoomInfo room in roomList)
            {
                if (room.Name == s)
                {
                    if (!room.IsOpen)
                    {
                        return false;
                    }
                }
            }
        }
        
        return true;
    }
    public void printListRoom()
    {
        List<RoomInfo> curRoom = new List<RoomInfo>();
        if (roomList != null)
        {
            int curI = 0;
            foreach (RoomInfo room in roomList)
            {
                if (room.IsOpen)
                {
                    curRoom.Add(room);
                    curI++;
                }
            }
            FindObjectOfType<RoomListing>().rI = curRoom;
            FindObjectOfType<RoomListing>().lengthRI = curI;
            FindObjectOfType<RoomListing>().caculatorPage();
        }
        else
        {
            Debug.Log("List room have not include");
        }
    }
    public void callConnect()
    {
        playerName = playerNameInputField.text;
        if (playerName == "" || playerName.Length <= (int)1)
        {
            PhotonNetwork.NickName = Random.Range(0, 10).ToString() + Random.Range(0, 10).ToString() + Random.Range(0, 10).ToString() + Random.Range(0, 10).ToString() + Random.Range(0, 10).ToString();
        }
        else
        {
            PhotonNetwork.NickName = playerName;
        }
        PhotonNetwork.AuthValues = new AuthenticationValues(Random.Range(0, 20).ToString() + Random.Range(0, 20).ToString() + Random.Range(0, 20).ToString() + Random.Range(0, 20).ToString() + Random.Range(0, 20).ToString());
        connect();
        connecting = true;
    }
    
    public void callCreateRoom()
    {
        RoomSetting.roomName = nameRoomToCreateField.text;
        if (RoomSetting.roomName != null && RoomSetting.maxPlayer != 0 && RoomSetting.roomName != "")
        {
            if (!checkNameRoomExist(RoomSetting.roomName))
            {
                if (RoomSetting.isPrivate)
                {
                    PhotonNetwork.CreateRoom("private" + RoomSetting.roomName);
                    createObj.SetActive(false);
                    joinObj.SetActive(false);
                }
                else
                {
                    PhotonNetwork.CreateRoom(RoomSetting.roomName);
                    createObj.SetActive(false);
                    joinObj.SetActive(false);
                }
            }
            else
            {
                roomExistText.GetComponent<turnOffText>().timeAlive = 0.25f;
                roomExistText.text = "Room is existing!!!";
            }
        }
        else
        {
            if (RoomSetting.roomName == null || RoomSetting.roomName == "")
            {
                roomExistText.GetComponent<turnOffText>().timeAlive = 0.5f;
                roomExistText.text = "Enter room's name!!!";
            }
            else if (RoomSetting.maxPlayer == 0)
            {
                roomExistText.GetComponent<turnOffText>().timeAlive = 0.5f;
                roomExistText.text = "Choose max player!!!";
            }
        }
    }
    public void callJoinRoom()
    {
        nameRoomToJoin = nameRoomToJoinInputField.text;
        if (checkNameRoomExist(nameRoomToJoin))
        {
            if (isOpen(nameRoomToJoin))
            {
                PhotonNetwork.JoinRoom(nameRoomToJoin);
                createObj.SetActive(false);
                joinObj.SetActive(false);
            }
            else
            {
                roomExistText.GetComponent<turnOffText>().timeAlive = 0.25f;
                roomExistText.text = "Room is closing!!!";
            }
        }
        else if (checkNameRoomExist("private" + nameRoomToJoin))
        {
            if (isOpen(nameRoomToJoin))
            {
                PhotonNetwork.JoinRoom("private" + nameRoomToJoin);
                createObj.SetActive(false);
                joinObj.SetActive(false);
            }
            else
            {
                roomExistText.GetComponent<turnOffText>().timeAlive = 0.25f;
                roomExistText.text = "Room is closing!!!";
            }
        }
        else
        {
            roomExistText.GetComponent<turnOffText>().timeAlive = 0.25f;
            roomExistText.text = "Not exist!!!";
        }
    }

    void IConnectionCallbacks.OnConnectedToMaster()
    {
        if (log.gameObject.activeSelf)
            log.text = log.text + "connected to master" + "\n";
        PhotonNetwork.JoinLobby();
        roomListPannel.SetActive(true);
        _shop.gameObject.SetActive(true);
    }
    void ILobbyCallbacks.OnJoinedLobby()
    {
        if (turtorial != null)
        {
            turtorial.SetActive(false);
        }
        listPlayer.gameObject.SetActive(false);
        if (log.gameObject.activeSelf)
            log.text = log.text + "connected to lobby" + "\n";
        log.gameObject.SetActive(false);
        connectingText.gameObject.SetActive(false);
        if (PhotonNetwork.LocalPlayer.CustomProperties["code"] != null)
        {
            PhotonNetwork.LocalPlayer.CustomProperties["code"] = null;
        }
        Debug.Log(PhotonNetwork.CloudRegion);
        timer.SetActive(false);
        skillButton.gameObject.SetActive(false);
        zoomUI.gameObject.SetActive(false);
        zoomUI.value = 5.5f;
        resetRoomSetting();
        createObj.SetActive(true);
        joinObj.SetActive(true);
        firebutton.gameObject.SetActive(false);
        jumpButton.gameObject.SetActive(false);
        RotateArea.SetActive(false);
        joyStickMove.SetActive(false);
        joyStickMove.GetComponent<FixedJoystick>().enabled = false;
        FindObjectOfType<MatchManager>().Playing = false;
        svManger.buttonSetting.gameObject.SetActive(false);
        svManger.pannel.gameObject.SetActive(false);
        lockAndUnlock.SetActive(false);
        FindObjectOfType<ClockManager>().currentTime = 1;
        FindObjectOfType<PlayerLocalController>().waitBg.gameObject.SetActive(false);
        win.gameObject.SetActive(false);
        lose.gameObject.SetActive(false);
        standbyCamera.enabled = true;
        standbyCamera.GetComponent<AudioListener>().enabled = true;
        standbyCamera.GetComponent<AudioSource>().Play();
        delayLoadRoom = 0.5f;
        onJoinedLobby = true;
        
    }
    void OnPhotonJoinRoomFailed(/*object [] codeAndMsg*/)
    {
        if (log.gameObject.activeSelf)
            log.text = log.text + "failed join room" + "\n";
        Debug.Log("failed join room");
    }
    void IConnectionCallbacks.OnDisconnected(DisconnectCause cause)
    {
        if (log != null)
        {
            log.text = log.text + "Can't connect to photon " + cause + "\n";
        }
        
    }
    void IMatchmakingCallbacks.OnJoinedRoom()
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties(TeamStaticVar.team0);

        timer.SetActive(true);
        skillButton.gameObject.SetActive(true);
        Debug.Log("joined");
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.MaxPlayers = RoomSetting.maxPlayer;
        }
        roomListPannel.SetActive(false);
        _shop.gameObject.SetActive(false);
        shopPannel.gameObject.SetActive(false);
        RotateArea.SetActive(true);
        jumpButton.gameObject.SetActive(true);
        firebutton.gameObject.SetActive(true);
        SpawnMyPlayer();
        joyStickMove.SetActive(true);
        joyStickMove.GetComponent<FixedJoystick>().enabled = true;
        zoomUI.gameObject.SetActive(true);
    }
    void IInRoomCallbacks.OnPlayerEnteredRoom(Player newPlayer)
    {
        countPlayer.text = PhotonNetwork.PlayerList.Length + " in room.";
        listPlayer.GetComponent<UiListPlayerController>().listOption = new List<string>(21);
        listPlayer.GetComponent<UiListPlayerController>().setSlots();
        if (PhotonNetwork.IsMasterClient)
        {
            GetComponent<PhotonView>().RPC("sendRoomSeting", Photon.Pun.RpcTarget.All, RoomSetting.isPrivate, RoomSetting.maxPlayer, RoomSetting.roomName);
        }
    }
    public void SpawnMyPlayer()
    {
        if (spawnSpots == null)
        {
            Debug.Log("SpawnSpotFAIL");
            return;
        }
        /*addNotice(PhotonNetwork.playerName + " has joined");*/
        SpawnSpot mySpawnSpot = spawnSpots[Random.Range(0, spawnSpots.Length)];
        GameObject myPlayerGO = (GameObject)PhotonNetwork.Instantiate("Character_Cowgirl_01", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);

        standbyCamera.enabled = false;
        standbyCamera.GetComponent<AudioSource>().Stop();
        standbyCamera.GetComponent<AudioListener>().enabled = false;
        myPlayerGO.GetComponent<AnimationController>().enabled = true;
        myPlayerGO.GetComponent<UserMovement>().enabled = true;
        myPlayerGO.GetComponent<playerShooting>().enabled = true;
        myPlayerGO.GetComponent<MinigameController>().enabled = true;
        FindObjectOfType<MatchManager>().playerShootLocal = myPlayerGO.GetComponent<playerShooting>();
        firebutton.GetComponent<FireButton>().plerShooting = myPlayerGO.GetComponent<playerShooting>();

        ///////get Appearance
        AppearanceManager appeaMangager = FindObjectOfType<AppearanceManager>();
        for (int i = 0; i < myPlayerGO.transform.Find("props").childCount; i++)
        {
            appeaMangager.getLocalProps(myPlayerGO.transform.Find("props").GetChild(i).gameObject, i);
        }
        for (int i = 0; i < myPlayerGO.transform.Find("characters").childCount; i++)
        {
            appeaMangager.getLocalChacracters(myPlayerGO.transform.Find("characters").GetChild(i).gameObject, i);
        }
        for (int i = 0; i < myPlayerGO.transform.Find("Root/Hips/Spine_01/Spine_02/Spine_03/Clavicle_R/Shoulder_R/Elbow_R/Hand_R/WeaponHolder").childCount; i++)
        {
            appeaMangager.getLocalGuns(myPlayerGO.transform.Find("Root/Hips/Spine_01/Spine_02/Spine_03/Clavicle_R/Shoulder_R/Elbow_R/Hand_R/WeaponHolder").GetChild(i).gameObject, i);
        }
        appeaMangager.getCharaControl(myPlayerGO.GetComponent<CharacterController>());
        ///////////////////////////

        myPlayerGO.transform.Find("Main Camera").gameObject.SetActive(true);
        myPlayerGO.GetComponent<audioEvent>().enabled = true;

        PlayerLocalController plc = FindObjectOfType<PlayerLocalController>();
        myPlayerGO.transform.GetComponent<UserMovement>().InsRotate = FindObjectOfType<InstantiateRotate>();
        plc.playLocalUserMovement = myPlayerGO.transform.GetComponent<UserMovement>();
        jumpButton.GetComponent<jumpbutton>().usermov = myPlayerGO.transform.GetComponent<UserMovement>();

        myPlayerGO.transform.Find("Main Camera").GetComponent<UserCamera>().InsRotate = FindObjectOfType<InstantiateRotate>();
        plc.playLocalUserCamera = myPlayerGO.transform.Find("Main Camera").GetComponent<UserCamera>();
        

        plc.parSys = myPlayerGO.transform.Find("Particle System").GetComponent<ParticleSystem>();

        plc.myLocalPlayer = myPlayerGO.gameObject;
        //name
        myPlayerGO.transform.Find("NameLable").gameObject.GetComponent<TextMeshPro>().SetText(PhotonNetwork.LocalPlayer.NickName);
        plc.namelable = myPlayerGO.transform.Find("NameLable").gameObject.GetComponent<TextMeshPro>();
        //bird
        listBirds listbirdGO = FindObjectOfType<listBirds>();
        listbirdGO.displayBird();
        myPlayerGO.transform.Find("birds/bird1").gameObject.SetActive(listbirdGO.birds[0]);
        myPlayerGO.transform.Find("birds/bird2").gameObject.SetActive(listbirdGO.birds[1]);
        myPlayerGO.transform.Find("birds/bird3").gameObject.SetActive(listbirdGO.birds[2]);
        myPlayerGO.transform.Find("birds/bird4").gameObject.SetActive(listbirdGO.birds[3]);
        FindObjectOfType<ClockManager>().birds = myPlayerGO.transform.Find("birds").gameObject;

    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient && !FindObjectOfType<MatchManager>().Playing)
        {
            startButton.gameObject.SetActive(true);
        }
        else
        {
            startButton.gameObject.SetActive(false);
        }
        if (onJoinedLobby && (delayLoadRoom - 0 <= 0.000001))
        {
            printListRoom();
            onJoinedLobby = false;
        }
        if (delayLoadRoom - 0 >= 0.000001)
        {
            delayLoadRoom = delayLoadRoom - Time.deltaTime;
        }
        /* debugTeamOfPlayers
        if (PhotonNetwork.playerList.Length >= 2)
        {

            Debug.Log(PhotonNetwork.playerList[0].NickName + PhotonNetwork.playerList[0].GetTeam());
            Debug.Log(PhotonNetwork.playerList[1].NickName + PhotonNetwork.playerList[1].GetTeam());
        }
        else
        {
            Debug.Log(PhotonNetwork.playerList[0].NickName + PhotonNetwork.playerList[0].GetTeam());
        }
        */
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount >= RoomSetting.maxPlayer)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
            }
            else
            {
                if (FindObjectOfType<MatchManager>().Playing)
                {
                    PhotonNetwork.CurrentRoom.IsOpen = false;
                }
                else
                {
                    PhotonNetwork.CurrentRoom.IsOpen = true;
                }
            }
        }
        if (respawnTime > 0)
        {
            respawnTime -= Time.deltaTime;
            if (respawnTime <= 0)
            {
                Debug.Log("Let's Respawn");
                {
                    SpawnMyPlayer();
                }
            }
        }
    }
}
