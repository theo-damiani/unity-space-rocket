using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;

public class AppManager : Singleton<AppManager>
{
    [Header("Affordances")]
    [SerializeField] private Affordances defaultAffordances;
    private Affordances currentAffordances;

    [Header("Camera")]
    [SerializeField] private CameraManager mainCamera;
    [SerializeField] private RectTransform cameraControls;
    [SerializeField] private ToggleIcons cameraLockingToggle;
    [SerializeField] private RectTransform cameraZoomSlider;

    [Header("Main App Controls")]
    [SerializeField] private RectTransform playButton;
    [SerializeField] private RectTransform resetButton;
    [SerializeField] private RectTransform metaPanel;

    [Header("Rocket Variables")]
    [SerializeField] private GameObject rocket;
    [SerializeField] private BoolVariable rocketIsInteractiveUp;
    [SerializeField] private BoolVariable rocketIsInteractiveDown;
    [SerializeField] private BoolVariable rocketIsInteractiveLeft;
    [SerializeField] private BoolVariable rocketIsInteractiveRight;
    [SerializeField] private Vector3Variable rocketVelocity;
    [SerializeField] private GameObject rocketVelocityVector;
    [SerializeField] private BoolVariable showVelocityEquation;
    [SerializeField] private GameObject velocityLabel;
    [SerializeField] private BoolVariable showRocketPath;
    [SerializeField] private RectTransform showRocketPathToggle;

    [Header("Thrust Variables")]
    [SerializeField] private BoolVariable thrustIsActive;
    [SerializeField] private BoolVariable thrustIsInteractive;
    [SerializeField] private Vector3Variable thrustForce;
    [SerializeField] private BoolVariable thrustShowVector;
    [SerializeField] private GameObject thrustShowLabel;
    [SerializeField] private BoolVariable thrustShowEquation;

    [Header("Rocket Controls")]
    [SerializeField] private RectTransform keyUpBtn;
    [SerializeField] private RectTransform keyDownBtn;
    [SerializeField] private RectTransform keyLeftBtn;
    [SerializeField] private RectTransform keyRightBtn;
    [SerializeField] private VerticalLayoutGroup keyBtnLayout;

    [Header("Extra")]
    [SerializeField] private GameObject referenceFrame;
    [SerializeField] private RectTransform asteroidButton;
    [SerializeField] private FloatVariable asteroidCollisionSpeed;
    [SerializeField] private LabelPositionManager equationsManager;

    public override void Awake()
    {
        base.Awake();
        
        currentAffordances = Instantiate(defaultAffordances);
        ResetApp();

        #if !UNITY_EDITOR && UNITY_WEBGL
            // disable WebGLInput.captureAllKeyboardInput so elements in web page can handle keyboard inputs
            WebGLInput.captureAllKeyboardInput = false;
        #endif
    }

    public void ResetAppFromJSON(string affordanceJson)
    {
        currentAffordances = Instantiate(defaultAffordances);
        JsonUtility.FromJsonOverwrite(affordanceJson, currentAffordances);
        ResetApp();
    }

