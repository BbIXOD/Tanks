using UnityEngine;
using System;

public class VerticalAiming : Aiming
{

    protected override void SlerpToDesired()
    {
        var tr = transform;
        var myForward = tr.forward;
        var pForward = tr.parent.forward;

        var desiredForward = desired.normalized;
        desiredForward.x = myForward.x;
        desiredForward.z = myForward.z;

        var speed = rotationSpeed * Time.fixedDeltaTime / Vector3.Angle(myForward, desiredForward);
        myForward = Vector3.Slerp(myForward, desiredForward, speed);
        var desiredAngle = Vector3.SignedAngle(myForward, pForward, Vector3.up);

        if (!desiredAngle.Between(minAngle, maxAngle))
        {
            return;
        }

        tr.forward = myForward;
    }
}
