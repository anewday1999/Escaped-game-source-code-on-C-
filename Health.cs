using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float health = 100.0f;
    [SerializeField]
    private float currentHealth= 100.0f;
    [SerializeField]
    float respawnTime;
    void Start()
    {
        PhotonNetwork.LocalPlayer.CustomProperties["code"] = 0;
    }
    public int getCurrentTeam()
    {
        return (int)this.GetComponent<PhotonView>().Owner.CustomProperties["code"];
    }
    [PunRPC]
    public void takeDamage(float damage)
    {
        Debug.Log("i taked damne");
        currentHealth -= 20;

        if (currentHealth <= 0)
        {
            die();
        }
    }
    void die()
    {
        Debug.Log("im dead");
        if (GetComponent<Photon.Pun.PhotonView>().InstantiationId == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            if (GetComponent<Photon.Pun.PhotonView>().IsMine)
            {
                if (gameObject.tag == "Player" || gameObject.tag == "props")
                {
                    //GameObject.FindObjectOfType<networkmanager>().standbyCamera.enabled = true;
                    //GameObject.FindObjectOfType<networkmanager>().respawnTime = 5.0f;
                    //FindObjectOfType<MatchManager>().Playing = false;
                    FindObjectOfType<AppearanceManager>().destroy();
                    PhotonNetwork.LocalPlayer.SetCustomProperties(TeamStaticVar.team0);
                    FindObjectOfType<PlayerLocalController>().changeSpeed(20);
                    currentHealth = 100.0f;
                }
                //PhotonNetwork.Destroy(gameObject);
            }
            else
            {

            }
            
        }
        
    }
}
