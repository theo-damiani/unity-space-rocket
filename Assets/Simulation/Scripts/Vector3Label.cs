using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Vector3Label : MonoBehaviour
{
    [SerializeField] private Vector3Reference vector3Variable;
    [SerializeField] private int fontSize;
    [SerializeField] private TextMeshProUGUI labelX;
    [SerializeField] private TextMeshProUGUI labelY;
    [SerializeField] private TextMeshProUGUI labelZ;
    
    void Start()
    {
        labelX.fontSize = fontSize;
        labelY.fontSize = fontSize;
        labelZ.fontSize = fontSize;
        SetText();
    }

    public void SetText()
    {
        labelX.text = vector3Variable.Value.x.ToString("F2");
        labelY.text = vector3Variable.Value.y.ToString("F2");
        labelZ.text = vector3Variable.Value.z.ToString("F2");
    }

    void OnEnable()
    {
        GameEvent gameEvent = vector3Variable.OnUpdateEvent;
        if (gameEvent)
            gameEvent.OnRaise += SetText;
    }

    void OnDisable()
    {
        GameEvent gameEvent = vector3Variable.OnUpdateEvent;
        if (gameEvent)
            gameEvent.OnRaise -= SetText;
    }
}
