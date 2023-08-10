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

    private string _ownerName;
    
    private bool _destroyed;
    public bool showable = true;

    private void Awake()
    {
        maxHealth = health;
        view = GetComponent<PhotonView>();

        _ownerName = view.Owner.NickName;
    }

    private void SetHealthRPC(float value)
    {
        SingletonHandler.damageShower
            .ShowDamage((int)(health - value), _ownerName, caption, value <= 0);
        
        view.RPC("SetHealth", view.Owner, value);
    }
    
    [PunRPC]
    private void SetHealth(float value)
    {
        health = value;

        if (health > 0)
        {
            return;
        }

        view.RPC("OnDestroyed", RpcTarget.All);
    }

    private void LogDamage(float newHealth)
    {
        if (!showable) return;
        
        DamageShower.Instance.ShowDamage
            ((int)(health - newHealth), _ownerName, connectedObject.name, health - newHealth <= 0);
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
