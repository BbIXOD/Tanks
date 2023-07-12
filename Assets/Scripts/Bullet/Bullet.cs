using System;
using System.Threading.Tasks;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    [NonSerialized]public Transform myTransform;

    [SerializeField] private float 
        speed,
        airFriction,
        airRotation;
    public float armourMult,
        damage;
    
    [SerializeField] private int lastingTime;

    public float Speed
    {
        get => speed;
        set => SetSpeed(value);
    }

    private PhotonView _view;

    private void Awake()
    {
        _view = GetComponent<PhotonView>();
        myTransform = transform;
        
        if (!_view.IsMine)
        {
            return;
        }

        DestroyTimer();
    }

    private void FixedUpdate()
    {
        if (!_view.IsMine)
        {
            return;
        }

        myTransform.position += myTransform.TransformDirection(Speed * Time.fixedDeltaTime * Vector3.forward);
        Speed -= Time.fixedDeltaTime * airFriction;
        
        myTransform.Rotate(0, -Time.fixedDeltaTime * airRotation, 0);
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
}
