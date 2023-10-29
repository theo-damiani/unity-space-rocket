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
    [SerializeField] private BoolVariable rocketIsInteractive;
    [SerializeField] private Vector3Variable rocketVelocity;
    [SerializeField] private GameObject rocketVelocityVector;
    [SerializeField] private BoolVariable showVelocityEquation;
    [SerializeField] private GameObject velocityLabel;
    [SerializeField] private BoolVariable showRocketPath;
    [SerializeField] private GameObject showRocketPathButton;

    [Header("Thrust Variables")]
    [SerializeField] private BoolVariable thrustIsActive;
    [SerializeField] private BoolVariable thrustIsInteractive;
    [SerializeField] private Vector3Variable thrustForce;
    [SerializeField] private BoolVariable thrustShowVector;
    [SerializeField] private GameObject thrustShowLabel;
    [SerializeField] private BoolVariable thrustShowEquation;

    [Header("Extra")]
    [SerializeField] private GameObject referenceFrame;

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
        rocket.transform.Find("RocketObject").transform.rotation = Quaternion.Euler(currentAffordances.physicalObject.initialRotation.ToVector3());
        rocketVelocity.Value = currentAffordances.physicalObject.initialVelocity.ToVector3();
        rocketVelocityVector.SetActive(currentAffordances.physicalObject.showVelocityVector);
        rocketVelocityVector.GetComponent<DraggableVector>().SetInteractable(currentAffordances.physicalObject.velocityVectorIsInteractive);
        rocketVelocityVector.GetComponent<DraggableVector>().Redraw();
        
        playButton.GetComponent<PlayButton>().PlayWithoutRaising();
        rocket.GetComponent<Rigidbody>().isKinematic = false;
        rocket.GetComponent<Rigidbody>().velocity = rocketVelocity.Value;

        velocityLabel.SetActive(currentAffordances.physicalObject.showVelocityLabel);
        showVelocityEquation.Value = currentAffordances.physicalObject.showVelocityEquation;
        rocketIsInteractive.Value = currentAffordances.physicalObject.isInteractive;
        // Path Renderer config:
        showRocketPath.Value = currentAffordances.physicalObject.showTrace;
        showRocketPathButton.SetActive(currentAffordances.physicalObject.showTraceIsInteractive);
        showRocketPathButton.GetComponent<Toggle>().isOn = currentAffordances.physicalObject.showTrace;
        // Thrust Config:
        thrustIsActive.Value = currentAffordances.thrustForce.isActive;
        thrustShowVector.Value = currentAffordances.thrustForce.showVector;

        thrustForce.Value = Vector3.up * currentAffordances.thrustForce.initialMagnitude;
        thrustForce.Value = Quaternion.Euler(currentAffordances.physicalObject.initialRotation.ToVector3()) * thrustForce.Value;

        thrustShowEquation.Value = currentAffordances.thrustForce.showEquation;
        thrustShowLabel.SetActive(currentAffordances.thrustForce.showLabel);
        thrustIsInteractive.Value = currentAffordances.thrustForce.isInteractive;
        // Extra:
        referenceFrame.SetActive(currentAffordances.showReferenceFrame);
    }
}
