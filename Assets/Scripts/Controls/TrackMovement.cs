using UnityEngine;
using System;
using System.Linq;
using Photon.Pun;

public class TrackMovement : MonoBehaviour, IMovable
{
    [SerializeField]private GroundChecker[] checkers;
    private MovementData _movementData;
    private Rigidbody _tankRb;
    
    private float _curSpeed;
    private Vector3 _curVelocity = Vector3.zero;

    private void Awake()
    {
        _movementData = GetComponent<MovementData>();
        _tankRb = GetComponent<Rigidbody>();

        if (GetComponent<PhotonView>().IsMine) return;
            
        Destroy(_tankRb);
        enabled = false;
    }

    public void SetDir(Vector2 dir)
    {
        if (checkers.Any(checker => checker.OnGround))
        {
            var sign = Math.Sign(dir.y);

            var speed = sign == 1 ? _movementData.speed : _movementData.reverseSpeed;
            speed *= sign;
            speed *= dir.x == 0 ? 1 : _movementData.turnSpeedMult;
            var power = sign == Math.Sign(_curSpeed) && _curSpeed != 0f ? _movementData.enginePower : _movementData.breakPower;
            
            _curSpeed = Mathf.Lerp(_curSpeed, speed, power * Time.fixedDeltaTime);
            _curVelocity = _curSpeed * Time.fixedDeltaTime * _tankRb.transform.forward;
        }
        _curVelocity += Physics.gravity;

        _tankRb.velocity = _curVelocity;


        var rotation = 0f;
        if (checkers[0].OnGround)
            rotation += dir.x * (dir.y == 0 ? _movementData.turnOnStand : _movementData.turnOnMove);
        if (checkers[1].OnGround) 
            rotation += dir.x * (dir.y == 0 ? _movementData.turnOnStand : _movementData.turnOnMove);
        
        rotation *= dir.y == 0 ? 1: Mathf.Sign(dir.y); 
        rotation *= Time.fixedDeltaTime; 
        var rotDelta = Quaternion.Euler(0, rotation, 0);
        
        _tankRb.MoveRotation(_tankRb.rotation * rotDelta);
    }
}
