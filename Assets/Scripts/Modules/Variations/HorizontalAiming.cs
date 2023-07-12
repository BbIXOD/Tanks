using UnityEngine;

public class HorizontalAiming : Aiming
{
    [SerializeField] private bool outer;
    
    protected override void SlerpToDesired()
    {
        var tr = transform;
        var myForward = tr.forward;
        var pForward = tr.parent.forward;

        var desiredForward = desired;
        desiredForward.y = pForward.y;
        
        var speed = rotationSpeed * Time.fixedDeltaTime / Vector3.Angle(myForward, desiredForward);

        myForward = Vector3.Slerp(myForward, desiredForward, speed);
        var desiredAngle = Vector3.SignedAngle(pForward, myForward, Vector3.up);
        
        var rotate = outer ? desiredAngle.BetweenOuter(minAngle, maxAngle)
            : desiredAngle.Between(minAngle, maxAngle);
        
        if (!rotate)
        {
            return;
        }

        var angles = tr.localEulerAngles;
        angles.y = desiredAngle;
        tr.localEulerAngles = angles;
    }
}