    public void ResetApp()
    {
        // Main control config:
        playButton.gameObject.SetActive(currentAffordances.showPlayButton);
        resetButton.gameObject.SetActive(currentAffordances.showResetButton);
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
        rocketIsInteractiveUp.Value = currentAffordances.physicalObject.isInteractiveUp;
        rocketIsInteractiveDown.Value = currentAffordances.physicalObject.isInteractiveDown;
        rocketIsInteractiveRight.Value = currentAffordances.physicalObject.isInteractiveRight;
        rocketIsInteractiveLeft.Value = currentAffordances.physicalObject.isInteractiveLeft;

        keyUpBtn.gameObject.SetActive(currentAffordances.physicalObject.isInteractiveUp);
        keyDownBtn.gameObject.SetActive(currentAffordances.physicalObject.isInteractiveDown);
        keyLeftBtn.gameObject.SetActive(currentAffordances.physicalObject.isInteractiveLeft);
        keyRightBtn.gameObject.SetActive(currentAffordances.physicalObject.isInteractiveRight);

        if (currentAffordances.physicalObject.isInteractiveLeft || currentAffordances.physicalObject.isInteractiveRight)
        {
            keyLeftBtn.parent.gameObject.SetActive(true);
        }
        else
        {
            keyLeftBtn.parent.gameObject.SetActive(false);
        }

        if (currentAffordances.physicalObject.isInteractiveUp)
        {
            keyBtnLayout.padding.top = 0;
        }
        else
        {
            keyBtnLayout.padding.top = 20;
        }

        if (currentAffordances.physicalObject.isInteractiveDown)
        {
            keyBtnLayout.padding.bottom = 0;
        }
        else
        {
            keyBtnLayout.padding.bottom = 20;
        }

        // Path Renderer config:
        showRocketPath.Value = currentAffordances.physicalObject.showTrace;
        showRocketPathToggle.gameObject.SetActive(currentAffordances.physicalObject.showTraceIsInteractive);
        showRocketPathToggle.GetComponent<ToggleIcons>().SetWithoutRaising(currentAffordances.physicalObject.showTrace);
        // Thrust Config:
        thrustIsActive.Value = currentAffordances.thrustForce.isActive;
        thrustShowVector.Value = currentAffordances.thrustForce.showVector;

        thrustForce.Value = Vector3.up * currentAffordances.thrustForce.initialMagnitude;
        thrustForce.Value = Quaternion.Euler(currentAffordances.physicalObject.initialRotation.ToVector3()) * thrustForce.Value;

        thrustShowEquation.Value = currentAffordances.thrustForce.showEquation;
        thrustShowLabel.SetActive(currentAffordances.thrustForce.showLabel);
        thrustIsInteractive.Value = currentAffordances.thrustForce.isInteractive;

        // Camera:
        Vector3 cameraPos = currentAffordances.camera.position.ToVector3();
        cameraLockingToggle.SetWithoutRaising(currentAffordances.camera.isLockedOnObject);

        Slider zoomSlider = cameraZoomSlider.GetComponent<Slider>();
        float minDistanceToObject = (rocket.transform.localScale.x + rocket.transform.localScale.y + rocket.transform.localScale.z)/3;
        mainCamera.InitCamera(
            rocket.transform,
            cameraPos,
            currentAffordances.camera.isLockedOnObject,
            minDistanceToObject,
            zoomSlider
        );

        mainCamera.transform.localRotation = Quaternion.Euler(currentAffordances.camera.rotation.ToVector3());

        cameraControls.gameObject.SetActive(currentAffordances.camera.showCameraControl);
        
        // Extra:
        referenceFrame.SetActive(currentAffordances.showReferenceFrame);
        asteroidButton.gameObject.SetActive(currentAffordances.showAsteroidButton);
        asteroidCollisionSpeed.Value = currentAffordances.asteroidCollisionForce;

        equationsManager.Start();

        // UI position
        bool rocketPanelActivation = currentAffordances.showAsteroidButton ||
            currentAffordances.physicalObject.isInteractiveUp ||
            currentAffordances.physicalObject.isInteractiveDown ||
            currentAffordances.physicalObject.isInteractiveLeft ||
            currentAffordances.physicalObject.isInteractiveRight ||
            currentAffordances.thrustForce.isInteractive;
        asteroidButton.parent.gameObject.SetActive(rocketPanelActivation);
        
        if (!currentAffordances.showPlayButton && !currentAffordances.showResetButton)
        {
            metaPanel.gameObject.SetActive(false);
            cameraControls.GetComponent<RectTransform>().anchoredPosition = new Vector2(25, -25);
        }
        else
        {
            metaPanel.gameObject.SetActive(true);
            cameraControls.GetComponent<RectTransform>().anchoredPosition = new Vector2(25, -110);
        }
        velocityLabel.GetComponent<VectorLabel>().SetSpriteOrientation();
        thrustShowLabel.GetComponent<VectorLabel>().SetSpriteOrientation();
    }
}
