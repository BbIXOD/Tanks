using System;
using UnityEngine;

public class Track : MonoBehaviour
{
    [NonSerialized]public float power;
    private Rigidbody _rigidbody;
    private MovementData _movementData;
    [SerializeField]private GroundChecker checker;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _movementData = GetComponentInParent<MovementData>();
    }

    private void FixedUpdate()
    {
        if (!checker.OnGround)
        {
            return;
        }

        power *= Time.fixedDeltaTime;


        _rigidbody.velocity = transform.forward
                              * Mathf.Lerp(_rigidbody.velocity.x, power, _movementData.enginePower);
    }
}
