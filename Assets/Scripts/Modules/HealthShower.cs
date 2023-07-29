using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthShower : MonoBehaviour
{
    [SerializeField] private GameObject
        textInstance,
        barInstance;

    private const float IncrementY = 60;
    
    private Module[] _modules;

    private void Awake()
    {
        _modules = transform.GetComponentsInChildren<Module>();
    }

    public void ShowHealth(List<GameObject> children, Transform parent)
    {
        var posY = 0f;
        
        foreach (var module in _modules.Where(x => x != null))
        {
            var text = Instantiate(textInstance, parent);
            text.transform.Translate(0, posY, 0);
            text.GetComponent<TMP_Text>().text = module.caption;
            children.Add(text);

            var bar = Instantiate(barInstance, parent);
            bar.transform.Translate(0, posY, 0);
            var scale = bar.transform.localScale;
            scale.x *= module.Health / module.maxHealth;
            bar.transform.localScale = scale;
            bar.GetComponent<Image>().color = new Color(1 - scale.y, scale.y, 0);
            children.Add(bar);

            posY += IncrementY;
        }
    }

}
