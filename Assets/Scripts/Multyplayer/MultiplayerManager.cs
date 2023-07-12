using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class MultiplayerManager : MonoBehaviourPunCallbacks
{
    private const string TankFolder = "Tanks/";
    private Transform[] _spawns;
    [SerializeField]private GameObject canvas, menu;

    private bool _leaving;
    
    
    private void Start()
    {
        _spawns = GetChildren(transform);
        CreateCanvas();
        
        SpawnPlayer();
    }

    private void OnApplicationQuit()
    {
        _leaving = true;
    }

    public void LeaveRoom()
    {
        _leaving = true;
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("StartRoom");
    }


    private static Transform[] GetChildren(Transform parent)
    {
        var count = parent.childCount;
        var list = new Transform[count];
        for (var i = 0; i < count; i++)
        {
            list[i] = parent.GetChild(i);
        }

        return list;
    }

    private void CreateCanvas()
    {
        var curCanvas = Instantiate(canvas);
        CanvasHandler.cameraSight = curCanvas.transform.GetChild(0);
        CanvasHandler.gunSight = curCanvas.transform.GetChild(1);

        CanvasHandler.menu = Instantiate(menu);
    }

    public void SpawnPlayer()
    {
        if (_leaving)
        {
            return;
        }
        
        var spawn = _spawns[Random.Range(0, _spawns.Length)];

        PhotonNetwork.Instantiate
        (TankFolder + PlayerPrefs.GetString(PlayerPrefsKeys.TankModel),
            spawn.position, spawn.rotation);
    }
}
