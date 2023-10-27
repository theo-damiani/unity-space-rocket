using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct UiItems
{
    public GameObject Items;
    public bool IsActive;
}
public class LabelPositionManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> uiItems;
    [SerializeField] private float spacingTop;

    void Start()
    {
        for (int i = 0; i < uiItems.Count; i++)
        {
            
        }
    }
}
