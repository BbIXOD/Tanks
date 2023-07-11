using Photon.Pun;
using UnityEngine;

public class ShowGunSight : MonoBehaviour
{
    private Camera _camera;
    private Transform _sight;

    private void Awake()
    {
        var view = GetComponent<PhotonView>();

        if (!view.IsMine)
        {
            enabled = false;
            return;
        }
        
        _sight = CanvasHandler.gunSight;
        _camera = Camera.main;
    }

    private void Update()
    {
        var tr = transform;
        var pos = new Ray(tr.position, tr.forward).PointOfHit();

        _sight.position = _camera.WorldToScreenPoint(pos);
    }
}
