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
    private PhotonView _view;
    
    private bool _destroyed;

    private void Awake()
    {
        _view = GetComponent<PhotonView>();
        
        if (!_view.IsMine)
        {
            enabled = false;
            return;
        }
        _maxHealth = health;
    }

    private void SetHealthRPC(float value)
    {
        if (_view.IsMine)
        {
            SetHealth(value);
        }
        else
        {
            _view.RPC("SetHealth", _view.Owner, value);
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

        PhotonNetwork.Destroy(connectedObject);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) { }
}
