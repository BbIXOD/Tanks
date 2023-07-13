using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class ModuleShower : MonoBehaviour
{
    [SerializeField]private float maxHeight;
    [SerializeField]private GameObject textInstance;
    private Transform _camera;
    private TMP_Text _textTMP; 
    
    private readonly List<Transform> _textTransforms = new();

    private PhotonView _view;

    private void Awake()
    {
        _camera = Camera.main!.transform;
        _view = GetComponent<PhotonView>();
        _textTMP = textInstance.GetComponent<TMP_Text>();
    }

    public void ShowDamage(float health, float maxHealth, string caption)
    {
        _view.RPC("TextOutDamage", RpcTarget.All, health, maxHealth, caption);
    }
    
    
    [PunRPC]
    private void TextOutDamage(float health, float maxHealth, string caption)
    {
        _textTMP.text = caption;

        var green = health / maxHealth;
        _textTMP.color = new Color(1 - green, green, 0);

        var text = Instantiate(textInstance, transform);
        _textTransforms.Add(text.transform);
    }

    private void Update()
    {
        for (var i = 0; i < _textTransforms.Count; i++)
        {
            var text = _textTransforms[i];
            text.Translate(Vector3.up * Time.deltaTime);
            text.LookAt(_camera);

            if (text.localPosition.y < maxHeight)
            {
                continue;
            }
            
            _textTransforms.Remove(text);
            Destroy(text.gameObject);
        }
    }
}
