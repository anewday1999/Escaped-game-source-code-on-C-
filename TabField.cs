using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabField : MonoBehaviour
{
    public InputField nextField;
    InputField myField;
    void Start()
    {
        if (nextField == null)
        {
            Destroy(this);
            return;
        }
        myField = GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myField.isFocused && nextField != null && Input.GetKeyDown(KeyCode.Tab))
        {
            nextField.ActivateInputField();
        }
    }
}
