using Photon.Pun;
using UnityEngine;

public class ShowGunSight : MonoBehaviour
{
    private Camera _camera;
    private Transform _sight, _myTr;

    private bool _showing;

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
        _myTr = transform;
    }

    private void Update()
    {
        var pos = new Ray(_myTr.position, _myTr.forward).PointOfHit();

        _sight.position = _camera.WorldToScreenPoint(pos);
    }
}
