using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class FXManager : MonoBehaviour
{
    public AudioClip[] footStep;
    public AudioClip FXAudioFirePistol;
    public AudioClip FXAudioHittingFlesh;
    public AudioClip FXAudioFailMiniGame;
    AudioSource audiosources;

    public GameObject FXBlood;
    public GameObject FXPathBullet;
    public GameObject ricoch;
    private void Start()
    {
        audiosources = GetComponent<AudioSource>();
    }
    public void callFootStep(Vector3 positionFootStep)
    {
        
        AudioClip fs = footStep[Random.Range(0, 5)];
        AudioSource.PlayClipAtPoint(fs, positionFootStep, audiosources.volume);
    }
    [PunRPC]
    void failMiniGame(Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(FXAudioFailMiniGame, pos, audiosources.volume);
    }
    [PunRPC]
    void onFire(Vector3 startPath)
    {
        AudioSource.PlayClipAtPoint(FXAudioFirePistol, startPath, audiosources.volume);
        
    }

    [PunRPC]
    void ricochetShot(Vector3 startPath, Vector3 endPath, string WhichObjectHitted)
    {
        AudioSource.PlayClipAtPoint(FXAudioHittingFlesh, endPath, audiosources.volume);
        
        if (WhichObjectHitted == "Player" || WhichObjectHitted == "props")
        {
            GameObject blood = (GameObject)Instantiate(FXBlood, endPath, Quaternion.LookRotation(startPath - endPath));
        }
        else
        {
            GameObject Ricoch = (GameObject)Instantiate(ricoch, endPath, Quaternion.LookRotation(startPath - endPath));
        }
        
    }
    [PunRPC]
    void PathBullet(Vector3 startPath, Vector3 endPath)
    {
        Debug.DrawRay(startPath, endPath - startPath, Color.green, 20);

        GameObject GO = (GameObject)Instantiate(FXPathBullet, startPath, Quaternion.LookRotation(endPath - startPath));

        GO.transform.Find("LineFX").transform.localScale = new Vector3(1, 0, Vector3.Distance(startPath, endPath));
    }
}
