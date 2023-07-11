using System;
using System.Threading.Tasks;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    private Transform _myTransform;

    private const float LerpSpeed = 100;
    
    [SerializeField] private float 
        speed,
        airFriction,
        airRotation;
    public float armourMult,
        damage;
    
    [SerializeField] private int lastingTime;

    [NonSerialized]public Vector3
        newPos,
        newForward;

    public float Speed
    {
        get => speed;
        set => SetSpeed(value);
    }

    private PhotonView _view;

    private void Awake()
    {
        _view = GetComponent<PhotonView>();
        
        if (!_view.IsMine)
        {
            return;
        }

        _myTransform = transform;
        newPos = _myTransform.position;
        newForward = _myTransform.forward;
        
        DestroyTimer();
    }

    private void FixedUpdate()
    {
        if (!_view.IsMine)
        {
            return;
        }

        newPos += _myTransform.TransformDirection(Speed * Time.fixedDeltaTime * Vector3.forward);
        Speed -= Time.fixedDeltaTime * airFriction;
        
        newForward.y -= Time.fixedDeltaTime * airRotation;
        
        _view.RPC("UpdateTransform", RpcTarget.Others, newPos, newForward);
    }

    private void Update()
    {
        _myTransform.position =
            Vector3.Lerp(_myTransform.position, newPos, LerpSpeed * Time.fixedDeltaTime);
        _myTransform.forward = 
            Vector3.Slerp(_myTransform.forward, newForward, LerpSpeed * Time.fixedDeltaTime);
    }

    private async void DestroyTimer()
    {
        await Task.Delay(lastingTime);
        if (this != null)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private void SetSpeed(float value)
    {
        speed = value;

        if (speed > 0)
        {
            return;
        }
        
        PhotonNetwork.Destroy(gameObject);
        enabled = false;
    }

    [PunRPC]
    private void UpdateTransform(Vector3 position, Vector3 forward)
    {
        newPos = position;
        newForward = forward;
    }
}
