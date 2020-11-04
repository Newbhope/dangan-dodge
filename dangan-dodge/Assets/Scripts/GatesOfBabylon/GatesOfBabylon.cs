using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatesOfBabylon : MonoBehaviour
{
    GateController[] controllers;

    void Start()
    {
        controllers = GetComponentsInChildren<GateController>();
    }

    public void ActivateGates()
    {
        foreach (GateController controller in controllers)
        {
            controller.ActivateGate();
        }
    }
}
