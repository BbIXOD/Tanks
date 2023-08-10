using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MultiplayerManager : MonoBehaviourPunCallbacks
{
    private const string TankFolder = "Tanks/";
    private Transform[] _spawns;
    [SerializeField]private GameObject canvas, menu;
     private GameObject _player;

    private bool _leaving;
    
    
    private void Start()
    {
        _spawns = GetChildren(transform);
        CreateCanvas();

        SingletonHandler.damageShower = GetComponent<DamageShower>();
        
        SpawnPlayer();
    }

    private void OnApplicationQuit()
    {
        _leaving = true;
    }

    public void LeaveRoom()
    {
        _leaving = true;
        PhotonNetwork.Destroy(_player);
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

    //todo: remove magic numbers
    private void CreateCanvas()
    {
        var curCanvas = Instantiate(canvas).transform;
        SingletonHandler.inGameCanvas = curCanvas;
        SingletonHandler.cameraSight = curCanvas.GetChild(0);
        SingletonHandler.gunSight = curCanvas.GetChild(1);
        SingletonHandler.healthHolder = curCanvas.GetChild(2);

        SingletonHandler.menu = Instantiate(menu);
        var button = SingletonHandler.menu.transform.GetComponentInChildren<Button>();
        button.onClick.AddListener(LeaveRoom);


    }

    public void SpawnPlayer()
    {
        if (_leaving)
        {
            return;
        }
        
        var spawn = _spawns[Random.Range(0, _spawns.Length)];

        _player = PhotonNetwork.Instantiate
        (TankFolder + PlayerPrefsKeys.tankModel,
            spawn.position, spawn.rotation);
    }
}
