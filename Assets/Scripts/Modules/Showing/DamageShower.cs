using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageShower : MonoBehaviour
{
    private const string DestroySign = " X";
    private const float Difference = 20f;
    private const int Time = 2;
    
    [SerializeField] private GameObject textInstance;

    private readonly List<Transform> _texts = new();
    public static DamageShower Instance { get; private set; }

    private Coroutine _lastTimer;
    private (int, string, string, GameObject) _previous;
    

    private void Awake()
    {
        Instance = this;
    }

    public void ShowDamage(int damage, string owner, string moduleName, bool destroyed = false)
    {
        void SetText(TMP_Text inst)
        {
            inst.text = owner + " " + moduleName  + " " + Convert.ToString(damage);
            if (destroyed) inst.text += DestroySign;
        }
        
        if (_previous.Item2 == owner && _previous.Item3 == moduleName && _previous.Item4)
        {
            damage += _previous.Item1;
            SetText(_previous.Item4.GetComponent<TMP_Text>());
            _previous.Item1 = damage;
            StopCoroutine(_lastTimer);
            _lastTimer = StartCoroutine(DestroyEvent(_previous.Item4));
            return;
        }

        var instantiated = Instantiate(textInstance, SingletonHandler.inGameCanvas);
        
        SetText(instantiated.GetComponent<TMP_Text>());

        foreach (var text in _texts)
        {
            text.position += Vector3.up * Difference;
        }
        
        _texts.Add(instantiated.transform);
        _lastTimer = StartCoroutine(DestroyEvent(instantiated));
        _previous = (damage, owner, moduleName, instantiated);

    }

    private IEnumerator  DestroyEvent(GameObject destroyable)
    {
        yield return new WaitForSeconds(Time);
        _texts.Remove(destroyable.transform); 
        Destroy(destroyable);
    }
}
