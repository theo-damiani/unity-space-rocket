using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.InteropServices;

public class AppManager : Singleton<AppManager>
{

    [DllImport("__Internal")]
    private static extern void UnityIsLoaded();
    void Start()
    {
        InformReactThatUnityIsLoaded();
    }

    void InformReactThatUnityIsLoaded()
    {
        #if UNITY_WEBGL == true && UNITY_EDITOR == false
            UnityIsLoaded();
        #endif
    }
}
