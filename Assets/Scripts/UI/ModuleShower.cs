using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class ModuleShower : MonoBehaviour
{
    [SerializeField]private float maxHeight;
    [SerializeField]private GameObject textInstance;
    
    private readonly List<Transform> _textTransforms = new();

    private void Awake()
    {
        enabled = PhotonNetwork.IsMasterClient;
    }

    //todo: introduce variables for transform
    public void ShowDamage(float health, float maxHealth, string caption)
    {
        var text = PhotonNetwork.Instantiate(textInstance.name, Vector3.zero, transform.rotation);
        text.transform.SetParent(transform, false);

        var textTMP = text.GetComponent<TMP_Text>();
        textTMP.text = caption;

        var green = health / maxHealth;
        textTMP.color = new Color(1 - green, green, 0);
        _textTransforms.Add(text.transform);
    }

    private void Update()
    {
        for (var i = 0; i < _textTransforms.Count; i++)
        {
            var text = _textTransforms[i];
            text.Translate(text.TransformDirection(Vector3.up) * Time.deltaTime);

            if (text.localPosition.y < maxHeight)
            {
                continue;
            }
            
            _textTransforms.Remove(text);
            PhotonNetwork.Destroy(text.gameObject);
        }
    }
}
