using System;
using UnityEngine;

public class Track : MonoBehaviour
{
    [NonSerialized]public float power, torque, sideFric;
    private WheelCollider[] _wheels;


    private void Awake()
    {
        _wheels = GetComponentsInChildren<WheelCollider>();
    }

    private void FixedUpdate()
    {
        power *= Time.timeScale;
        foreach (var wheel in _wheels)
        {
            wheel.brakeTorque = torque;
            wheel.motorTorque = power;
            var sf = wheel.sidewaysFriction;
            sf.stiffness = sideFric;
            wheel.sidewaysFriction = sf;
        }
    }
}
