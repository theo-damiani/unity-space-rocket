using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class MotionData
{
    public Motion motion;
    public bool isInit;
    public bool isActive;
}

[RequireComponent(typeof(Rigidbody))]
public class MotionManager : MonoBehaviour
{
    [SerializeField] private MotionData[] listMotionData;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        for (int i = 0; i < listMotionData.Length; i++)
        {
            if (listMotionData[i].isActive)
            {
                if (!listMotionData[i].isInit)
                {
                    listMotionData[i].motion.InitMotion(rb);
                    listMotionData[i].isInit = true;
                }
                listMotionData[i].motion.ApplyMotion(rb);
            }
        }
    }

    public void SetMotionIndex(int index)
    {
        if (index >= listMotionData.Length)
        {return;}

        for (int i = 0; i < listMotionData.Length; i++)
        {
            if (i==index)
            {
                listMotionData[i].isActive = true;
            }
            else
            {
                listMotionData[i].isActive = false;
            }
        }
    }

    public void StopMotionIndex(int index)
    {
        if (index >= listMotionData.Length)
        {return;}

        listMotionData[index].isActive = false;
    }

    public void StopAllMotion()
    {
        for (int i = 0; i < listMotionData.Length; i++)
        {
            listMotionData[i].isActive = false;
        }
    }
}
