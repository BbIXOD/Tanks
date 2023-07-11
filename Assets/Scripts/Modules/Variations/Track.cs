using System;
using UnityEngine;

public class Track : MonoBehaviour
{
    [NonSerialized]public float power;
    private Rigidbody _rigidbody;
    private MovementData _movementData;

    private int _colsNumber;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _movementData = GetComponentInParent<MovementData>();
    }

    private void FixedUpdate()
    {
        power *= Time.fixedDeltaTime;


        _rigidbody.velocity = transform.forward
                              * Mathf.Lerp(_rigidbody.velocity.x, power, _movementData.enginePower);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        _colsNumber++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        _colsNumber--;
    }
}
