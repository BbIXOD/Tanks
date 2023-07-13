using UnityEngine;
using UnityEngine.UI;

public class SensitivityScroll : MonoBehaviour
{
    private Scrollbar _scrollbar;

    private void Awake()
    {
        _scrollbar = GetComponent<Scrollbar>();
        _scrollbar.value = PlayerPrefsKeys.sensitivity;
    }

    public void ChangeValue()
    {
        PlayerPrefsKeys.sensitivity = _scrollbar.value;
    }
}
