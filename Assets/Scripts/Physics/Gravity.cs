using System;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField]private float scale;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rb.AddForce(Vector3.down * scale, ForceMode.Acceleration);
    }
}
