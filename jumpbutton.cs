using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpbutton : MonoBehaviour
{
    public UserMovement usermov;
    public void click()
    {
        usermov.jump = true;
    }
}
