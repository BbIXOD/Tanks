using Photon.Pun;
using UnityEngine;

public class Module : MonoBehaviour, IModule, IPunObservable
{
    [SerializeField]private float health;
    private float _maxHealth;
    [SerializeField] private string caption;
    public float Health { get => health; set => SetHealthRPC(value); }
    [SerializeField]private GameObject connectedObject;
    [SerializeField]private ModuleShower shower;
    public PhotonView view;
    
    private bool _destroyed;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        
        if (!view.IsMine)
        {
            return;
        }
        _maxHealth = health;
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
        if (value < health)
        {
            shower.ShowDamage(health, _maxHealth, caption);
        }

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
