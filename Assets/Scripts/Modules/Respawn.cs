using UnityEngine;
using Photon.Pun;

public class Respawn : MonoBehaviourPunCallbacks
{
    private PhotonView _view;
    private bool _onQuit;
    
    private void Awake()
    {
        _view = GetComponent<PhotonView>();
    }

    private void OnApplicationQuit()
    {
        _onQuit = true;
    }

    private void OnDestroy()
    {
        if (!_view.IsMine || _onQuit)
        {
            return;
        }

        var manager = FindObjectOfType<MultiplayerManager>();
        manager.SpawnPlayer();
    }
}
