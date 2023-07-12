using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    private Controls _controls;
    private View _playerView;
    private IMovable _movable;
    private PhotonView _view;
    private Cannon[] _cannons;
    private MenuController _menu;
    
    private float _sensitivity;


    private void Awake()
    {
        _view = GetComponent<PhotonView>();
        
        if (!_view.IsMine)
        {
            enabled = false;
            return;
        }

        _controls = new Controls();
        _movable = GetComponent<IMovable>();
        _playerView = GetComponent<View>();
        _cannons = GetComponentsInChildren<Cannon>();
        _menu = new MenuController(_view);

        _sensitivity = PlayerPrefs.GetFloat(PlayerPrefsKeys.Sensitivity);
    }

    private void OnEnable()
    {
        _controls.Enable();
        _controls.Menu.SetActive.performed += SetActive;
        

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        if (!_view.IsMine)
        {
            return;
        }
        _controls.Disable();
        _controls.Menu.SetActive.performed -= SetActive;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        SetDirection();
        Look();
        Shoot();
        Recharge();
    }

    private void SetActive(InputAction.CallbackContext context)
    {
        if (_controls.Basic.enabled)
        {
            _controls.Basic.Disable();
        }
        else
        {
            _controls.Basic.Enable();
            _sensitivity = PlayerPrefs.GetFloat(PlayerPrefsKeys.Sensitivity);
        }
        _menu.ChangeActivity();
    }
    
    private void SetDirection()
    {
        _movable.SetDir(_controls.Basic.Movement.ReadValue<Vector2>());
    }

    private void Look()
    {
        var delta = _controls.Basic.Aiming.ReadValue<Vector2>();
        delta *= _sensitivity;
        _playerView.TurnView(delta);
    }

    private void Shoot()
    {
        if (!_controls.Basic.Shooting.IsPressed())
        {
            return;
        }
        
        foreach (var cannon in _cannons)
        {
            cannon.Shoot();
        }
    }

    private void Recharge()
    {
        if (!_controls.Basic.Reload.IsPressed())
        {
            return;
        }

        foreach (var cannon in _cannons)
        {
            cannon.RechargeClip();
        }
    }
}

