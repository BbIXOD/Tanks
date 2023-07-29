using System;
using Photon.Pun;
using UnityEngine;

public class Module : MonoBehaviour, IModule, IPunObservable
{
    [SerializeField]private float health;
    [NonSerialized] public float maxHealth;
    public string caption;
    public float Health { get => health; set => SetHealthRPC(value); }
    [SerializeField]private GameObject connectedObject;
    public PhotonView view;
    
    private bool _destroyed;

    private void Awake()
    {
        maxHealth = health;
        view = GetComponent<PhotonView>();
        
        if (!view.IsMine)
        {
            return;
        }
    }

    private void SetHealthRPC(float value)
    {
        if (view.IsMine)
        {
            SetHealth(value);
        }
        else
        {
            view.RPC("SetHealth", view.Owner, value);
        }
    }
    
    [PunRPC]
    public void SetHealth(float value)
    {
        health = value;

        if (health > 0)
        {
            return;
        }

        view.RPC("OnDestroyed", RpcTarget.All);
    }

    
    //todo: maybe put this in func below
    [PunRPC]
    private void OnDestroyed()
    {
        Destroy(connectedObject);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (float)stream.ReceiveNext();
        }
    }
}
