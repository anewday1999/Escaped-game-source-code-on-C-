using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Lookat : MonoBehaviour
{
    public Vector3 offset;
    public GameObject targetCam;

    // Start is called before the first frame update
    [PunRPC]
    void lookAt()
    {
        transform.LookAt(targetCam.transform);

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (targetCam != null)
        {
            gameObject.GetComponent<PhotonView>().RPC("lookAt", Photon.Pun.RpcTarget.All);
        }
    }
}
