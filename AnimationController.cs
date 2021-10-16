using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator anim;
    float theNextTime;
    public bool isPistol;
    float setWeigh;
    float realWeigh;

    public UserMovement _usermove;
    public float delayMask = 0.2f;
    void Start()
    {
        theNextTime = 0;
        anim = GetComponent<Animator>();
        _usermove = FindObjectOfType<PlayerLocalController>().playLocalUserMovement;
    }

    void animSetBool(string name, bool bo)
    {
        anim.SetBool(name, bo);
    }

    void NoWeapon()
    {
        if (_usermove.joyVertical != 0 || _usermove.joyHorizontal != 0)
        {
            animSetBool("run", true);
        }
        else
        {
            animSetBool("run", false);
        }
        if (Input.GetButtonDown("Jump"))
        {
            animSetBool("jump", true);
            theNextTime = Time.time + 0.2f;
        }
        else if (theNextTime <= Time.time)
        {
            animSetBool("jump", false);
        }
    }
    [PunRPC]
    void Pistol(float WeighLayer)
    {
        Animator a;
        a = GetComponent<PhotonView>().transform.GetComponent<Animator>();
        if (a ==  null)
        {
            Debug.Log("cant find animator");
            return;
        }
        a.SetLayerWeight(a.GetLayerIndex("Pistol"), WeighLayer);
        if (Mathf.Abs(WeighLayer - 1) < 0.001f)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                a.SetBool("PistolFire", true);
            }
            else
            {
                a.SetBool("PistolFire", false);
            }
        }
        
    }
    void Update()
    {
        NoWeapon();

        if (isPistol == true /*&& (Input.GetMouseButton(1))*/)
        {
            setWeigh = 1;
        }
        else
        {
            setWeigh = 0;
        }
        if (setWeigh == 0)
        {
            realWeigh = Mathf.Lerp(this.GetComponent<Animator>().GetLayerWeight(this.GetComponent<Animator>().GetLayerIndex("Pistol")), setWeigh, 0.2f);
        }
        else
        {
            realWeigh = Mathf.Lerp(this.GetComponent<Animator>().GetLayerWeight(this.GetComponent<Animator>().GetLayerIndex("Pistol")), setWeigh, 1.0f);
        }

        GetComponent<PhotonView>().RPC("Pistol", Photon.Pun.RpcTarget.All, realWeigh);

    }
}
