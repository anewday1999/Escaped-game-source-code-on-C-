using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamStaticVar : MonoBehaviour
{
    public static ExitGames.Client.Photon.Hashtable team0 = new ExitGames.Client.Photon.Hashtable { { "code", (int)0 } };
    public static ExitGames.Client.Photon.Hashtable team1 = new ExitGames.Client.Photon.Hashtable { { "code", (int)1 } }; //blue
    public static ExitGames.Client.Photon.Hashtable team2 = new ExitGames.Client.Photon.Hashtable { { "code", (int)2 } }; // red
}
