using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.InteropServices;
using Unity.VisualScripting;

public class AppManager : Singleton<AppManager>
{

    [DllImport("__Internal")]
    private static extern void UnityIsLoaded();

    // Uniform Motion State:
    [Header("Uniform Motion States")]
    [SerializeField, LabelOverride("IsInteractable")] private BoolVariable uniformMotionIsInteractable;
    [SerializeField, LabelOverride("IsActiveAtStart")] private BoolVariable uniformMotionIsActiveAtStart;
    [SerializeField, LabelOverride("InitVelocity")] private Vector3Variable uniformMotionInitVelocity;

    [Header("Uniform Motion Variables")]
    [SerializeField, LabelOverride("velocity")] private Vector3Variable UM_velocity;
    [SerializeField, LabelOverride("IsActive")] private BoolVariable UM_isActive;

    void Start()
    {
        #if UNITY_EDITOR == true
            DefaultConfigMotion();
            ResetApp();
        #endif

        InformReactThatUnityIsLoaded();
    }

    void InformReactThatUnityIsLoaded()
    {
        #if UNITY_WEBGL == true && UNITY_EDITOR == false
            UnityIsLoaded();
        #endif
    }

    private void DefaultConfigMotion()
    {
        ConfigMotion(false, true, Vector3.right*5);
    }

    public void ConfigMotion(bool isInteractable, bool isActiveAtStart, Vector3 initVelocity)
    {
        uniformMotionIsInteractable.Value = isInteractable;
        uniformMotionIsActiveAtStart.Value = isActiveAtStart;
        uniformMotionInitVelocity.Value = initVelocity;
    }

    public void ResetApp()
    {
        UM_velocity.Value = uniformMotionInitVelocity.Value;
        UM_isActive.Value = uniformMotionIsActiveAtStart.Value;
    }
}
