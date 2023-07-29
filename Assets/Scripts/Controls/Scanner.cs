using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private float _timer = MaxTime;
    private const float MaxTime = 60;

    private readonly List<GameObject> _showers = new();
    private Transform _wrapper;

    private void Awake()
    {
        _wrapper = CanvasHandler.healthHolder;
        _wrapper.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        _timer -= Time.fixedDeltaTime;
        
        if (_timer > 0) return;

        Hide();
    }

    public void Scan()
    {
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
