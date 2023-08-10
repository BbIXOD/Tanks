using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private float _timer;
    private const float MaxTime = 3;

    private readonly List<GameObject> _showers = new();
    private Transform _wrapper;
    
    private bool _hidden;

    private void Awake()
    {
        _wrapper = SingletonHandler.healthHolder;
        _wrapper.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (_timer > 0)
        {
            _timer -= Time.fixedDeltaTime;
            return;
        }
        
        if (_hidden) return;
        Hide();
        _hidden = true;
    }

    public void Scan()
    {
        _hidden = false;
        var tr = transform;
        
        if (!Physics.Raycast(tr.position, tr.forward, out var hit))
        {
            return;
        }

        var success = hit.transform.root.TryGetComponent<HealthShower>(out var shower);
        if (!success)
        {
            return;
        }

        _timer = MaxTime;
        DestroyAllInList();
        _wrapper.gameObject.SetActive(true);
        shower.ShowHealth(_showers, _wrapper);
    }
    
    private void Hide()
    {
        DestroyAllInList();
        _wrapper.gameObject.SetActive(false);
    }

    private void DestroyAllInList()
    {
        foreach (var shower in _showers)
        {
            Destroy(shower);
        }
        _showers.Clear();
    }
}
