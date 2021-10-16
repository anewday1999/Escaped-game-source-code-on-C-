using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NetworkPlayer : MonoBehaviour, IPunObservable
{
    string s;
    bool tmp;
    bool parSys = false;

    bool isNameLableAct;
    Vector3 target = Vector3.zero;
    Vector3 realPosition = Vector3.zero;
    Quaternion realRotation = Quaternion.identity;
    bool gotFirstUpdate = false;
    private int team;

    /// <summary> 

    //appearance;
    public GameObject[] props;
    public int countProps = 34;

    public GameObject[] characters;
    public int countCharacters = 8;

    public GameObject[] guns;
    public int countGuns = 1 + 1;

    bool isActive;
    /// </summary>
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        props = new GameObject[countProps];
        characters = new GameObject[countCharacters];
        guns = new GameObject[countGuns];
        isActive = false;
        initAnim();
        initAppea();
    }
    void initAnim()
    {
        if (anim != null)
            return;
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.Log("Forgot to add Animator component to characters");
        }
    }
    void initAppea()
    {
        for (int i = 0; i < gameObject.transform.Find("props").childCount; i++)
        {
            props[i] = gameObject.transform.Find("props").GetChild(i).gameObject;
        }
        for (int i = 0; i < gameObject.transform.Find("characters").childCount; i++)
        {
            characters[i] = gameObject.transform.Find("characters").GetChild(i).gameObject;
        }
        for (int i = 0; i < gameObject.transform.Find("Root/Hips/Spine_01/Spine_02/Spine_03/Clavicle_R/Shoulder_R/Elbow_R/Hand_R/WeaponHolder").childCount; i++)
        {
            guns[i] = gameObject.transform.Find("Root/Hips/Spine_01/Spine_02/Spine_03/Clavicle_R/Shoulder_R/Elbow_R/Hand_R/WeaponHolder").GetChild(i).gameObject;
        }
    }
    void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            //do nothing, the character is moving us.
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, realPosition, 0.2f);
            transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.2f);
        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo MessInfo)
    {
        initAnim();
        if (stream.IsWriting)
        {
            //gui thong tin cua minh len server
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(anim.GetBool("run"));
            stream.SendNext(anim.GetBool("jump"));
            stream.SendNext(transform.Find("Main Camera/TargetCam").transform.position);
            for (int i = 0; i < countProps; i++)
            {
                stream.SendNext(props[i].activeSelf);
            }
            for (int i = 0; i < countCharacters; i++)
            {
                stream.SendNext(characters[i].activeSelf);
            }
            for (int i = 0; i < countGuns; i++)
            {
                stream.SendNext(guns[i].activeSelf);
            }
            stream.SendNext(gameObject.GetComponent<CharacterController>().radius);

            //parSys
            stream.SendNext(FindObjectOfType<PlayerLocalController>().parSys.gameObject.activeSelf);
            //bird
            stream.SendNext(gameObject.transform.Find("birds/bird1").gameObject.activeSelf);
            stream.SendNext(gameObject.transform.Find("birds/bird2").gameObject.activeSelf);
            stream.SendNext(gameObject.transform.Find("birds/bird3").gameObject.activeSelf);
            stream.SendNext(gameObject.transform.Find("birds/bird4").gameObject.activeSelf);
            stream.SendNext(gameObject.transform.Find("birds").gameObject.activeSelf);
            //name
            isNameLableAct = gameObject.transform.Find("NameLable").GetComponent<TextMeshPro>().IsActive();
            stream.SendNext(isNameLableAct);
            stream.SendNext(gameObject.transform.Find("NameLable").GetComponent<TextMeshPro>().text);
            //scale
            stream.SendNext(gameObject.transform.localScale);
        }
        else
        {
            //lay thong tin tu thang khac ve
            realPosition = (Vector3)stream.ReceiveNext();
            realRotation = (Quaternion)stream.ReceiveNext();
            anim.SetBool("run", (bool)stream.ReceiveNext());
            anim.SetBool("jump", (bool)stream.ReceiveNext());

            target = (Vector3)stream.ReceiveNext();
            gameObject.transform.Find("Root/Hips/Spine_01/Spine_02/Spine_03/Neck/Head").GetComponent<Lookat>().targetCam.transform.position = target;
            for (int i = 0; i < countProps; i++)
            {
                isActive = (bool)stream.ReceiveNext();
                props[i].SetActive(isActive);
            }
            for (int i = 0; i < countCharacters; i++)
            {
                isActive = (bool)stream.ReceiveNext();
                characters[i].SetActive(isActive);
            }
            for (int i = 0; i < countGuns; i++)
            {
                isActive = (bool)stream.ReceiveNext();
                guns[i].SetActive(isActive);
            }
            gameObject.GetComponent<CharacterController>().radius = (float)stream.ReceiveNext();

            //particle system
            parSys = (bool)stream.ReceiveNext();
            if (PhotonNetwork.LocalPlayer.CustomProperties["code"] != null)
            {
                if ((int)PhotonNetwork.LocalPlayer.CustomProperties["code"] != 1)
                {
                    gameObject.transform.Find("Particle System").gameObject.SetActive(parSys);
                }
                else
                {
                    tmp = parSys;
                }
            }
            
            //bird
            gameObject.transform.Find("birds/bird1").gameObject.SetActive( (bool) stream.ReceiveNext());
            gameObject.transform.Find("birds/bird2").gameObject.SetActive( (bool) stream.ReceiveNext());
            gameObject.transform.Find("birds/bird3").gameObject.SetActive( (bool) stream.ReceiveNext());
            gameObject.transform.Find("birds/bird4").gameObject.SetActive( (bool) stream.ReceiveNext());
            gameObject.transform.Find("birds").gameObject.SetActive( (bool) stream.ReceiveNext());
            //name
            gameObject.transform.Find("NameLable").GetComponent<TextMeshPro>().gameObject.SetActive((bool)stream.ReceiveNext());
            s = (string)stream.ReceiveNext();
            gameObject.transform.Find("NameLable").GetComponent<TextMeshPro>().text = s;
            //scale
            gameObject.transform.localScale = (Vector3)stream.ReceiveNext();

            if (!gotFirstUpdate)
            {
                transform.position = realPosition;
                transform.rotation = realRotation;
                gotFirstUpdate = true;
            }
        }
    }
}
