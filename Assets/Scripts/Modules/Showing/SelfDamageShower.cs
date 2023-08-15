using System;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class SelfDamageShower : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    private TMP_Text _damage, _name;
    private RectTransform _arrowTransform;
    private PhotonView _view;
    private Camera _camera;

    private void Awake()
    {
        _view = GetComponent<PhotonView>();
        if (!_view.IsMine) return;
        
        _arrowTransform = arrow.GetComponent<RectTransform>();
        
        _damage = arrow.transform.GetChild(0).GetComponent<TMP_Text>(); //a bit of hardcode
        _name = arrow.transform.GetChild(1).GetComponent<TMP_Text>();

        _camera = Camera.main;
    }

    public void Show(int damage, string moduleCaption, Vector3 bulletPos)
    {
        _view.RPC("ShowWithRPC", _view.Owner, damage, moduleCaption, bulletPos);
    }

    [PunRPC]
    private void ShowWithRPC(int damage, string moduleCaption, Vector3 bulletPos)
    {
        _damage.text = Convert.ToString(damage);
        _name.text = moduleCaption;
        var direction = bulletPos - transform.position;
        var forward = new Vector3(direction.x, direction.z, 0);
        _arrowTransform.forward = forward;
        Instantiate(arrow, SingletonHandler.inGameCanvas);

    }
}
