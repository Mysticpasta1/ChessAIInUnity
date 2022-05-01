using System;
using UnityEngine;

public class FenDontDestrory : MonoBehaviour
{
    public static FenDontDestrory instance;
    
    public void Start()
    {
        instance = this;
    }

    public void DontDestroryFenScript()
    {
        DontDestroyOnLoad(this);
    }
}
