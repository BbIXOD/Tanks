using Photon.Pun;
using UnityEngine;

public class View : MonoBehaviour
{
    private const int 
        MinXRot = -40,
        MaxXRot = 90;

    private float
        _verticalRot,
        _horizontalRot;
    
    private Camera _camera;
    private PhotonView _view;
    [SerializeField] private Transform anchor;
    
    private readonly Vector3 _offset = new(0, 2, -5);

    private void Awake()
    {
        _view = GetComponent<PhotonView>();
        if (!_view.IsMine)
        {
            Destroy(this);
            return;
        }
        
        _camera = Camera.main;

        var angles = transform.eulerAngles;
        _verticalRot -= angles.x;
        _horizontalRot += angles.y;
    }

    public void TurnView(Vector2 delta)
    {
        _verticalRot += delta.y;
        _horizontalRot += delta.x;
        
        _verticalRot = Mathf.Clamp(_verticalRot, MinXRot, MaxXRot);

        var rotation = Vector3.left * _verticalRot;
        rotation.y = _horizontalRot;

        anchor.eulerAngles = rotation;
        
        
        var cameraTransform = _camera.transform;
        cameraTransform.position = anchor.TransformDirection(_offset) + anchor.position;
        cameraTransform.LookAt(anchor);
    }
}
