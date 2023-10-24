using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;

public class AppManager : Singleton<AppManager>
{

    [DllImport("__Internal")]
    private static extern void UnityIsLoaded();

    [Header("Rocket States")]
    [SerializeField] private BoolVariable stateShowRocketPath;

    [Header("Rocket Variables")]
    [SerializeField] private BoolVariable showRocketPath;
    [SerializeField] private GameObject showRocketPathButton;



    // Uniform Motion State:
    [Header("Uniform Motion States")]
    [SerializeField, LabelOverride("IsInteractable")] private BoolVariable uniformMotionIsInteractable;
    [SerializeField, LabelOverride("IsActiveAtStart")] private BoolVariable uniformMotionIsActiveAtStart;
    [SerializeField, LabelOverride("InitVelocity")] private Vector3Variable uniformMotionInitVelocity;

    [Header("Uniform Motion Variables")]
    [SerializeField, LabelOverride("velocity")] private Vector3Variable UM_velocity;
    [SerializeField, LabelOverride("IsActive")] private BoolVariable UM_isActive;
    [SerializeField, LabelOverride("IsInteractable")] private GameObject UM_button;

    void Start()
    {
        #if UNITY_EDITOR == true
            DefaultConfigFromAffordances(DefaultAffordances.GetDefaultAffordances());
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

    public void ResetAppFromJSON(string affordanceJson)
    {
        Affordances newAffordances = JsonUtility.FromJson<Affordances>(affordanceJson);
        DefaultConfigFromAffordances(newAffordances);
        ResetApp();
    }

    private void DefaultConfigFromAffordances(Affordances affordances)
    {
        A_Motion m = affordances.PhysicsObject.UniformMotion;
        ConfigMotion(m.IsInteractive, m.IsActive, m.Velocity.X, m.Velocity.Y, m.Velocity.Z);
        ConfigRocket(affordances.PhysicsObject.ShowTrace);
    }

    private void ConfigMotion(bool isInteractable, bool isActiveAtStart, float x, float y, float z)
    {
        uniformMotionIsInteractable.Value = isInteractable;
        uniformMotionIsActiveAtStart.Value = isActiveAtStart;
        uniformMotionInitVelocity.Value = new Vector3(x, y, z);
    }

    private void ConfigRocket(bool showPath)
    {
        stateShowRocketPath.Value = showPath;
    }

    public void ResetApp()
    {
        UM_velocity.Value = uniformMotionInitVelocity.Value;
        UM_isActive.Value = uniformMotionIsActiveAtStart.Value;
        UM_button.SetActive(uniformMotionIsInteractable.Value);
        UM_button.GetComponent<Toggle>().isOn = uniformMotionIsActiveAtStart.Value;

        showRocketPath.Value = stateShowRocketPath.Value;
        showRocketPathButton.SetActive(true);
        showRocketPathButton.GetComponent<Toggle>().isOn = stateShowRocketPath.Value;
    }
}
