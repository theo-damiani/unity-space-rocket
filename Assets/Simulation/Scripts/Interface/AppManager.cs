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
    [Header("Affordances")]
    [SerializeField] private Affordances defaultAffordances;
    private Affordances currentAffordances;

    [Header("Main App Controls")]
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject resetButton;

    [Header("Rocket Variables")]
    [SerializeField] private GameObject rocket;
    [SerializeField] private Vector3Variable rocketVelocity;
    [SerializeField] private GameObject rocketVelocityVector;
    [SerializeField] private BoolVariable showRocketPath;
    [SerializeField] private GameObject showRocketPathButton;

    [Header("Thrust Variables")]
    [SerializeField] private BoolVariable thrustIsActive;
    [SerializeField] private Vector3Variable thrustForce;
    [SerializeField] private BoolVariable thrustShowVector;

    void Start()
    {
        #if UNITY_EDITOR == true
            currentAffordances = Instantiate(defaultAffordances);
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
        currentAffordances = JsonUtility.FromJson<Affordances>(affordanceJson);
        ResetApp();
    }

    public void ResetApp()
    {
        // Main control config:
        playButton.SetActive(currentAffordances.showPlayButton);
        resetButton.SetActive(currentAffordances.showResetButton);
        // Rocket config:
        rocket.transform.SetPositionAndRotation(currentAffordances.physicalObject.initialPosition.ToVector3(), Quaternion.identity);
        rocket.transform.Find("RocketObject").transform.rotation = Quaternion.identity;
        rocketVelocity.Value = currentAffordances.physicalObject.initialVelocity.ToVector3();
        rocketVelocityVector.SetActive(currentAffordances.physicalObject.velocityVectorIsVisible);
        rocketVelocityVector.GetComponent<DraggableVector>().SetInteractable(currentAffordances.physicalObject.velocityVectorIsVisible);
        rocketVelocityVector.GetComponent<DraggableVector>().Redraw();
        rocket.GetComponent<Rigidbody>().velocity = rocketVelocity.Value;
        // Path Renderer config:
        showRocketPath.Value = currentAffordances.physicalObject.showTrace;
        showRocketPathButton.SetActive(currentAffordances.physicalObject.showTraceIsInteractive);
        showRocketPathButton.GetComponent<Toggle>().isOn = currentAffordances.physicalObject.showTrace;
        // Thrust Config:
        thrustIsActive.Value = currentAffordances.thrustForce.isActive;
        thrustShowVector.Value = currentAffordances.thrustForce.showVector;
        thrustForce.Value = Vector3.up * currentAffordances.thrustForce.initialMagnitude;
    }
}
