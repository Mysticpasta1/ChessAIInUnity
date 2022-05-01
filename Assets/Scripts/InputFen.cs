using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFen : MonoBehaviour
{
    public static string customFen;

    void Start()
    {
        var input = gameObject.GetComponent<InputField>();
        input.onEndEdit.AddListener(SubmitName);
    }

    private void SubmitName(string arg0)
    {
        customFen = arg0;
    }
}
