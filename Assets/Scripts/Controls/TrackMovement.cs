using UnityEngine;

public class TrackMovement : MonoBehaviour, IMovable
{
    [SerializeField]private Track leftTrack;
    [SerializeField]private Track rightTrack;
    private MovementData _movementData;

    private void Awake()
    {
        _movementData = GetComponent<MovementData>();
    }

    public void SetDir(Vector2 dir)
    {
        if (dir == Vector2.zero)
        {
            leftTrack.torque = rightTrack.torque = _movementData.brakeTorque;
            leftTrack.power = rightTrack.power = 0;
            return;
        }
        
        if (dir.y == 0)
        {
            leftTrack.torque = rightTrack.torque = _movementData.standRotBrakeTorque;
            leftTrack.power = dir.x * _movementData.turnSpeed;
            
            rightTrack.power = -leftTrack.power;
            
        }
        else
        {
            var speed = Mathf.Sign(dir.y) > 0 ? _movementData.speed : -_movementData.reverseSpeed;
            leftTrack.torque = rightTrack.torque = 0;
            leftTrack.power = rightTrack.power = speed;

            switch (dir.x)
            {
                case < 0:
                    leftTrack.torque = _movementData.moveRotBrakeTorque;
                    leftTrack.power *= _movementData.innerTurn;
                    break;
                case > 0:
                    rightTrack.torque = _movementData.moveRotBrakeTorque;
                    rightTrack.power *= _movementData.innerTurn;
                    break;
            }
        }
    }
}
