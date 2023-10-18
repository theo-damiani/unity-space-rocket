using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Variable", menuName = "Variables / Bool")]
public class BoolVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    [SerializeField] private bool _value;
    [SerializeField] private GameEvent OnUpdateEvent;

    public bool Value 
    {
        get {return _value;}

        set
        {
            _value = value;
            if (OnUpdateEvent)
                OnUpdateEvent.Raise();
        }
    }

    public void SetReversedValue(bool value)
    {
        _value = !value;
            if (OnUpdateEvent)
                OnUpdateEvent.Raise();
    }
}
