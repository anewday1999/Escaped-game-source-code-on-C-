using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillTurtorial : MonoBehaviour
{
    // Start is called before the first frame update
    public Text buttonText;
    public Text turtorial;
    public void onClick()
    {
        if (buttonText.text == "SKILL >>")
        {
            buttonText.text = "MAIN >>";
            turtorial.text = "\n\n\tRelease an electrified zone in front of the character.\n\n\tIf there is a prop in the area," +
                " they will have to complete a mini game (matching lightning bolts of the same color).\n\n\tIf they don't finish in time, their size will increase randomly.";
        }
        else
        {
            buttonText.text = "SKILL >>";
            turtorial.text = "\n\tGet started with a sort turtorial right now!!!" +
                "\n\n\t1/5 of the total players will have to find other players, who have transformed into props." +
                "\n\n\tFor the first 30 seconds, the hunter will be blindfolded, and props get increased movement speed. At 2:30, props will have to transform again." +
                "\n\n\tWhen the props lock their body, they can switch the camera to see other players.";
        }
    }
}
