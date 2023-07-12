using Photon.Pun;
using UnityEngine;

public class Penetrator : MonoBehaviour
{
    private const int BufferSize = 100;
    private Bullet _bullet;
    private PhotonView _view;

    private GameObject _current;
    private float _curSolidity;

    private void Awake()
    {
        
        _view = GetComponent<PhotonView>();

        if (!_view.IsMine)
        {
            enabled = false;
            return;
        }
        
        _bullet = GetComponent<Bullet>();
    }

    private void Start()
    {
        FixedUpdate();
    }

    private void FixedUpdate()
    {
        var maxDist = _bullet.Speed * Time.fixedDeltaTime;
        var hits = new RaycastHit[BufferSize];
        var length = Physics.RaycastNonAlloc
            (_bullet.myTransform.position, _bullet.myTransform.forward, hits, maxDist);
        //hits = hits[..length].OrderBy(hit => hit.distance).ToArray();
        
        for (var i = 0; i < length; i++)
        {
            ArmourInteract(hits[i], out var destroyed, out var isArmour);
            if (destroyed)
            {
                return;
            }
            ModuleInteract(hits[i], out var isModule);
            if (isArmour && !isModule)
            {
                continue;
            }

            _bullet.Speed = float.NegativeInfinity;
            return;
        }
    }

    private void ArmourInteract(RaycastHit hit,out bool destroyed, out bool isArmour)
    {
        var other = hit.collider;

        destroyed = false;
        isArmour = other.TryGetComponent<Armour>(out var armour);

        if (!isArmour)
        {
            return;
        }

        var chicness = armour.solidity * _bullet.armourMult
                       / Mathf.Cos(Vector3.Angle(hit.normal, -transform.forward) * Mathf.Deg2Rad);
        
        
        _bullet.Speed -= chicness;

        destroyed = _bullet.Speed <= 0;
    }

    private void ModuleInteract(RaycastHit hit, out bool isModule)
    {
        var other = hit.collider;

        isModule = other.TryGetComponent<IModule>(out var module);

        if (!isModule)
        {
            return;
        }
        
        module.Health -= _bullet.damage;
    }
}
