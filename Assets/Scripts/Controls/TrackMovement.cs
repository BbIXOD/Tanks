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
        if (dir.y == 0)
        {
            leftTrack.power = dir.x * _movementData.speed;
            rightTrack.power = -leftTrack.power;
            
        }
        else
        {
            var speed = Mathf.Sign(dir.y) > 0 ? _movementData.speed : -_movementData.reverseSpeed;
            
            leftTrack.power = rightTrack.power = speed;

            switch (dir.x)
            {
                case < 0:
                    leftTrack.power *= _movementData.innerTurn;
                    rightTrack.power *= _movementData.outerTurn;
                    break;
                case > 0:
                    leftTrack.power *= _movementData.outerTurn;
                    rightTrack.power *= _movementData.innerTurn;
                    break;
            }
        }
    }
}
