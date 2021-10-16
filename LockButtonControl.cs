using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockButtonControl : MonoBehaviour
{
    public Button lockButton;
    public Button unlockButton;
    public void callLockAppearance()
    {
        FindObjectOfType<PlayerLocalController>().playLocalUserMovement.enabled = false;
        lockButton.gameObject.SetActive(false);
        unlockButton.gameObject.SetActive(true);
    }
    public void callUnlockAppearance()
    {
        FindObjectOfType<PlayerLocalController>().playLocalUserMovement.enabled = true;
        lockButton.gameObject.SetActive(true);
        unlockButton.gameObject.SetActive(false);
    }
}
