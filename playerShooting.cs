using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class playerShooting : MonoBehaviour
{
    public bool clickFire;
    public GameObject targetCam;
    public float shootingRate = 0.25f;
    float theNextTime = 0;
    public float WeaponDamage = 20.0f;
    Vector3 barrel;

    private string WhichObjectHitted; // 1: people
    RaycastHit hitInfo;
    FXManager FXManager;
    whistleBird _whistleBird;
    void Start()
    {
        _whistleBird = GameObject.FindObjectOfType<whistleBird>();
        if (_whistleBird == null)
        {
            Debug.Log("can't ind _whistleBird");
        }
        FXManager = GameObject.FindObjectOfType<FXManager>();
        if (FXManager == null)
        {
            Debug.Log("can't ind FXManager");
        }
    }

    public void callfire()
    {
        Transform cam = Camera.main.GetComponent<Transform>();
        Transform player = this.GetComponent<Transform>();
        if (theNextTime <= Time.time && Vector3.Angle(player.forward, cam.forward) < 85)
        {
            fire();
        }
    }
    void Update()
    {
        /*
        if (clickFire && theNextTime <= Time.time && Vector3.Angle(player.forward, cam.forward) < 85)
        {
            fire();
        }*/
    }
    void fire()
    {
        Debug.Log("fire");
        barrel = this.transform.Find("Root/Hips/Spine_01/Spine_02/Spine_03/Clavicle_R/Shoulder_R/Elbow_R/Hand_R/WeaponHolder/Barrel").transform.position;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Transform hitTransform;
        Vector3 direction = new Vector3(0, 0, 0);
        bool check;
        //debug
        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 100, Color.red, 20);
        //////////////////

        check = FindRealHitInfo(ray);
        // update new ray
        if (check)
        {
            direction = hitInfo.point - (barrel);
            ray = new Ray(barrel, direction);
        }
        else
        {
            if (FXManager != null)
            {
                Debug.Log("don't hit anythings");

                FXManager.GetComponent<PhotonView>().RPC("PathBullet", Photon.Pun.RpcTarget.All, barrel, targetCam.transform.position);

                FXManager.GetComponent<PhotonView>().RPC("onFire", Photon.Pun.RpcTarget.All, barrel);
                //FXManager.GetComponent<PhotonView>().RPC("ricochetShot", PhotonTargets.All, barrel, Camera.main.transform.forward * 100, WhichObjectHitted);
            }
        }
        
        check = FindRealHitInfo(ray); ;
        if (check)
        {
            if (FXManager != null)
            {
                FXManager.GetComponent<PhotonView>().RPC("PathBullet", Photon.Pun.RpcTarget.All, barrel, hitInfo.point);

                FXManager.GetComponent<PhotonView>().RPC("onFire", Photon.Pun.RpcTarget.All, barrel);

                WhichObjectHitted = hitInfo.transform.tag;
                FXManager.GetComponent<PhotonView>().RPC("ricochetShot", Photon.Pun.RpcTarget.All, barrel, hitInfo.point, WhichObjectHitted);
            }
            //Debug.DrawRay(this.transform.position + offset, hitInfo.point - (this.transform.position + offset), Color.green, 20);
            Debug.Log("we hit " + hitInfo.transform.name);
            hitTransform = hitInfo.transform;

            Health h = hitTransform.GetComponent<Health>();

            if (h == null && hitTransform.parent)
            {
                hitTransform = hitTransform.parent;
                h = hitTransform.GetComponent<Health>();
                if (h == null && hitTransform.parent)
                {
                    hitTransform = hitTransform.parent;
                    h = hitTransform.GetComponent<Health>();
                    if (h == null && hitTransform.parent)
                    {
                        hitTransform = hitTransform.parent;
                        h = hitTransform.GetComponent<Health>();
                    }
                }
            }
            if (h != null)
            {
                Debug.Log((int)PhotonNetwork.LocalPlayer.CustomProperties["code"] + "  " +  h.getCurrentTeam());
                if (h.GetComponent<PhotonView>() == null)
                {
                    Debug.Log("Didnt add photonview to object");
                }
                else if ((int)PhotonNetwork.LocalPlayer.CustomProperties["code"] != h.getCurrentTeam())
                {
                    //h.takeDamage(WeaponDamage);
                    h.GetComponent<PhotonView>().RPC("takeDamage", Photon.Pun.RpcTarget.All, WeaponDamage);
                    if (listBirds.birdEnabled != -1 && listBirds._listBirds[listBirds.birdEnabled])
                        _whistleBird.GetComponent<PhotonView>().RPC("whistle", Photon.Pun.RpcTarget.All, listBirds.birdEnabled, this.gameObject.transform.position);
                }
            }
        }

        theNextTime = Time.time + shootingRate;
    }

    bool FindRealHitInfo(Ray ray)
    {
        bool check = true;
        RaycastHit[] hits = Physics.RaycastAll(ray);
        /*
        for (int i = 0; i < hits.Length; i++)
        {
            Debug.Log(hits[i].transform.name + "  " + hits[i].distance);
        }*/
        float distance = 0;
        check = Physics.Raycast(ray, 100f);
        ///check

        if (check)
        {
            hitInfo = hits[0];
            distance = hits[0].distance;
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform != this.transform && hit.distance < distance && hit.transform != hits[0].transform)
                {
                    hitInfo = hit;
                    distance = hit.distance;
                }
            }
        }
        return check;
        
    }
}
