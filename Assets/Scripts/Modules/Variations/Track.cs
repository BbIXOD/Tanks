using System;
using UnityEngine;

public class Track : MonoBehaviour
{
    [NonSerialized]public float power;
    private float _curPower;
    private Vector3 _velocityAccum;
    
    private Rigidbody _rigidbody;
    private MovementData _movementData;
    [SerializeField]private GroundChecker checker;
    
    public bool OnGround => checker.OnGround;

    private void Awake()
    {
        _rigidbody = GetComponentInParent<Rigidbody>();
        _movementData = GetComponentInParent<MovementData>();
    }
    
}
