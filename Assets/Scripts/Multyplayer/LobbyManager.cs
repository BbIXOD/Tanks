using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField
        nickname,
        id;
    [SerializeField] private GameObject loadingScreen;
        
    
    private void Awake()
    {
        PhotonNetwork.GameVersion = "1.0";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnConnectedToMaster()
    {
        Destroy(loadingScreen);
    }
    

    public void JoinRoom()
    { 
        //PhotonNetwork.NickName = CheckNickName(nickname.text);

        PhotonNetwork.JoinOrCreateRoom(CheckID(id.text), null, null);
    }

    private static string CheckNickName(string nick)
    {
        const string defaultName = "Player";
        
        var nickName = nick.Trim();
        return nickName == "" ? defaultName : nickName;

    }

    private static string CheckID(string id)
    {
        const string defaultID = "Server";
        
        var nickName = id.Trim();
        return nickName == "" ? defaultID : id;
    }
}
