using UnityEngine;
using Photon.Pun;

public class MenuController
{
    private const string Menu = nameof(Menu);
    private readonly GameObject _menu;
    private bool _enabled;

    public MenuController(PhotonView view)
    {
        
        if (!view.IsMine)
        {
            return;
        }
        _menu = SingletonHandler.menu;
        _menu.SetActive(_enabled);
    }

    public void ChangeActivity()
    {
        _enabled = !_enabled;
        _menu.SetActive(_enabled);
        Cursor.visible = _enabled;
    }
}
