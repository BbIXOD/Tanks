using Photon.Pun;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private const string Folder = "BUllets/";
    
    private GunData _data;

    private float _charge = 1;
    private float _chargeTime;
    private int _clip;
    private void Awake()
    {
        var view = GetComponent<PhotonView>();

        if (!view.IsMine)
        {
            enabled = false;
            return;
        }
        
        _data = GetComponent<GunData>();
        _clip = _data.clip;
    }

    private void FixedUpdate()
    {
        if (_charge >= 1)
        {
            return;
        }
        
        _charge += Time.fixedDeltaTime / _chargeTime;
    }

    public void Shoot()
    {
        if (_charge < 1)
        {
            return;
        }

        var tr = transform;

        _charge = 0;
        _clip--;
        PhotonNetwork.Instantiate(Folder + _data.bullet.name, tr.position, tr.rotation);
        
        var empty = _clip == 0;

        _chargeTime = empty ? _data.recharge : _data.inClipRecharge;

        if (!empty)
        {
            return;
        }
        
        _clip = _data.clip;
    }

    public void RechargeClip()
    {
        if (_clip == _data.clip || _data.clip == 1)
        {
            return;
        }

        _charge = 0;
        _chargeTime = _data.recharge;
        _clip = _data.clip;
        
    }
}
