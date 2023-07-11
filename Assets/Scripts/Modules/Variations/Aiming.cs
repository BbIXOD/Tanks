using Photon.Pun;
using UnityEngine;

public abstract class Aiming : MonoBehaviour
{

    protected Vector3 desired;

    [SerializeField]
    protected float 
        minAngle,
        maxAngle,
        rotationSpeed;

    private Camera _cameraMain;
    private Transform _placeToAim;

    private void Start()
    {
        var view = GetComponent<PhotonView>();

        if (!view.IsMine)
        {
            enabled = false;
            return;
        }
        
        _cameraMain = Camera.main;
        _placeToAim = CanvasHandler.cameraSight;
    }

    protected void FixedUpdate()
    {
        desired = GetDesiredRot();

        SlerpToDesired();
    }

    private Vector3 GetDesiredRot()
    {
            var desiredRay = _cameraMain.ScreenPointToRay(_placeToAim.position);
            var point = desiredRay.PointOfHit();
            var desiredForward = point - transform.position;

            return desiredForward;
    }
    
    protected static float AngleDirection(float startAngle, float endAngle, out float diff)
    {
        diff = endAngle - startAngle;
        var delta = Mathf.Abs(diff);
        if (delta < float.Epsilon)
        {
            return 0;
        }

        var dir = Mathf.Sign(diff);
        dir *= delta > 180 ? -1 : 1;

        return dir;
    }

    protected virtual void SlerpToDesired() {}
}
