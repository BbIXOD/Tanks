using UnityEngine;
using Photon.Pun;

public class AdditionalModule : MonoBehaviour, IModule
{
    [SerializeField]private Module module;
    public float Health {get => module.Health; set => module.Health = value;}

    private void Awake()
    {
        enabled = PhotonNetwork.IsMasterClient;
    }
}
