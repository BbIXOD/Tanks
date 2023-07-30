using UnityEngine;

public class MovementData : MonoBehaviour
{
    [SerializeField] public float
        speed,
        reverseSpeed,
        brakeTorque,
        standRotBrakeTorque,
        moveRotBrakeTorque,
        innerTurn,
        turnSpeed;

    [SerializeField] public float enginePower;

}
